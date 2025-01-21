using System;
using Router.Core.Models;

namespace Router.Core.Services;

public interface IIdentifyPIIService
{
    Task<PIIResponse> PredictAsync(PIIRequest request);
}
