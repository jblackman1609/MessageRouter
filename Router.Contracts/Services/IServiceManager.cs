using System;
using Router.Contracts.MachineLearning;

namespace Router.Contracts.Services;

public interface IServiceManager
{
    IEmailRelayService EmailRelayService { get; }
    IMessageRelayService MessageRelayService { get; }
    IPersistMessageService PersistMessageService { get; }
    IRouteMessageService RouteService { get; }
    IModelTrainer ModelTrainer { get; }
    IPredictionService PredictionService { get; }
}
