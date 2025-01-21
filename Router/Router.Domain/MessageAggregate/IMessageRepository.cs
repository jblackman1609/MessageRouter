using System;

namespace Router.Domain.MessageAggregate;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
    Task UpdateMessageAsync(Message message);
}
