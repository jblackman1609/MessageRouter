using System;

namespace Router.Domain.TenantRecipientAggregate;

public class Recipient
{
    public decimal Id { get; private set; }
    private string _phone = "";
    public decimal CountryDataId { get; private set; }
    
    public Recipient(string phone) => _phone = phone;

    public string Phone => _phone;
}
