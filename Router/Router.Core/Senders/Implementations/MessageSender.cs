using System;
using Router.Core.Handlers;
using Router.Domain.MessageAggregate;

namespace Router.Core.Senders.Implementations;

internal class MessageSender : Message, IMessageSender
{
    public IMessageHandler Handler { get; set; } = null!;
    
    public async Task SendMessageAsync()
    {
        await Handler.HandleAsync(this);
    }
}
