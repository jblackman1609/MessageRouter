using System;
using Router.Core.Handlers;
using Router.Domain.MessageAggregate;

namespace Router.Core.Services.Implementations;

internal class MessageSender : Message, IMessageSender
{
    public MessageSender(string subject, string tenantPhone, string recipientPhone, decimal templateId) : 
        base(subject, tenantPhone, recipientPhone, templateId)
    {
    }

    public IMessageHandler Handler { get; set; } = null!;

    public async Task<(bool, string)> SendMessageAsync()
    {
        return await Handler.HandleAsync(this);
    }
}
