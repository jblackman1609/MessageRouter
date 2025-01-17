using System;

namespace Router.Domain.TenantRecipientAggregate;

public class Tenant
{
    public decimal Id { get; private set; }
    private string _phone = "";

    public Tenant(string phone) => _phone = phone;

    public string Phone => _phone = Phone;
}
