using System;

namespace Router.External.DataModels;

public class TenantRecipientModel
{
    public decimal Id { get; set; }
    public decimal TenantId { get; set; }
    public decimal RecipientId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsOptedIn { get; set; }
    public bool IsOptedOut { get; set; }
}
