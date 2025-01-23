using System;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;

namespace Router.External.Services;

internal class MessageService : IMessageService
{
    private readonly IPredictionService _prediction;
    private readonly IMessageRelayService _relay;
    private readonly IEmailService _email;

    public MessageService(
        IPredictionService prediction, IMessageRelayService relay, IEmailService email) =>
        (_prediction, _relay, _email) = (prediction, relay, email);

    public async Task<bool> PredictAsync(PIIData data) =>
        await _prediction.PredictAsync(data);

    public async Task SendEmailAsync(EmailModel emailModel) =>
        await _email.SendEmailAsync(emailModel);

    public async Task<(MessageStatus, string)> SendToRelayAsync(SmsModel sms) =>
        await _relay.SendToRelayAsync(sms);
}
