using System;

namespace Router.Contracts.Models;

public class CountryDataDto
{
    public bool IsOTPAllowed { get; set; }
    public bool IsOptInRequired { get; set; }
    public bool IsOconus { get; set; }
}
