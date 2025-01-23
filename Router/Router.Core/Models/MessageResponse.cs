using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Models;

public class MessageResponse
{
    public bool IsSuccess { get; set; }
    public MessageStatus Status { get; set; }
}
