using System;

namespace Router.Contracts.Routing;

public interface IRouteManager
{
    IRouteServices RouteService { get; }
    IRouteStrategy RouteStrategy { get; }
}
