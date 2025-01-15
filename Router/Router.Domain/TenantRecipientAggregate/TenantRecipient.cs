using System;

namespace Router.Domain.TenantRecipientAggregate;

public class TenantRecipient : AggregateRoot
{
    public decimal TenantId { get; private set; }
    public decimal RecipientId { get; private set; }
    private bool _isOptedIn;
    private bool _isOptedOut;

    public TenantRecipient(decimal id, decimal tenantId, decimal recipientId) :
        base(id)
    {
        TenantId = tenantId;
        RecipientId = recipientId;
    }

    public bool IsOptedIn => _isOptedIn;
    
    public bool IsOptedOut => _isOptedOut;
    
    public void OptIn()
    {
        _isOptedIn = true;
        _isOptedOut = false;
        LastUpdated = DateTime.UtcNow;
        UpdatedBy = "";
    }

    public void OptOut()
    {
        _isOptedOut = true;
        _isOptedIn = false;
        LastUpdated = DateTime.UtcNow;
        UpdatedBy = "";
    }    
}
