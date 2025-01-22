using System;
using Router.Domain;

namespace Router.Core.Services;

public interface IMessagePersistenceService
{
    IRepository Repository { get; }
}
