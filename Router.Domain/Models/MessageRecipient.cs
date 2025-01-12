using System;

namespace Router.Domain.Models;

public class MessageRecipient
{
    public bool IsOptedIn { get; set; }
    public bool IsOptedOut { get; set; }
    public decimal RecipientId { get; set; }
    public Recipient? Recipient { get; set; }
    public decimal MessageId { get; set; }
    public Message? Message { get; set; }
}
