using System;

namespace Router.Domain.TenantRecipientAggregate;

public class Recipient
{
    public decimal Id { get; private set; }
    private string _phone = "";
    private BlockStatus _status;
    public decimal CountryDataId { get; private set; }
    public bool IsBlocked { get; private set; }    
    
    public Recipient(string phone) => _phone = phone;

    public string Phone => _phone;

    public BlockStatus Status => _status;
}

public enum BlockStatus
{
    Permanent, Temporary, None
}
