using System;
using System.Data.Common;

namespace Router.Domain;

public abstract class AggregateRoot
{
    public decimal Id { get; protected set; }    
    public DateTime DateCreated { get; protected set; }
    public DateTime LastUpdated { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public string? UpdatedBy { get; protected set; }

    protected AggregateRoot(decimal id) 
    {
        Id = id;
        DateCreated = DateTime.UtcNow;
        LastUpdated = DateTime.UtcNow;
        CreatedBy = "";
        UpdatedBy = "";
    } 
}
