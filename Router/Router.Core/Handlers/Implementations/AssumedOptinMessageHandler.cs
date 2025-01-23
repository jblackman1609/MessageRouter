using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class AssumedOptinMessageHandler : MessageHandlerBase, IMessageHandler
{
    private readonly IPredictionService _pService;
    
    public AssumedOptinMessageHandler(
        IRepository repo, IService service, IPredictionService pService) :
        base(repo, service) => _pService = pService;
        
    public async Task<MessageStatus> HandleAsync(Message message)
    {
        bool isPII = await _pService.PredictAsync(new()
        {
            BodyText = message.Body
        });

        return await base.ProcessMessageAsync(message, isPII);
    }
}
