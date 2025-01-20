using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Router.Domain.MessageAggregate;

namespace Router.External.DataModels;

[Table("Tenants")]
public class TenantModel
{
    [Key]
    public decimal Id { get; set; }
    public string? Phone { get; set; }
    public List<TenantRecipientModel>? TenantRecipients { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public List<TemplateModel>? Templates { get; set; }
}
