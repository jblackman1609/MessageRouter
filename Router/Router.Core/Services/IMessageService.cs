using System;

namespace Router.Core.Services;

public interface IMessageService : 
    IMessageRelayService, IEmailService, IPredictionService
{

}
