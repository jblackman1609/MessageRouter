using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;
using Router.External.Contexts;
using Router.External.Contexts.Implementations;
using Router.External.Repositories;

namespace Router.External;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, string connectionString)
    {
        return services
            .AddDbContextPool<SqlServerContext>(opts => opts.UseSqlServer(connectionString))
            .AddSingleton<IMessageRouterContext, SqlServerContext>()
            .AddScoped<ITemplateRepository, TemplateRepository>()
            .AddScoped<ITenantRecipientRepository, TenantRecipientRepository>()
            .AddScoped<IMessageRepository, MessageRepository>();
    }
}
