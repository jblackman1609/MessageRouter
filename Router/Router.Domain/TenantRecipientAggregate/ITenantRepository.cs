using System;

namespace Router.Domain.TenantRecipientAggregate;

public interface ITenantRepository
{
    Task<Tenant> GetTenantAsync(decimal id);
}
