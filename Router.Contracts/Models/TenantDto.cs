using System;

namespace Router.Contracts.Models;

public class TenantDto
{
    public decimal Id { get; set; }
    public string? Name { get; set; }
    public string? FromPhone { get; set; }
    public string? ClientId { get; set; }
    public string? ResponseUrl { get; set; }
    public TenantType? Type { get; set; }
}

public enum TenantType
{
    OTP,
    AssumedOptIn,
    Default
}
