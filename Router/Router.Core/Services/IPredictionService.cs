using System;
using Router.Core.Models;
using Router.Domain.MessageAggregate;

namespace Router.Core.Services;

public interface IPredictionService
{
    Task<bool> PredictAsync(PIIData data);
}
