using System;

namespace Router.Domain.MessageAggregate;

public interface IMessageRepository
{
    public Task AddMessageAsync(Message message);
    public Task UpdateMessageAsync(Message message);
    public Task<Message> GetMessageAsync(decimal id);
}
