using System;

namespace Router.Domain.Models;

public class Message
{
    public string? GuidId { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? MessageLogs { get; set; }
    public decimal TemplateId { get; set; }
    public Template? Template { get; set; }
    public List<MessageRecipient>? MessageRecipients { get; set; }
}
