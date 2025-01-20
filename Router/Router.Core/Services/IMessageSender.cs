using System;
using Router.Core.Handlers;
using Router.Domain.MessageAggregate;

namespace Router.Core.Services;

public interface IMessageSender
{
    IMessageHandler Handler { get; }

    Task<(bool, string)> SendMessageAsync();
}
