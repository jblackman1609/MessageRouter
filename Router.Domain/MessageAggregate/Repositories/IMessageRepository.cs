using System;
using Router.Domain.Models;

namespace Router.Domain.Repositories;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
    Task UpdateMessageAsync(Message message);
    Task<Message> GetMessageAsync(decimal id);
    Task<Message> GetMessageAsync(string guid);
}
