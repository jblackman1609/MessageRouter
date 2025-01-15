using System;
using Router.Contracts.Models;

namespace Router.Contracts.Routing;

public interface IRouteStrategy
{
    Task<MessageResponse> RouteOTPAsync(MessageDto message);
    Task RouteAssumedOptinAsync(MessageDto message);
    Task RouteDefaultAsync(MessageDto message);
}
