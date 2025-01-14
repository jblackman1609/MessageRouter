using System;
using Router.Contracts.Models;

namespace Router.Contracts.Services;

public interface IPersistService
{
    Task AddMessgeAsync(MessageDto messageDTO);
    Task UpdateMessageAsync(MessageDto messageDto, string id);
    Task<RecipientDto> GetRecipientAsync(string phone);
    Task<TenantDto> GetTenantByTemplateIdAsync(decimal templateId);
    Task<CountryDataDto> GetCountryDataAsync(string phone);
}
