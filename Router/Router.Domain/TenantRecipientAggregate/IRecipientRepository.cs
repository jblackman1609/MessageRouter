using System;

namespace Router.Domain.TenantRecipientAggregate;

public interface IRecipientRepository
{
    Task<decimal> GetRecipientIdAsync(string phone);
}
