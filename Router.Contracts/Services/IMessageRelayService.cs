using System;
using Router.Contracts.Models;

namespace Router.Contracts.Services;

public interface IMessageRelayService
{
    Task<MessageResponseModel> SendToRelayAsync(TenantMessageModel tenantMessage);
}
