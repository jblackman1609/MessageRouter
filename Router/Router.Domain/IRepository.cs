using System;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Domain;

public interface IRepository
{
    IMessageRepository MessageRepo { get; }
    ITemplateRepository TemplateRepo { get; }
    ITenantRecipientRepository TenantRecipientRepo { get; }
}
