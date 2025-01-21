using System;
using Router.Core.Models;
using Router.Domain.MessageAggregate;

namespace Router.Core.Services;

public interface IMessageRelayService
{
    Task<(MessageStatus, string)> SendToRelayAsync(SmsModel sms);
}
