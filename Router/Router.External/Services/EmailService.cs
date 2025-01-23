using System;
using Router.Core.Models;
using Router.Core.Services;

namespace Router.External.Services;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(EmailModel emailModel)
    {
        throw new NotImplementedException();
    }
}
