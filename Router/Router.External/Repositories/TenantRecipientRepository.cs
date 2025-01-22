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

    public async Task<CountryData> GetCountryDataAsync(string phone)
    {
        return (await _context.Countries
            .Join(_context.Recipients, c => c.Id, r => r.CountryDataId, (c, r) => new { c, r })
            .Where(cr => cr.r.Phone == phone)
            .FirstOrDefaultAsync())!.c.Map();
    }

    public async Task<Recipient> GetRecipientAsync(string phone)
    {
        return (await _context.Recipients
            .FirstOrDefaultAsync(r => r.Phone == phone))!.Map() ??
            throw new NullReferenceException(); 
    }

    public async Task<Tenant> GetTenantAsync(decimal templateId)
    {
        return (await _context.Tenants
            .Join(_context!.Templates, t => t.Id, tm => tm.TenantId, (t, tm) => new { t, tm })
            .FirstOrDefaultAsync(ttm => ttm.tm.Id == templateId))!.t.Map();
    }

    public async Task<TenantRecipient> GetTenantRecipientAsync(decimal templateId, string phone)
    {
        return await Task.FromResult(_context.Tenants
            .Join(_context.Templates, tn => tn.Id, tm => tm.TenantId, (tn, tm) => new { tn, tm })
            .Join(_context.TenantRecipients, tntm => tntm.tn.Id, tr => tr.TenantId, (tntm, tr) => new { tntm, tr })
            .Join(_context.Recipients, tntmtr => tntmtr.tr.RecipientId, r => r.Id, (tntmtr, r) => new { tntmtr, r })
            .FirstOrDefault(tntmtrr => tntmtrr.tntmtr.tntm.tm.Id == templateId && tntmtrr.r.Phone == phone)!.tntmtr.tr.Map());                    
    }

    public async Task UpdateTenantRecipientAsync(TenantRecipient tenantRecipient)
    {
        _context.TenantRecipients.Update(tenantRecipient.Map());
        await _context.SaveAsync();
    }
}
