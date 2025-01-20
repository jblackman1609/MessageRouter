using System;
using Router.Core.Models;

namespace Router.Core.Services;

public interface IMessageRelayService
{
    Task<RelayResponse> SendToRelayAsync(SmsModel sms);
}
