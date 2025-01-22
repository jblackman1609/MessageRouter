using System;
using System.Net.Cache;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class DefaultMessageHandler : MessageHandlerBase, IMessageHandler
{
    private readonly IPredictionService _predictionService;
    private readonly IRepository _repo;
    
    public DefaultMessageHandler(
        IRepository repo, IMessageRelayService service, IPredictionService predictionService) :
        base(repo, service) => (_repo, _predictionService) = (repo, predictionService);
    
    public async Task<MessageStatus> HandleAsync(Message message)
    {
        Recipient recipient = await _repo.TenantRecipientRepo
            .GetRecipientAsync(message.RecipientPhone);

        TenantRecipient tenantRecipient = await _repo.TenantRecipientRepo
            .GetTenantRecipientAsync(message.TemplateId, message.RecipientPhone);
        
        if (recipient.IsBlocked)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_BLOCKED);

            return message.Status;
        }

        else if (!tenantRecipient.IsOptedIn)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_OPTIN);
            // Get Optin Request template and send it.

            return message.Status;
        }

        bool isPII = await _predictionService.PredictAsync(new()
        {
            BodyText = message.Body
        });

        return await base.ProcessMessageAsync(message, isPII);
    }
}
