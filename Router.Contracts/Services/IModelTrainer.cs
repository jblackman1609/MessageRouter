using System;
using Router.Contracts.Models;

namespace Router.Contracts.MachineLearning;

public interface IModelTrainer
{
    Task FitAsync();
    Task<MLResponse<BinaryMetricDto>> EvaluateAsync();
    Task SaveModelAsync(); 
}
