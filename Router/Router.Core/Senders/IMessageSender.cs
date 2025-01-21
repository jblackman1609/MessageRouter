using System;
using Router.Core.Handlers;
using Router.Domain.MessageAggregate;

namespace Router.Core.Senders;

public interface IMessageSender
{
    IMessageHandler Handler { get; }

    Task SendMessageAsync();
}
