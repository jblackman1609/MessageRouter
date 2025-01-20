using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Router.External.DataModels;

[Table("CountryData")]
public class CountryData
{
    [Key]
    public decimal Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CountryCode { get; set; }
    public bool IsOptInRequired { get; set; }
    public bool IsOTPAllowed { get; set; }
}
