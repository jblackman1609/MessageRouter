using System;
using Router.Contracts.MachineLearning;
using Router.Contracts.Models;
using Router.Contracts.Routing;
using Router.Contracts.Services;
using Router.Domain.Models;
using Router.Domain.Repositories;

namespace Router.Core.Routing;

internal class RouteStrategy : IRouteStrategy
{
    private readonly IRouteService _service;
    private readonly IRepository _repository;

    public RouteStrategy(IRouteService service, IRepository repository) 
    {
        _service = service;
        _repository = repository;
    }

    public async Task RouteAssumedOptinAsync(MessageDto message)
    {
        await _repository.MessageRepo.AddMessageAsync()

        RecipientDto recipient = await _service.Persist
            .GetRecipientAsync(message.ToPhone!);

        if (!recipient.IsOptedIn)
        {
            SmsModel sms = ApplyTemplateAsync(new SmsModel(
                message.Subject!,
                message.Body!,
                message.FromPhone,
                message.ToPhone!
            ));
        }
    }

    public Task RouteDefaultAsync(MessageDto message)
    {
        throw new NotImplementedException();
    }

    public async Task<MessageResponse> RouteOTPAsync(MessageDto message)
    {
        return await _service.MessageRelay.SendMessageAsync(new SmsModel(
            message.Body!, 
            message.Subject!,
            message.FromPhone!,
            message.ToPhone!));
    }

    private async Task<SmsModel> ApplyTemplateAsync(SmsModel sms, Template template)
    {
        return await Task.FromResult();
    }
}
