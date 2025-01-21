using System;
using Router.Core.Handlers;
using Router.Domain.MessageAggregate;

namespace Router.Core.Senders.Implementations;

internal class MessageSender : Message, IMessageSender
{
    public IMessageHandler Handler { get; set; } = null!;

    public MessageSender(string subject, string tenantPhone, string recipientPhone, decimal templateId) : 
        base(subject, tenantPhone, recipientPhone, templateId)
    {
    }
    public Task SendMessageAsync()
    {
        throw new NotImplementedException();
    }
}
