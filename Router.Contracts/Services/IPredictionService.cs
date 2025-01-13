using System;
using Router.Contracts.MachineLearning;
using Router.Contracts.Models;

namespace Router.Contracts.MachineLearning;

public interface IPredictionService
{
    Task<PIIPrediction> PredictAsync(PIIDataDto request);
}
