using System;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Core.Services;

public interface IMessageService
{
    Task<(bool, bool, bool)> GetRecipientDataAsync(decimal templateId, string rPhone);
    Task UpsertMessageAsync(Message message);
    Task<Template> GetTemplateAsync(decimal templateId);
    Task<(bool, bool)> GetCountryDataAsync(string rPhone);
    Task<TenantType> GetTenantTypeAsync(decimal tenantId);
}
