using System;

namespace Router.Domain.TenantRecipientAggregate;

public interface ITenantRecipientRepository
{
    Task AddTenantRecipientAsync(TenantRecipient tenantRecipient);
    Task UpdateTenantRecipientAsync(TenantRecipient tenantRecipient);
    Task<TenantRecipient> GetTenantRecipientAsync(decimal tenantId, decimal recipientId);
    Task<Tenant> GetTenantAsync(decimal tenantId);
    Task<Recipient> GetRecipientAsync(decimal recipientId);
}
