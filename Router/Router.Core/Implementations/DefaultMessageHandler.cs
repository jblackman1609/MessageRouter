using System;
using Router.Core.Interfaces;
using Router.Domain.MessageAggregate;

namespace Router.Core.Implementations;

internal class DefaultMessageHandler : IMessageHandler
{
    private readonly ITemplateRepository _templateRepo;
    
    public DefaultMessageHandler(ITemplateRepository templateRepo)
    {
        _templateRepo = templateRepo;
    }
    
    public Task<(bool, string)> HandleAsync(Message message)
    {
        throw new NotImplementedException();
    }
}
