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
    private readonly IPredictionService _pService;
    private readonly IMessageService _mService;
    
    public DefaultMessageHandler(
        IRepository repo, IService service, IPredictionService pService, IMessageService mService) :
        base(repo, service) => (_mService, _pService) = (mService, pService);
    
    public async Task<MessageStatus> HandleAsync(Message message)
    {
        (bool isOptedIn, bool isoptedOut, bool isBlocked) =
            await _mService.GetRecipientDataAsync(message.TemplateId, message.RecipientPhone);
                
        if (isBlocked)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_BLOCKED);
            await _mService.UpsertMessageAsync(message);

            return message.Status;
        }

        else if (!isOptedIn || isoptedOut)
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLog.DENIED_OPTIN);
            await _mService.UpsertMessageAsync(message);
            // Get Optin Request template and send it.

            return message.Status;
        }

        bool isPII = await _pService.PredictAsync(new()
        {
            BodyText = message.Body
        });

        return await base.ProcessMessageAsync(message, isPII);
    }
}
