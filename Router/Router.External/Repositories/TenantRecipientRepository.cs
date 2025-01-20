using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;
using Router.External.Contexts;
using Router.External.DataModels;

namespace Router.External.Repositories;

internal class TenantRecipientRepository : ITenantRecipientRepository
{
    private readonly IMessageRouterContext _context;

    public TenantRecipientRepository(IMessageRouterContext context) => _context = context;

    public async Task AddTenantRecipientAsync(TenantRecipient tenantRecipient)
    {
        await _context.TenantRecipients.AddAsync(tenantRecipient.Map());
        await _context.SaveAsync();
    }

    public Task<Recipient> GetRecipientAsync(decimal recipientId)
    {
        throw new NotImplementedException(); 
    }

    public async Task<Tenant> GetTenantAsync(decimal templateId)
    {
        return (await _context.Tenants
            .Join(_context!.Templates, t => t.Id, tm => tm.TenantId, (t, tm) => new { t, tm })
            .FirstOrDefaultAsync(ttm => ttm.tm.Id == templateId))!.t.Map();
    }

    public async Task<TenantRecipient> GetTenantRecipientAsync(decimal tenantId, decimal recipientId)
    {
        return (await _context.TenantRecipients
            .FirstOrDefaultAsync(tr => tr.TenantId == tenantId && tr.RecipientId == recipientId))!.Map() ??
            throw new NullReferenceException();
    }

    public async Task UpdateTenantRecipientAsync(TenantRecipient tenantRecipient)
    {
        _context.TenantRecipients.Update(tenantRecipient.Map());
        await _context.SaveAsync();
    }
}
