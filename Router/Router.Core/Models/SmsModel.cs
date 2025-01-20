using System;

namespace Router.Core.Models;

public class SmsModel
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? TenantPhone { get; set; }
    public string? RecipientPhone { get; set; }
}
