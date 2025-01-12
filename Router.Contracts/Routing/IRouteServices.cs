using System;
using Router.Contracts.Models;

namespace Router.Contracts.Routing;

public interface IRouteServices
{
    Task<bool> IsOptInRequiredAsync(string code);
    Task<bool> IsOTPAllowedAsync(string code);
    Task<bool> IsAssumedOptInTenantAsync(decimal id);
    Task<bool> IsOTPTenantAsync(decimal id);
    Task<TenantType?> GetTenantTypeAsync(decimal id);
    Task<TenantDto> GetTenantByTemplateIdAsync(decimal id);
    Task AddMessgeAsync(MessageDto messageDTO);
}
