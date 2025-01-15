using System;
using Router.Domain.Models;

namespace Router.Domain.Repositories;

public interface IMessageRecipientRepository
{
    Task AddMessageRecipientAsync(MessageRecipient messageRecipient);
    Task UpdateMessageRecipientAsync(MessageRecipient messageRecipient);
    Task<List<MessageRecipient>> GetAllMessageRecipientsByMessageAsync(decimal messageId);
    Task<List<MessageRecipient>> GetAllMessageRecipientsByRecipientAsync(decimal RecipientId);
    Task<MessageRecipient> GetMessageRecipientAsync(decimal messageId, decimal recipientId);
}
