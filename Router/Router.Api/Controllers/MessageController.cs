using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Router.Core.Handlers;
using Router.Core.Handlers.Implementations;
using Router.Core.Models;
using Router.Core.Senders;
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
        //private readonly IMessageHandler _handler;
        private readonly IMessageSender _sender;
        private readonly IMessageService _service;
        private readonly ILogger<MessageController> _logger;
        public HandlerType Handler;
        public new HandlerResponse Response = new();

        public MessageController(
            IMessageSender sender, IMessageService service, ILogger<MessageController> logger) =>
            (_sender, _service, _logger) = (sender, service, logger);

        [Route("sendSms")]
        public async Task<IActionResult> SendSmsAsync([FromBody] InputMessage inputMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Template template = await _service
                .GetTemplateAsync(inputMessage.TemplateId);
            
            TenantType tenantType = await _service.GetTenantTypeAsync(template.TenantId);

            foreach (string phone in inputMessage.RecipientPhones!)
            {
                Message message = template
                    .CreateMessage(inputMessage.Subject!, inputMessage.TenantPhone!, phone);

                template.AddMessageId(message.Id);

                message.BuildMessageBody(template.Text, inputMessage.Keywords!);

                (bool isOptInRequired, bool isOTPAllowed) = await _service
                    .GetCountryDataAsync(message.RecipientPhone);

                DetermineStrategy(message.Body, isOptInRequired, isOTPAllowed, tenantType);

                switch (Handler)
                {
                    case: HandlerType.OTP
                        _sender.Handler = new OTPMessageHandler()
                }
            }
            
            
            return Ok();
        }

        private void DetermineStrategy(string body, bool isOptInRequired, bool isOTPAllowed, TenantType type)
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
                    Response.Status = MessageStatus.Denied;
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
