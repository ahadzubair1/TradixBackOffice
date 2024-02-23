using AspNetCoreHero.Boilerplate.Application.DTOs.Identity;
using AspNetCoreHero.Boilerplate.Application.DTOs.Mail;
using AspNetCoreHero.Boilerplate.Application.DTOs.Settings;
using AspNetCoreHero.Boilerplate.Application.Enums;
using AspNetCoreHero.Boilerplate.Application.Exceptions;
using AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;
using AspNetCoreHero.Boilerplate.Infrastructure.Services;
using AspNetCoreHero.Results;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NanoidDotNet;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VM.WebApi.Domain.App;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Services
{

    public class IntegrationService : IIntegrationService
    {

        private readonly ILogger<IntegrationService> _logger;
        //private readonly IIdentityService _identityService;
        public IntegrationService(ILogger<IntegrationService> logger)
        {
            _logger = logger;
            //_identityService = identityService;
        }

        public Task<UserSignInResponse> PerformSignIn(UserSignInRequest request)
        {
            UserSignInResponse response = new UserSignInResponse();

            // Etherscan API endpoint for gas oracle
            string url = $"https://demo.miningrace.com/gateway/auth/api/partner/login";

            var apiHeaders = ApiRequestHeadersBuilder.CreateCommonHeadersJson();

            RestClientConfiguration restClientConfiguration = new RestClientConfiguration();
            restClientConfiguration.Url = url;
            restClientConfiguration.EnableTls = false;
            restClientConfiguration.RequestHeaders = new RestClientRequestHeaders();
            restClientConfiguration.RequestHeaders.AddRange(apiHeaders.Headers);
            restClientConfiguration.RequestHeaders.Add("x-partner-auth", "Basic b4eed0047100e7eeb7cd");
            RestClientFactory factory = new RestClientFactory(restClientConfiguration);
            var restSharpResponse = factory.PostCall(request, null, true);
            //var request = Mapper.Map(model);
            //PopulateSessionFields(request);
            if (restSharpResponse.IsSuccessful)
            {
                response = JsonConvert.DeserializeObject<UserSignInResponse>(restSharpResponse.Content);
            }
            else
            {
                //throw new ApplicationException("Failed to get response from API");
            }

            return Task.FromResult(response);
            //return Mediator.Send(request);
        }

        public Task<UserSignUpResponse> PerformSignUp(UserSignUpRequest request)
        {
            UserSignUpResponse response = new UserSignUpResponse();

            // Etherscan API endpoint for gas oracle
            string url = $"https://miningrace.com/gateway/auth/api/partner/signup";

            var apiHeaders = ApiRequestHeadersBuilder.CreateCommonHeadersJson();

            RestClientConfiguration restClientConfiguration = new RestClientConfiguration();
            restClientConfiguration.Url = url;
            restClientConfiguration.EnableTls = false;
            restClientConfiguration.RequestHeaders = new RestClientRequestHeaders();
            restClientConfiguration.RequestHeaders.AddRange(apiHeaders.Headers);
            restClientConfiguration.RequestHeaders.Add("x-partner-auth", "Basic 083b421d7b4e03799008");
            RestClientFactory factory = new RestClientFactory(restClientConfiguration);
            var restSharpResponse = factory.PostCall(request, null, true);
            //var request = Mapper.Map(model);
            //PopulateSessionFields(request);
            if (restSharpResponse.IsSuccessful)
            {
                response = JsonConvert.DeserializeObject<UserSignUpResponse>(restSharpResponse.Content);
            }
            else
            {
                //throw new ApplicationException("Failed to get response from API");
            }

            return Task.FromResult(response);
            //return Mediator.Send(request);
        }
    }
}