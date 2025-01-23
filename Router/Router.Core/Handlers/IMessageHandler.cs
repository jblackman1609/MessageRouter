using System;
using Router.Core.Models;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers;

public interface IMessageHandler
{
    Task<MessageStatus> HandleAsync(Message message, HandlerType type);
}
