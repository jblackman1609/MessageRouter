using System;

namespace Router.Contracts.Services;

public interface IEmailRelayService
{
    Task SendEmailAsync();
}
