using System;
using Router.Contracts.Services;

namespace Router.Contracts.Routing;

public interface IRouteService
{
    IPersistService PersistService { get; } 
}
