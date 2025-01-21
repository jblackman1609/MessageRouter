using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;

namespace Router.Core.Handlers.Implementations;

internal class OTPMessageHandler : IMessageHandler
{
    private readonly IMessageRelayService _service;

    public OTPMessageHandler(IMessageRelayService service) =>
        _service = service;

    public async Task<MessageStatus> HandleAsync(Message message)
    {
        SmsModel smsModel = new()
        {
            Subject = message.Subject,
            Body = message.Body,
            TenantPhone = message.TenantPhone,
            RecipientPhone = message.RecipientPhone
        };

        (MessageStatus status, string log) = await _service
            .SendToRelayAsync(smsModel);
        
        return status;
    }
}
