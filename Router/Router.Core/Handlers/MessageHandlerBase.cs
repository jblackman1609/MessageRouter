using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers;

public abstract class MessageHandlerBase
{
    private readonly IRepoService _repoService;
    private readonly IMessageService _messageService;
    public MessageStatus Status; 
    public string Log = "";

    protected MessageHandlerBase(IRepoService repoService, IMessageService messageService) => 
        (_repoService, _messageService) = (repoService , messageService);

    protected virtual async Task<MessageStatus> ProcessMessageAsync(Message message, HandlerType type)
    {
        switch (type)
        {
            case HandlerType.OTP:
                await PersistMessageAsync(message, MessageStatus.Approved, MessageLog.APPROVED);
            
                (Status, Log) = await _messageService.SendToRelayAsync(new SmsModel
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    TenantPhone = message.TenantPhone,
                    RecipientPhone = message.RecipientPhone
                });

                await PersistMessageAsync(message, Status, Log);
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
        
        await _repoService.UpdateMessageAsync(message);
    }

    private async Task<bool> GetPredictionAsync(Message message)
    {
        return await _messageService.PredictAsync(new()
        {
            BodyText = message.Body
        });
    }

    private async Task ProcessPIIAsync(Message message)
    {
        await PersistMessageAsync(message, MessageStatus.Denied, MessageLog.DENIED_PII);
            
        await _messageService.SendEmailAsync(new EmailModel
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
            
        (Status, Log) = await _messageService.SendToRelayAsync(new()
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
        TenantRecipient tenantRecipient = await _repoService
            .GetTenantRecipientAsync(templateId, phone);

        Recipient recipient = await _repoService
            .GetRecipientAsync(phone);

        return (tenantRecipient.IsOptedIn, tenantRecipient.IsOptedOut, recipient.IsBlocked);
    }
}
