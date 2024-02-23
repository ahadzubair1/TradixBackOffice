using AspNetCoreHero.Boilerplate.Application.DTOs.Integration;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;
using AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{

    public class UserSignInController : BaseApiController<UserSignInController>
    {
        private readonly IIntegrationService _integrationService;
        public UserSignInController(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<UserSignInResponse> Post(UserSignInRequest request)
        {
            UserSignInResponse response = await _integrationService.PerformSignIn(request);

            return response;
            //return Mediator.Send(request);
        }

    }
}