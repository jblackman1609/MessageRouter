using System;

namespace Router.External.DataModels;

public class TemplateModel
{
    public decimal Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? Status { get; set; }
    public string? Keywords { get; set; }
    public string? Text { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public decimal TenantId { get; set; }
}
