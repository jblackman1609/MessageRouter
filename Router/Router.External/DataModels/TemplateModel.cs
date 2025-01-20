using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Router.External.DataModels;

[Table("Templates")]
public class TemplateModel
{
    [Key]
    public decimal Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? Status { get; set; }
    public string? Text { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public decimal TenantId { get; set; }
    public List<MessageModel>? Messages { get; set; }
}
