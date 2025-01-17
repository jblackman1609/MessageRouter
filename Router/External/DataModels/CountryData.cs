using System;

namespace Router.External.DataModels;

public class CountryData
{
    public decimal Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CountryCode { get; set; }
    public bool IsOptInRequired { get; set; }
    public bool IsOTPAllowed { get; set; }
}
