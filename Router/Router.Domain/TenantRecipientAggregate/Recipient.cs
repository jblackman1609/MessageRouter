using System;

namespace Router.Domain.TenantRecipientAggregate;

public class Recipient
{
    public decimal Id { get; private set; }
    private string _phone = "";

    public decimal CountryDataId { get; private set; }
    public bool IsBlocked { get; private set; }    
    
    public Recipient(string phone)
    {
        _phone = phone;
        Status = BlockStatus.None;
    }

    public string Phone => _phone;

    public BlockStatus Status { get; private set; }

    public void UpdateBlockStatus(BlockStatus status)
    {
        if (Status == status)
            throw new ArgumentException($"Block status already set to {nameof(status)}.");

        else Status = status;
    }
}

public enum BlockStatus
{
    Permanent, Temporary, None
}
