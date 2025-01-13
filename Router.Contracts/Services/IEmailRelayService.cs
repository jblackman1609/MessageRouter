using System;
using Router.Contracts.Models;

namespace Router.Contracts.Services;

public interface IEmailRelayService
{
    Task SendEmailAsync(EmailModel emailModel);
}
