using System;
using Router.Core.Services;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers;

public class MessageProcessors
{
    public async Task PersistMessageAsync(IMessageRepository repo, Message message)
    {
        if (message.Status is MessageStatus.Denied && 
            message.MessageLogs.Contains(MessageLog.DENIED_PII))
        {
            message.Body.Encrypt();
        }

        await repo.UpdateMessageAsync(message);
    }

    public async Task ProcessMessageAsync(
        IMessageRelayService service, 
        IMessageRepository repo,
        Message message, bool isPII)
    {
        if (isPII)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_PII);
            
            await PersistMessageAsync(repo, message);

            // Send email notification
            message.UpdateStatus(MessageStatus.PendingReview);
            message.AddMessageLog(MessageLog.PENDING_REVIEW);
            
            await PersistMessageAsync(repo, message);
        }

        else
        {
            message.UpdateStatus(MessageStatus.Approved);
            message.AddMessageLog(MessageLog.APPROVED);
            
            await PersistMessageAsync(repo, message);
            await service.SendToRelayAsync(new Models.SmsModel
            {
                Subject = message.Subject,
                Body = message.Body,
                TenantPhone = message.TenantPhone,
                RecipientPhone = message.RecipientPhone
            });
        }
    }
}
