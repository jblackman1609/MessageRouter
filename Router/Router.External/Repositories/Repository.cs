using System;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;
using Router.External.Contexts;

namespace Router.External.Repositories;

internal class Repository : IRepository
{
    private readonly Lazy<IMessageRepository> _lazymessageRepo;
    private readonly Lazy<ITemplateRepository> _lazytemplateRepo;
    private readonly Lazy<ITenantRecipientRepository> _lazytenantRecipientRepo;

    public Repository(IMessageRouterContext context)
    {
        _lazymessageRepo = new Lazy<IMessageRepository>(() => new MessageRepository(context));
        _lazytemplateRepo = new Lazy<ITemplateRepository>(() => new TemplateRepository(context));
        _lazytenantRecipientRepo = new Lazy<ITenantRecipientRepository>(() => new TenantRecipientRepository(context));
    }
    public IMessageRepository MessageRepo => _lazymessageRepo.Value;

    public ITemplateRepository TemplateRepo => _lazytemplateRepo.Value;

    public ITenantRecipientRepository TenantRecipientRepo => _lazytenantRecipientRepo.Value;
}
