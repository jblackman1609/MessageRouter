using System;
using Router.Contracts.Models;

namespace Router.Contracts.Services;

public interface IMessageRelayService
{
    Task<MessageResponse> SendToRelayAsync(TenantMessage tenantMessage);
}
