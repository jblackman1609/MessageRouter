using System;

namespace Router.Domain.TenantRecipientAggregate;

public class Tenant
{
    public decimal Id { get; private set; }
    private string _phone = "";
    public TenantType Type { get; private set; }

    public Tenant(string phone) => _phone = phone;

    public string Phone => _phone = Phone;
    public TenantType GetTenantType()
    {
        return Type;
    }
}

public enum TenantType
{
    Default, AssumedOptIn, OTP
}
