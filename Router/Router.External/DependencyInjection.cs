

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;
using Router.External.Contexts;
using Router.External.Contexts.Implementations;
using Router.External.Repositories;
using Router.External.Services;

namespace Router.External;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalServices(
        this IServiceCollection services, string connectionString, 
        string relayClient, string baseAddress)
    {
        services
            .AddDbContextPool<SqlServerContext>(opts => opts.UseSqlServer(connectionString))
            .AddSingleton<IMessageRouterContext, SqlServerContext>()
            .AddScoped<ITemplateRepository, TemplateRepository>()
            .AddScoped<ITenantRecipientRepository, TenantRecipientRepository>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddScoped<IRepoService, RepoService>()
            .AddScoped<IPredictionService, PredictionService>()
            .AddScoped<IMessageRelayService, MessageRelayService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IMessageService, MessageService>()
            .AddHttpClient(relayClient, client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

        return services;
    }
}
