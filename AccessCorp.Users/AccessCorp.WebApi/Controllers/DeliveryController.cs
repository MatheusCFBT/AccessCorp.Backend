using AccessCorpUsers.Application.Interfaces;
using AccessCorpUsers.WebApi.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AccessCorpUsers.WebApi.Controllers
{

    [ApiVersion("1.0")]
    [ClaimsAuthorize("Permission", "FullAccess"), Route("users/v1/delivery")]
    public class DeliveryController : MainController
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;   
        }

        public async Task<ActionResult> GetAllDeliveries()
        {
            var userId = GetUserId(HttpContext.User);
            

        }
    }
}
