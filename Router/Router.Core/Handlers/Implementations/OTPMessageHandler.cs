using System;
using System.Data;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers.Implementations;

public class OTPMessageHandler : MessageHandlerBase, IMessageHandler
{
    public OTPMessageHandler(
        IRepository repo, IService service) : base(repo, service) { }

    public async Task<MessageStatus> HandleAsync(Message message)
    {
        return await base.ProcessMessageAsync(message, false);
    }
}
