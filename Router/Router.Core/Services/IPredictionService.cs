using System;
using Router.Core.Models;
using Router.Domain.MessageAggregate;

namespace Router.Core.Services;

public interface IIdentifyPIIService
{
    Task<(MessageStatus, string)> PredictAsync();
}
