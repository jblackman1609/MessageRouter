using System;
using Router.Domain.Models;

namespace Router.Domain.Repositories;

public interface ITenantRepository
{
    Task<Tenant> GetTenantAsync(decimal id);
    Task<Tenant> GetTenantByTemplateAsync(decimal templateId);
}
