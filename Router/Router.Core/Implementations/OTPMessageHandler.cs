using System;
using Router.Core.Interfaces;
using Router.Domain.MessageAggregate;

namespace Router.Core.Implementations;

public class OTPMessageHandler : IMessageHandler
{
    private readonly ITemplateRepository _templateRepo;

    public OTPMessageHandler(ITemplateRepository templateRepo)
    {
        _templateRepo = templateRepo;
    }

    public async Task<(bool, string)> HandleAsync(Message message)
    {
        await Task.FromResult();
    }
}
