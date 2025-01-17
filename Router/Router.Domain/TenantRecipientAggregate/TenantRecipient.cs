using System;

namespace Router.Domain.TenantRecipientAggregate;

public class TenantRecipient : AggregateRoot
{
    private bool _isOptedIn;
    private bool _isOptedOut;
    public decimal TenantId { get; private set; }
    public decimal RecipientId { get; private set; }
    
    public TenantRecipient(decimal id, decimal tenantId, decimal recipientId) :
        base(id)
    {
        TenantId = tenantId;
        RecipientId = recipientId;
        _isOptedIn = false;
        _isOptedOut = false;
    }

    public bool IsOptedIn => _isOptedIn;
    
    public bool IsOptedOut => _isOptedOut;
    
    public void OptIn()
    {
        if (!_isOptedIn)
        {
            _isOptedIn = true;
            _isOptedOut = false;
            LastUpdated = DateTime.UtcNow;
            UpdatedBy = "";
        }        
        
        else throw new ArgumentException($"Recipient Id: {RecipientId} is already opted int to Tenant Id: {TenantId}.");
    }

    public void OptOut()
    {
        if (!_isOptedOut)
        {
            _isOptedOut = true;
            _isOptedIn = false;
            LastUpdated = DateTime.UtcNow;
            UpdatedBy = "";
        }
        
        else throw new ArgumentException($"Recipient Id: {RecipientId} is already opted out of Tenant Id: {TenantId}.");
    }    
}
