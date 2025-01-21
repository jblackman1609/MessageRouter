using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers.Implementations;

internal class DefaultMessageHandler : IMessageHandler
{
    public Task<MessageStatus> HandleAsync(Message message)
    {
        throw new NotImplementedException();
    }
}
