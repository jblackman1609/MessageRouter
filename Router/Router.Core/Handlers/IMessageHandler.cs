using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers;

public interface IMessageHandler
{
    Task<(bool, string)> HandleAsync(Message message);
}
