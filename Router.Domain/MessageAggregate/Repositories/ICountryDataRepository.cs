using System;
using Router.Domain.Models;

namespace Router.Domain.Repositories;

public interface ICountryDataRepository
{
    Task<CountryData> GetCountryDataAsync(decimal id);
    Task<CountryData> GetCountryDataAsync(string code);
    Task<List<CountryData>> GetAllCountryDataAsync();
}
