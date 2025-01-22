using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Router.External.DataModels;

[Table("Recipients")]
public class RecipientModel
{
    [Key]
    public decimal Id { get; set; }
    public string? Phone { get; set; }
    public List<TenantRecipientModel>? TenantRecipients { get; set; }
    public decimal CountryDataId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
