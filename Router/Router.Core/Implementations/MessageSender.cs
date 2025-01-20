using System;
using Router.Core.Interfaces;

namespace Router.Core.Implementations;

internal class MessageSender : IMessageSender
{
    public IMessageHandler Handler { get; set; } = null!;

    public Task SendMessageAsync()
    {
        throw new NotImplementedException();
    }
}
