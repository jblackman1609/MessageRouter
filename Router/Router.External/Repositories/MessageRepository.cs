using System;
using Router.Domain.MessageAggregate;
using Router.External.Contexts;

namespace Router.External.Repositories;

internal class MessageRepository : IMessageRepository
{
    private readonly IMessageRouterContext _context;

    public MessageRepository(IMessageRouterContext context) => _context = context;

    public async Task AddMessageAsync(Message message)
    {
        await _context.Messages.AddAsync(message.Map());
        await _context.SaveAsync();
    }

    public async Task UpdateMessageAsync(Message message)
    {
        _context.Messages.Update(message.Map());
        await _context.SaveAsync();
    }
}
