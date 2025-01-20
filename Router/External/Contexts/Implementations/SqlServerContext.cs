using System;
using Microsoft.EntityFrameworkCore;
using Router.External.DataModels;

namespace Router.External.Contexts.Implementations;

public class SqlServerContext : DbContext, IMessageRouterContext
{
    public DbSet<TemplateModel> Templates { get; set; }

    public DbSet<MessageModel> Messages { get; set; }
    public DbSet<TenantRecipientModel> TenantRecipients { get; set; }

    public DbSet<TenantModel> Tenants { get; set; }

    public DbSet<RecipientModel> Recipients { get; set; }

    public DbSet<CountryData> Countries { get; set; }

    public SqlServerContext(DbContextOptions<SqlServerContext> opts) : base(opts) { }

    public async Task SaveAsync()
    {
        await base.SaveChangesAsync();
    }
}
