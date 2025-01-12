using System;
using Router.Contracts.Models;

namespace Router.Contracts.Routing;

public interface IRouteStrategy
{
    Task<MessageResponseModel> DefaultRouteAsync(MessageDto messageDTO);
    Task<MessageResponseModel> AssumedOptInRouteAsync(MessageDto messageDTO);
    Task<MessageResponseModel> OTPRouteAsync(MessageDto messageDTO);
}
