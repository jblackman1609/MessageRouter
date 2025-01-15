using System;

namespace Router.Domain.TenantRecipientAggregate;

public interface IRecipientRepository
{
    Task<Recipient> GetRecipientAsync(string phone);
}
