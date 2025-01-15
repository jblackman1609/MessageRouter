using System;
using Router.Domain.Models;

namespace Router.Domain.Repositories;

public interface IRecipientRepository
{
    Task<Recipient> GetRecipientAsync(decimal id);
    Task<Recipient> GetRecipientAsync(string phoneNumber);
}
