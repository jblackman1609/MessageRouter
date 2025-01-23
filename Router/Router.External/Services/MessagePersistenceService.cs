using System;
using Router.Core.Services;
using Router.Domain;
using Router.External.Contexts;
using Router.External.Repositories;
using TorchSharp.Modules;

namespace Router.External.Services;

public class MessagePersistenceService : IMessagePersistenceService
{
    private readonly Lazy<IRepository> _lazyRepo;

    public MessagePersistenceService(IMessageRouterContext context) =>
        _lazyRepo = new Lazy<IRepository>(() => new Repository(context));
    
    public IRepository Repository => _lazyRepo.Value;
}
