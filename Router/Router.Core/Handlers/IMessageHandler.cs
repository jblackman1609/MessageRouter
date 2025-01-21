using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers;

public interface IMessageHandler
{
    Task<MessageStatus> HandleAsync(Message message);
}
