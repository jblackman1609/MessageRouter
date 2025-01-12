using System;
using Router.Domain.Models;

namespace Router.Domain.Repositories;

public interface ITemplateRepository
{
    Task<Template> GetTemplateAsync(decimal id);
    Task<Template> GetTemplateAsync(string name);
    Task<List<Template>> GetAllTemplatesByTenantAsync(decimal tenantId);
}
