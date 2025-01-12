using System;

namespace Router.Domain.Models;

public class CountryData
{
    public string? CountryCode { get; set; }
    public bool IsOptInRequired { get; set; }
    public bool IsOTPAllowed { get; set; }
}
