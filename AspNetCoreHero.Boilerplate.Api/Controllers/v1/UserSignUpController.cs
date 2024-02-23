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

    public class UserSignUpController : BaseApiController<UserSignUpController>
    {
        private readonly IIntegrationService _integrationService;
        public UserSignUpController(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<UserSignUpResponse> Post(UserSignUpRequest request)
        {
            UserSignUpResponse response = await _integrationService.PerformSignUp(request);

            return response;
            //return Mediator.Send(request);
        }

    }
}