using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Interfaces;

public interface IMessageHandler
{
    Task<(bool, string)> HandleAsync(Message message);
}
