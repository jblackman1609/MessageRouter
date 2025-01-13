using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Router.Contracts.Models;
using Router.Contracts.Services;

namespace Router.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IServiceManager service, ILogger<MessageController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("SendSMS")]
        [Authorize]
        public async Task<IActionResult> SendSmsAsync([FromBody] TenantMessage tenantMessage)
        {
            MessageDto messageDto = new();
            MessageResponse response = new();

            if (!ModelState.IsValid)
            {
                messageDto.Status = MessageStatus.Rejected;
                messageDto.MessageLogs!.Add(MessageLog.REJECTED_LOG);
                await _service.PersistMessageService.AddMessgeAsync(messageDto);
                _logger.LogError($"");

                return BadRequest();
            }

            messageDto.TenantMessage = tenantMessage;
            messageDto.Status = MessageStatus.Accepted;
            messageDto.MessageLogs!.Add(MessageLog.ACCEPTED_LOG);
            await _service.PersistMessageService.AddMessgeAsync(messageDto);

            bool isOTPMessage = new Regex("^[0-9]+$").IsMatch(tenantMessage.Body!);
            
            CountryDataDto countryData = await _service.RouteService.GetCountryDataAsync(tenantMessage.ToPhone!);
            TenantDto tenant = await _service.RouteService.GetTenantByTemplateIdAsync(tenantMessage.TemplateId);
            RecipientDto recipient = await _service.RouteService.GetRecipientAsync(tenantMessage.ToPhone!);
            
            RouteStrategy strategy = DetermineStrategy(isOTPMessage, countryData.IsOTPAllowed, countryData.IsOptInRequired, tenant.Type);

            if (strategy is RouteStrategy.OTP)
            {
                response = await _service.MessageRelayService.SendToRelayAsync(tenantMessage);                
            }

            else if (strategy is RouteStrategy.AssumedOptIn)
            {
                response = await _service.RouteService.RouteAssumedOptinAsync(tenantMessage);                
            }

            else if (strategy is RouteStrategy.Default)
            {
                response = await _service.RouteService.RouteDefaultAsync(tenantMessage);
            }

            else response = new()
            {
                Success = false,
                Status = MessageStatus.Declined,
                MessageLog = $""
            };

            messageDto.Status = response.Status;
            messageDto.MessageLogs.Add(response.MessageLog!);

            await _service.PersistMessageService.UpdateMessageAsync(messageDto, messageDto.GuidId!);

            return Ok();
        }

        private RouteStrategy DetermineStrategy(bool isOTPMessage, bool isOTPAllowed, bool isOptInRequired, TenantType tenantType)
        {
            if (isOTPMessage)
            {                
                if (isOTPAllowed)
                {
                    return RouteStrategy.OTP;
                }

                else
                {
                    return RouteStrategy.None;
                }
            }

            else
            {
                if (!isOptInRequired && tenantType == TenantType.AssumedOptIn)
                {
                    return RouteStrategy.AssumedOptIn;
                }

                else return RouteStrategy.Default;
            } 
        }
    }
}
