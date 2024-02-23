using AspNetCoreHero.Boilerplate.Application.Features.Events.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.MiningSubscription.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading.Tasks;
using static AspNetCoreHero.Boilerplate.Application.Constants.Permissions;
using VM.WebApi.Domain.App;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class EventsController : BaseApiController<EventsController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityService _identityService;
        public EventsController(UserManager<ApplicationUser> userManager, IIdentityService identityService)
        {
            _userManager = userManager;
            _identityService = identityService;
        }
        // POST api/<controller>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateMiningRaceEventCommand command)
        {


         //  await _mediator.Send(new GetSubscriptionBySubscriptionIdQuery { SubscriptionId = 15 });
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (command == null || command.Email == null || user.Email == null)
            {
                return BadRequest("Invalid User");
            }
            else
            {
                command.UserId = user.Id;
                command.Username = user.UserName;
                command.ReferredBy = user.ReferredBy.ToString();
            }
            return Ok(await _mediator.Send(command));

        }

      //  Rank volume Event(Mining Race => MemberWebApp)
      //  This is the Get function call in our system to show User’s Rank Volume.

    }
}
