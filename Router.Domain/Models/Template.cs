using System;

namespace Router.Domain.Models;

public class Template
{
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public decimal TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public List<Message>? Messages { get; set; }
}
