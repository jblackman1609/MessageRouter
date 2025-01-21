using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Handlers.Implementations;

internal class AssumedOptinMessageHandler : IMessageHandler
{
    private readonly ITemplateRepository _repo;
    private readonly IPredictionService _service;

    public AssumedOptinMessageHandler(ITemplateRepository repo,
        IPredictionService service) => (_repo, _service) = (repo, service);
        
    public async Task<(MessageStatus, string)> HandleAsync(Message message)
    {
        bool isPII = await _service.PredictAsync(new()
        {
            BodyText = message.Body
        });

        if (!isPII)
        {
            message.UpdateStatus(MessageStatus.Approved);
            message.AddMessageLog(MessageLogs.APPROVED);
        }

        else
        {
            message.UpdateStatus(MessageStatus.Denied);
            message.AddMessageLog(MessageLogs.DENIED_PII);
            message.Body.Encrypt();
        }

        return await Task.FromResult((MessageStatus.Accepted, ""));
    }
}
