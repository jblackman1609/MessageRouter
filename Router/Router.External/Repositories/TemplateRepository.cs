using System;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.External.Contexts;
using Router.External.DataModels;

namespace Router.External.Repositories;

internal class TemplateRepository : ITemplateRepository
{
    private readonly IMessageRouterContext _context;

    public TemplateRepository(IMessageRouterContext context) => _context = context;
    
    public async Task<Template> GetTemplateAsync(decimal id)
    {
        return (await _context.Templates
            .Include(t => t.Messages)
            .FirstOrDefaultAsync(t => t.Id == id))!.Map() ??
                throw new NullReferenceException();
    }

    public async Task UpdateTemplateAsync(Template template)
    {
        TemplateModel dataModel = template.Map();
        
        TemplateModel attached = await _context.Templates
            .Include(t => t.Messages)
            .FirstOrDefaultAsync(t => t.Id == template.Id) ??
                throw new NullReferenceException();
        
            foreach (MessageModel message in dataModel.Messages!.ToList())
            {
                if (!attached.Messages!.Contains(message))
                    await _context.Messages.AddAsync(message);

                else _context.Messages.Update(message);
            }

        _context.Templates.Update(dataModel);
        await _context.SaveAsync();
    }
}
