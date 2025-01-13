using System;
using Router.Contracts.Models;

namespace Router.Contracts.Services;

public interface IPersistMessageService
{
    Task AddMessgeAsync(MessageDto messageDTO);
    Task UpdateMessageAsync(MessageDto messageDto, string id);
}
