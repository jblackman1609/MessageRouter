using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class AssumedOptinMessageHandler : IMessageHandler
{
    private readonly IMessageRepository _repo;
    private readonly IPredictionService _predictionService;
    private readonly IMessageRelayService _messageRelayService;
    public MessageStatus Status;

    public AssumedOptinMessageHandler(
        IMessageRepository repo,
        IPredictionService predictionService, 
        IMessageRelayService messageRelayService) => 
        (_repo, _predictionService, _messageRelayService) = 
        (repo, predictionService, messageRelayService);
        
    public async Task<MessageStatus> HandleAsync(Message message)
    {
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

        await _repo.UpdateMessageAsync(message);
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
            
            (Status, string log) = await _messageRelayService.SendToRelayAsync(new SmsModel
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
