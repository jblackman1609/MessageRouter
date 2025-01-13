using System;
using Router.Contracts.Models;

namespace Router.Contracts.Services;

public interface IRouteServices
{
    Task<bool> GetCountryDataAsync(string phone);
    Task<bool> GetTenantAsync(decimal id);
    Task<TenantDto> GetTenantByTemplateIdAsync(decimal id);    
}
