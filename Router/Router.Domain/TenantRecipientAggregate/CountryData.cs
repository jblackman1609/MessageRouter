using System;

namespace Router.Domain.TenantRecipientAggregate;

public class CountryData
{
    public decimal Id { get; private set; }
    public bool IsOptInRequired { get; private set; }
    public bool IsOTPAllowed { get; private set; }    
}
