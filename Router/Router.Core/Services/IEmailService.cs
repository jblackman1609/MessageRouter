using System;
using Router.Core.Models;

namespace Router.Core.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailModel emailModel);
}
