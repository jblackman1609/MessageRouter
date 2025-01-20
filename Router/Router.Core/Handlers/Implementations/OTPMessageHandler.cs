using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers.Implementations;

public class OTPMessageHandler : IMessageHandler
{
    private readonly IMessageRelayService _relayService;

    public OTPMessageHandler(IMessageRelayService relayService)
    {
        _relayService = relayService;
    }

    public async Task<(bool, string)> HandleAsync(Message message)
    {
        SmsModel smsModel = new()
        {
            Subject = message.Subject,
            Body = message.Body,
            TenantPhone = message.TenantPhone,
            RecipientPhone = message.RecipientPhone
        };

        RelayResponse response = await _relayService.SendToRelayAsync(smsModel);

        if (response.Success)
        {
            return (true, response.MessageLog!);
        }

        else return (false, response.MessageLog!);
    }
}
