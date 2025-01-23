using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Models;

public class HandlerResponse
{
    public bool IsSuccess { get; set; }
    public MessageStatus Status { get; set; }
}
