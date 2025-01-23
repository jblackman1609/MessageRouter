using System;

namespace Router.Contracts.Models;

public class HandlerResponse
{
    public bool IsSuccess { get; set; }
    public MessageStatus Status { get; set; }
}
