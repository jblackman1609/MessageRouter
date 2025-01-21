using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class AssumedOptinMessageHandler : IMessageHandler
{
    private readonly IIdentifyPIIService _piiService;
    private readonly IMessageRelayService _relayService;

    public AssumedOptinMessageHandler(IIdentifyPIIService piiService,
        IMessageRelayService relayService)
    {
        _piiService = piiService;
        _relayService = relayService;
    }
    
    public async Task<(bool, string)> HandleAsync(Message message)
    {
        PIIResponse piiResponse = await _piiService.PredictAsync(new PIIRequest());
        if (!piiResponse.PredictedLabel)
        {
            SmsModel smsModel = new()
            {
                Subject = message.Subject,
                Body = message.Body,
                TenantPhone = message.TenantPhone,
                RecipientPhone = message.RecipientPhone
            };

            RelayResponse relayResponse = await _relayService.SendToRelayAsync(smsModel);

            if (relayResponse.Success)
            {
                return (true, relayResponse.MessageLog!);
            }

            else return (false, relayResponse.MessageLog!);
        }

        else
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLogs.DENIED_PII);
            return (false, )
        }
    }
}
