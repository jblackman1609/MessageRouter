using System;

namespace Router.Domain.TenantRecipientAggregate;

public interface ITenantRepository
{
    Task<string> GetTenantPhoneAsync(decimal id);
}
