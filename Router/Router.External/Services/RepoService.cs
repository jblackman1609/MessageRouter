using System;
using Router.Core.Services;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.External.Services;

internal class RepoService : IRepoService
{
    private readonly IMessageRepository _messageRepo;
    private readonly ITemplateRepository _templateRepo;
    private readonly ITenantRecipientRepository _tenantRecipientRepo;

    public RepoService(
        IMessageRepository messageRepo, ITemplateRepository templateRepo, ITenantRecipientRepository tenantRecipientRepo) =>
        (_messageRepo, _templateRepo, _tenantRecipientRepo) = (messageRepo, templateRepo, tenantRecipientRepo);

    public async Task AddMessageAsync(Message message) =>
        await _messageRepo.AddMessageAsync(message);

    public async Task AddTenantRecipientAsync(TenantRecipient tenantRecipient) =>
        await _tenantRecipientRepo.AddTenantRecipientAsync(tenantRecipient);

    public async Task<CountryData> GetCountryDataAsync(string phone) =>
        await _tenantRecipientRepo.GetCountryDataAsync(phone);

    public async Task<Recipient> GetRecipientAsync(string phone) =>
        await _tenantRecipientRepo.GetRecipientAsync(phone);

    public async Task<Template> GetTemplateAsync(decimal id) =>
        await _templateRepo.GetTemplateAsync(id);

    public async Task<Tenant> GetTenantAsync(decimal templateId) =>
        await _tenantRecipientRepo.GetTenantAsync(templateId);

    public async Task<TenantRecipient> GetTenantRecipientAsync(decimal templateId, string phone) =>
        await _tenantRecipientRepo.GetTenantRecipientAsync(templateId, phone);

    public async Task UpdateMessageAsync(Message message) =>
        await _messageRepo.UpdateMessageAsync(message);

    public async Task UpdateTemplateAsync(Template template) =>
        await _templateRepo.UpdateTemplateAsync(template);

    public async Task UpdateTenantRecipientAsync(TenantRecipient tenantRecipient) =>
        await _tenantRecipientRepo.UpdateTenantRecipientAsync(tenantRecipient);
}
