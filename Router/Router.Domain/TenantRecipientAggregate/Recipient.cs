using System;

namespace Router.Domain.TenantRecipientAggregate;

public class Recipient
{
    public decimal Id { get; private set; }
    private string _phone = "";
    
    public Recipient(string phone) => _phone = phone;

    public string Phone => _phone;
}
