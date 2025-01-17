using System;

namespace Router.External.DataModels;

public class MessageModel
{
    public decimal Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? GuidId { get; set; }
    public string? Subject { get; set; }
    public string? Status { get; set; }
    public string? Body { get; set; }
    public string? RecipientPhone { get; set; }
    public string? TenantPhone { get; set; }
    public decimal TemplateId { get; set; }
    public string? MessageLogs { get; set; }
}
