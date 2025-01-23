using System;

namespace Router.Domain.TenantRecipientAggregate;

public interface ITenantRecipientRepository
{
    Task AddTenantRecipientAsync(TenantRecipient tenantRecipient);
    Task UpdateTenantRecipientAsync(TenantRecipient tenantRecipient);
    Task<TenantRecipient> GetTenantRecipientAsync(decimal templateId, string phone);
    Task<Tenant> GetTenantAsync(decimal templateId);
    Task<Recipient> GetRecipientAsync(string phone);
    Task<CountryData> GetCountryDataAsync(string phone);
}
