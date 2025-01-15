using System;

namespace Router.Contracts.Models;

public class TenantDto
{
    public TenantType Type { get; set; }
}

public enum TenantType
{
    OTP,
    AssumedOptIn,
    Default
}
