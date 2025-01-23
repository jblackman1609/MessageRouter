using System;
using System.Net.Cache;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class MessageHandler : MessageHandlerBase, IMessageHandler
{
    public MessageHandler(
        IRepoService repoService, IMessageService messageService) : base(repoService, messageService) { }
    
    public async Task<MessageStatus> HandleAsync(Message message, HandlerType type)
    {
        return await base.ProcessMessageAsync(message, type);
    }
}
