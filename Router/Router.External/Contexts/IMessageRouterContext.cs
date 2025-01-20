using System;
using Microsoft.EntityFrameworkCore;
using Router.External.DataModels;

namespace Router.External.Contexts;

internal interface IMessageRouterContext
{
    DbSet<TemplateModel> Templates { get; }
    DbSet<MessageModel> Messages { get; }
    DbSet<TenantRecipientModel> TenantRecipients { get; }
    DbSet<TenantModel> Tenants { get; }
    DbSet<RecipientModel> Recipients { get; }
    DbSet<CountryData> Countries { get; }

    Task SaveAsync();
}

