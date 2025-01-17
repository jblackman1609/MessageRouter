using System;

namespace Router.External.DataModels;

public class RecipientModel
{
    public decimal Id { get; set; }
    public string? Phone { get; set; }
    public List<TenantRecipientModel>? TenantRecipients { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
