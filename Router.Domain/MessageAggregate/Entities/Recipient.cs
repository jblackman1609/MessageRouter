using System;

namespace Router.Domain.Models;

public class Recipient
{
    public string? PhoneNumber { get; set; }
    public List<MessageRecipient>? MessageRecipients { get; set; }
}
