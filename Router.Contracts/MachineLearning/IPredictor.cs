using System;
using Router.Contracts.MachineLearning;
using Router.Contracts.Models;

namespace Router.Contracts.MachineLearning;

public interface IPredictor
{
    Task<MLResponse<PIIPrediction>> PredictAsync(MLRequest<PIIDataDto> request);
}
