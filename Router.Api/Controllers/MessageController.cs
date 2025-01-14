using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Router.Contracts.Models;
using Router.Contracts.Routing;
using Router.Contracts.Services;

namespace Router.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IRouteService _service;
        private readonly IRouteStrategy _router;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IRouteService service, IRouteStrategy router, ILogger<MessageController> logger)
        {
            _service = service;
            _router = router;
            _logger = logger;
        }

        [HttpPost("SendSMS")]
        [Authorize]
        public async Task<IActionResult> SendSmsAsync([FromBody] TenantMessage tenantMessage)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"{MessageLog.REJECTED_LOG}");
                return BadRequest();
            }

            TenantDto tenant = await _service.PersistService.GetTenantByTemplateIdAsync(tenantMessage.TemplateId);
            
            foreach(string phone in tenantMessage.ToPhones!)
            {
                MessageDto messageDto = new()
                {
                    Body = tenantMessage.Body,
                    Subject = tenantMessage.Subject,
                    ToPhone = phone,
                    TemplateId = tenantMessage.TemplateId
                };

                CountryDataDto countryData = await _service.PersistService.GetCountryDataAsync(phone);
                
                try
                {
                    await ProcessMessageAsync(messageDto, countryData, tenant);
                }

                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}");
                }
                 
            }
            
            return Ok();
        }

        private async Task ProcessMessageAsync(MessageDto message, CountryDataDto countryData, TenantDto tenant)
        {
            bool isOTPMessage = message.IsOTPMessage();
            bool isOTPAllowed = countryData.IsOTPAllowed;
            bool isOptInRequired = countryData.IsOptInRequired;

            RouteStrategy strategy = DetermineStrategy(isOTPMessage, isOTPAllowed, isOptInRequired, tenant.Type);
            if (strategy is RouteStrategy.None)
            {
                await Task.FromResult(new MessageResponse
                {
                    Success = false,
                    Status = MessageStatus.Declined,
                    MessageLog = MessageLog.DECLINED_LOG
                });
            }

            else if (strategy is RouteStrategy.OTP)
            {
                await _router.RouteOTPAsync(message);
            }

            else if (strategy is RouteStrategy.AssumedOptIn)
            {
                await _router.RouteAssumedOptinAsync(message);
            }

            else await _router.RouteDefaultAsync(message);
        }

        private RouteStrategy DetermineStrategy(bool isOTPMessage, bool isOTPAllowed, bool isOptInRequired, TenantType tenantType)
        {
            if (isOTPMessage)
            { 
                return isOTPAllowed ? RouteStrategy.OTP : RouteStrategy.None; 
            }

            else
            {
                return !isOptInRequired && tenantType == TenantType.AssumedOptIn ?
                    RouteStrategy.AssumedOptIn : RouteStrategy.Default;
            } 
        }
    }
}
