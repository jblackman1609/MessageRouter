using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Router.Core.Handlers;
using Router.Core.Handlers.Implementations;
using Router.Core.Models;
using Router.Core.Senders;
using Router.Core.Services;
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
        private readonly IMessagePersistenceService _service;
        private readonly ILogger<MessageController> _logger;
        public HandlerType Handler;
        public new MessageResponse Response = new();

        public MessageController(
            IMessageHandler handler, IMessagePersistenceService service, ILogger<MessageController> logger) =>
            (_handler, _service, _logger) = (handler, service, logger);

        [Route("sendSms")]
        public async Task<IActionResult> SendSmsAsync([FromBody] InputMessage inputMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Template template = 
                await _service.Repository.TemplateRepo.GetTemplateAsync(inputMessage.TemplateId);

            foreach (string phone in inputMessage.RecipientPhones!)
            {
                Message message = template
                    .CreateMessage(inputMessage.Subject!, inputMessage.TenantPhone!, phone);

                template.AddMessageId(message.Id);

                message.BuildMessageBody(template.Text, inputMessage.Keywords!);

                CountryData countryData = 
                    await _service.Repository.TenantRecipientRepo.GetCountryDataAsync(message.RecipientPhone);

                //await DetermineStrategy(message, countryData.IsOptInRequired, countryData.IsOTPAllowed);
            }
            
            
            return Ok();
        }

        private async Task DetermineStrategy(Message message, bool isOptInRequired, bool isOTPAllowed, TenantType)
        {
            if (message.Body!.IsOTPMessage())
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

            else if (!isOptInRequired && await _router.RouteService.IsAssumedOptInTenantAsync(messageDTO.TenantMessage!.TenantId))
            {
                _response = await _router.RouteStrategy.AssumedOptInRouteAsync(messageDTO);
            }

            else
            {
                _response = await _router.RouteStrategy.DefaultRouteAsync(messageDTO);
            }          
        }
    }
}
