using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class AssumedOptinMessageHandler : MessageHandlerBase, IMessageHandler
{
    private readonly IPredictionService _predictionService;
    
    public AssumedOptinMessageHandler(
        IRepository repo, IMessageRelayService service, IPredictionService predictionService) :
        base(repo, service) => _predictionService = predictionService;
        
    public async Task<MessageStatus> HandleAsync(Message message)
    {
        bool isPII = await _predictionService.PredictAsync(new()
        {
            BodyText = message.Body
        });

        await base.ProcessMessageAsync(message, isPII);

        return MessageStatus.Transmitted;
    }
}
