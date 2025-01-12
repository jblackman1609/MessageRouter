using System;

namespace Router.Domain.Repositories;

public interface IRepository
{
    ICountryDataRepository CountryDataRepo { get; }
    IMessageRecipientRepository MessageRecipientRepo { get; }
    IMessageRepository MessageRepo { get; }
    IRecipientRepository RecipientRepo { get; }
    ITemplateRepository TemplateRepo { get; }
    ITenantRepository TenantRepo { get; }
}
