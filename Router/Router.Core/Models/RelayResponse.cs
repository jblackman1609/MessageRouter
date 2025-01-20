using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Models;

public class RelayResponse
{
    public MessageStatus Status { get; set; }
    public string? MessageLog { get; set; }
    public bool Success { get; set; }    
}
