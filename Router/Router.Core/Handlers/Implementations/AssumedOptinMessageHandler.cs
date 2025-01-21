using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class AssumedOptinMessageHandler : IMessageHandler
{
    public Task<(MessageStatus, string)> HandleAsync(Message message)
    {
        throw new NotImplementedException();
    }
}
