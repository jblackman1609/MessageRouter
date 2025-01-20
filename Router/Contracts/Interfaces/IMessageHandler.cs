using System;

namespace Contracts.Interfaces;

public interface IMessageHandler
{
    Task<(bool, string)> Handle(Message)
}
