using System;
using Router.Contracts.MachineLearning;
using Router.Contracts.Services;

namespace Router.Contracts.Routing;

public interface IRouteService
{
    IPredictionService Prediction { get; } 
    IEmailRelayService EmailRelay { get; }
    IMessageRelayService MessageRelay { get; }
    IPersistService Persist { get; }
}
