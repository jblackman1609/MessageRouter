using System;
using Router.Contracts.Models;

namespace Router.Contracts.Services;

public interface IRouteMessageService
{
    
    Task<TenantDto> GetTenantAsync(decimal id);
    Task<TenantDto> GetTenantByTemplateIdAsync(decimal id);   
    Task<RecipientDto> GetRecipientAsync(string phone); 
    Task<MessageResponse> RouteAssumedOptinAsync(TenantMessage tenantMessage);
    Task<MessageResponse> RouteDefaultAsync(TenantMessage tenantMessage);
}
