using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class AssumedOptinMessageHandler : IMessageHandler
{
    private readonly ITemplateRepository _templateRepo;
    private readonly ITenantRecipientRepository _tenantRecipienRepo;
    private readonly IMessageRelayService _relayService;

    public AssumedOptinMessageHandler(ITemplateRepository templateRepo,
        ITenantRecipientRepository tenantRecipientRepo,
        IMessageRelayService relayService)
    {
        _templateRepo = templateRepo;
        _tenantRecipienRepo = tenantRecipientRepo;
        _relayService = relayService;
    }
    
    public async Task<(bool, string)> HandleAsync(Message message)
    {
        // Check for PII (PII interface)

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
