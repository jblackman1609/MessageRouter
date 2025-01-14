using System;

namespace Router.Contracts.Models;

public class MessageResponse
{
    public bool Success { get; set; }
    public MessageStatus Status { get; set; }
    public string? MessageLog { get; set; }
    public string? ProviderId { get; set; }
}
