using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Interfaces;

public interface IMessageSender
{
    IMessageHandler Handler { get; }

    Task SendMessageAsync();
}
