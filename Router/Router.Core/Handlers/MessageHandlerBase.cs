using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers;

public abstract class MessageHandlerBase
{
    private readonly IRepository _repo;
    private readonly IService _service;

    protected MessageHandlerBase(IRepository repo, IService service) => 
        (_repo, _service) = (repo , service);

    protected virtual async Task<MessageStatus> ProcessMessageAsync(Message message, bool isPII)
    {
        if (isPII)
        {
            await PersistMessageAsync(message, MessageStatus.Denied, MessageLog.DENIED_PII);
            
            await _service.SendEmailAsync(new EmailModel
            {
                Subject = "Possible PII Notification",
                Body = "",
                FromAddress = "",
                ToAddresses = new List<string>()
            });
            
            await PersistMessageAsync(message, MessageStatus.PendingReview, MessageLog.PENDING_REVIEW);
        }
        
        else
        {
            await PersistMessageAsync(message, MessageStatus.Approved, MessageLog.APPROVED);
            
            (MessageStatus status, string log) = await _service.SendToRelayAsync(new SmsModel
            {
                Subject = message.Subject,
                Body = message.Body,
                TenantPhone = message.TenantPhone,
                RecipientPhone = message.RecipientPhone
            });

            await PersistMessageAsync(message, status, log);
        }

        return message.Status;
    }

    private async Task PersistMessageAsync(Message message, MessageStatus status, string log)
    {
        message.UpdateStatus(status);
        message.AddMessageLog(log);

        if (log is MessageLog.DENIED_PII)
        {
            message.Body.Encrypt();
        }        
        
        await _repo.MessageRepo.UpdateMessageAsync(message);
    }
}
