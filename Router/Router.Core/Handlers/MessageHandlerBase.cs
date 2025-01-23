using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers;

public abstract class MessageHandlerBase
{
    private readonly IRepoService _repo;
    private readonly IMessageService _message;
    public MessageStatus Status; 
    public string Log = "";

    protected MessageHandlerBase(IRepoService repo, IMessageService message) => 
        (_repo, _message) = (repo , message);

    protected virtual async Task<MessageStatus> ProcessMessageAsync(Message message, HandlerType type)
    {
        switch (type)
        {
            case HandlerType.OTP:
                await ProcessNonPIIAsync(message);
                break;

            case HandlerType.AssumedOptin:
                if (await GetPredictionAsync(message))
                {
                    await ProcessPIIAsync(message);
                }

                else
                {
                    await ProcessNonPIIAsync(message);
                }
                break;

            case HandlerType.Default:
                (bool isOptedIn, bool isOptedOut, bool isBlocked) =
                    await ValidateRecipientAsync(message.TemplateId, message.RecipientPhone);
                
                if (!isBlocked && isOptedIn && !isOptedOut)
                {
                    Status = MessageStatus.Approved;
                    Log = MessageLog.APPROVED;

                    if (await GetPredictionAsync(message))
                    {
                        await ProcessPIIAsync(message);
                    }

                    else await ProcessNonPIIAsync(message);
                }

                else
                {
                    Status = MessageStatus.Denied;

                    if (isBlocked)
                        Log = MessageLog.DENIED_BLOCKED;

                    if (!isOptedIn || isOptedOut)
                        Log = MessageLog.DENIED_OPTIN;
                }

                await PersistMessageAsync(message, Status, Log);                
                break;
        }
        
        return Status;
    }

    private async Task PersistMessageAsync(Message message, MessageStatus status, string log)
    {
        message.UpdateStatus(status);
        message.AddMessageLog(log);

        if (log is MessageLog.DENIED_PII)
        {
            message.Body.Encrypt();
        }        
        
        await _repo.UpdateMessageAsync(message);
    }

    private async Task<bool> GetPredictionAsync(Message message)
    {
        return await _message.PredictAsync(new()
        {
            BodyText = message.Body
        });
    }

    private async Task ProcessPIIAsync(Message message)
    {
        await PersistMessageAsync(message, MessageStatus.Denied, MessageLog.DENIED_PII);
            
        await _message.SendEmailAsync(new EmailModel
        {
            Subject = "Possible PII Notification",
            Body = "",
            FromAddress = "",
            ToAddresses = new List<string>()
        });
            
        await PersistMessageAsync(message, MessageStatus.PendingReview, MessageLog.PENDING_REVIEW);
    }

    private async Task ProcessNonPIIAsync(Message message)
    {
        await PersistMessageAsync(message, MessageStatus.Approved, MessageLog.APPROVED);
            
        (Status, Log) = await _message.SendToRelayAsync(new()
        {
            Subject = message.Subject,
            Body = message.Body,
            TenantPhone = message.TenantPhone,
            RecipientPhone = message.RecipientPhone
        });

        await PersistMessageAsync(message, Status, Log);
    }

    private async Task<(bool, bool, bool)> ValidateRecipientAsync(
        decimal templateId, string phone)
    {
        TenantRecipient tenantRecipient = await _repo
            .GetTenantRecipientAsync(templateId, phone);

        Recipient recipient = await _repo
            .GetRecipientAsync(phone);

        return (tenantRecipient.IsOptedIn, tenantRecipient.IsOptedOut, recipient.IsBlocked);
    }
}
