using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers.Implementations;

internal class OTPMessageHandler : IMessageHandler
{
    public Task<(MessageStatus, string)> HandleAsync(Message message)
    {
        throw new NotImplementedException();
    }
}
