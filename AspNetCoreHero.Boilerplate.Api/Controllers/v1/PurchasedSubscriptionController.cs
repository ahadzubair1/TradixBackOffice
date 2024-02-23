using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using VM.WebApi.Application.App.UserMemberships;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class PurchasedSubscriptionController : BaseApiController<PurchasedSubscriptionController>
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post()
        {
              PurchaseUserSubscriptionRequest request = new PurchaseUserSubscriptionRequest();
              request.UserId = Guid.Parse("bbcdca25-450a-49c1-a2eb-9246f7a3ccd4");
              request.SubscriptionId = Guid.Parse("36417B35-2560-4FF1-3E61-08DC20ACF668");
               request.SubscriptionId = Guid.Parse("36417B35-2560-4FF1-3E61-08DC20ACF668");
            //  request.WalletId = Guid.NewGuid();
            return Ok(await _mediator.Send(request));

        }
    }
}
