using System;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;

namespace Router.External.Services;

internal class MessageRelayService : IMessageRelayService
{
    private readonly IHttpClientFactory _factory;

    public MessageRelayService(IHttpClientFactory factory) =>
        _factory = factory;

    public async Task<(MessageStatus, string)> SendToRelayAsync(SmsModel sms)
    {
        HttpClient client = _factory.CreateClient();

        using StringContent content = new(JsonSerializer
            .Serialize(sms, new JsonSerializerOptions(JsonSerializerDefaults.Web)),
            Encoding.UTF8, MediaTypeNames.Application.Json);

        using HttpResponseMessage response = await client
            .PostAsync("",content);
        
        if (response.IsSuccessStatusCode)
        {
            return (MessageStatus.Transmitted, MessageLog.TRANSMITTED);
        }

        else return (MessageStatus.Failed, MessageLog.FAILED);
    }
}
