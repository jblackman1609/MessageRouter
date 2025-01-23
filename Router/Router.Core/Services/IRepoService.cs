using System;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Services;

public interface IRepoService : 
    IMessageRepository, ITemplateRepository, ITenantRecipientRepository
{

}
