using System;

namespace Router.Domain.TenantRecipientAggregate;

public class Recipient
{
    public decimal Id { get; private set; }
    public string Phone { get; }

    public Recipient(string phone) => Phone = phone;
}
