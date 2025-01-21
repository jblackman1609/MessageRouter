using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class DefaultMessageHandler : IMessageHandler
{
    private readonly IMessageRelayService _relayService;
    private readonly IPredictionService _predictionService;
    private readonly IMessageRepository _messageRepo;
    private readonly ITenantRecipientRepository _tenantRecipientRepo;
    public MessageStatus Status;

    public DefaultMessageHandler(
        IMessageRelayService relayService,
        IPredictionService predictionService,
        IMessageRepository messageRepo,
        ITenantRecipientRepository tenantRecipientRepo) =>
        (_relayService, _predictionService, _messageRepo, _tenantRecipientRepo) =
        (relayService, predictionService, messageRepo, tenantRecipientRepo);
    
    public async Task<MessageStatus> HandleAsync(Message message)
    {
        Tenant tenant = await _tenantRecipientRepo
            .GetTenantAsync(message.TemplateId);
        
        Recipient recipient = await _tenantRecipientRepo
            .GetRecipientAsync(message.RecipientPhone);

        TenantRecipient tenantRecipient = await _tenantRecipientRepo
            .GetTenantRecipientAsync(tenant.Id, recipient.Id);
        
        if (recipient.IsBlocked)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_BLOCKED);
        }

        else if (!tenantRecipient.IsOptedIn)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_OPTIN);
            // Get Optin Request template and send it.
        }

        bool isPII = await _predictionService.PredictAsync(new()
        {
            BodyText = message.Body
        });

        await ProcessMessageAsync(message, isPII);

        return Status;
    }

    private async Task PersistMessageAsync(Message message)
    {
        if (message.Status is MessageStatus.Denied && 
            message.MessageLogs.Contains(MessageLog.DENIED_PII))
        {
            message.Body.Encrypt();
        }

        await _messageRepo.UpdateMessageAsync(message);
    }

    private async Task ProcessMessageAsync(Message message, bool isPII)
    {
        if (isPII)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_PII);            
            await PersistMessageAsync(message);

            // Send email notification
            message.UpdateStatus(MessageStatus.PendingReview);
            message.AddMessageLog(MessageLog.PENDING_REVIEW);            
            await PersistMessageAsync(message);
        }

        else
        {
            message.UpdateStatus(MessageStatus.Approved);
            message.AddMessageLog(MessageLog.APPROVED);            
            await PersistMessageAsync(message);
            
            (Status, string log) = await _relayService.SendToRelayAsync(new SmsModel
            {
                Subject = message.Subject,
                Body = message.Body,
                TenantPhone = message.TenantPhone,
                RecipientPhone = message.RecipientPhone
            });

            message.UpdateStatus(Status);
            message.AddMessageLog(log);
            await PersistMessageAsync(message);
        }
    }
}
