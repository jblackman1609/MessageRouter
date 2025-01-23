using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Router.Core.Handlers;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain;
using Router.Domain.MessageAggregate;
using Router.Domain.TemplateAggregate;
using Router.Domain.TenantRecipientAggregate;

namespace Router.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageHandler _handler;
        private readonly IRepoService _service;
        private readonly ILogger<MessageController> _logger;
        public HandlerType Handler;
        public new HandlerResponse Response = new();

        public MessageController(
            IMessageHandler handler, IRepoService service, ILogger<MessageController> logger) =>
            (_handler, _service, _logger) = (handler, service, logger);

        [Route("sendSms")]
        public async Task<IActionResult> SendSmsAsync([FromBody] InputMessage inputMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Template template = await _service
                .GetTemplateAsync(inputMessage.TemplateId);
            
            TenantType tenantType = (await _service
                .GetTenantAsync(inputMessage.TemplateId)).Type;

            foreach (string phone in inputMessage.RecipientPhones!)
            {
                Message message = template
                    .CreateMessage(inputMessage.Subject!, inputMessage.TenantPhone!, phone);

                template.AddMessageId(message.Id);

                message.BuildMessageBody(template.Text, inputMessage.Keywords!);

                CountryData countryData = await _service
                    .GetCountryDataAsync(phone);

                DetermineHandler(message.Body, countryData.IsOptInRequired, countryData.IsOTPAllowed, tenantType);

                Response
                    .AddStatus(await _handler.HandleAsync(message, Handler));
            }
            
            return Ok(Response);
        }

        private void DetermineHandler(string body, bool isOptInRequired, bool isOTPAllowed, TenantType type)
        {
            if (body.IsOTPMessage())
            {
                if (isOTPAllowed)
                {
                    Handler = HandlerType.OTP;
                }
                
                else 
                {
                    Response.IsSuccess = false;
                    Response.AddStatus(MessageStatus.Denied);
                }
            }

            else if (!isOptInRequired && type is TenantType.AssumedOptIn)
            {
                Handler = HandlerType.AssumedOptin;
            }

            else
            {
                Handler = HandlerType.Default;
            }          
        }
    }
}
