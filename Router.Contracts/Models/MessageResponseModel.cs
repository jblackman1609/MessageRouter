using System;

namespace Router.Contracts.Models;

public class MessageResponseModel
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}
