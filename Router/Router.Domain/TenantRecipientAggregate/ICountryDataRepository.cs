using System;

namespace Router.Domain.TenantRecipientAggregate;

public interface ICountryDataRepository
{
    Task<CountryData> GetCountryDataAsync(decimal id);
}
