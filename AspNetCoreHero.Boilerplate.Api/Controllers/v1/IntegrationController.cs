using AspNetCoreHero.Boilerplate.Application.DTOs.Integration;
using AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;
using AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{

    public class IntegrationController : BaseApiController<IntegrationController>
    {
        [HttpGet]
        [AllowAnonymous]
        public Task<GasPriceResponse> Get()
        {
            GasPriceResponse response = new GasPriceResponse();

            // Replace with your Etherscan API key
            string apiKey = "YourApiKeyToken";

            // Etherscan API endpoint for gas oracle
            string url = $"https://api.etherscan.io/api?module=gastracker&action=gasoracle&apikey={apiKey}";

            var apiHeaders = ApiRequestHeadersBuilder.CreateCommonHeadersJson();

            RestClientConfiguration restClientConfiguration = new RestClientConfiguration();
            restClientConfiguration.Url = url;
            restClientConfiguration.EnableTls = false;
            restClientConfiguration.RequestHeaders = new RestClientRequestHeaders();
            restClientConfiguration.RequestHeaders.AddRange(apiHeaders.Headers);
            RestClientFactory factory = new RestClientFactory(restClientConfiguration);
            var restSharpResponse = factory.GetCall();
            //var request = Mapper.Map(model);
            //PopulateSessionFields(request);
            if (restSharpResponse.IsSuccessful)
            {
                response = JsonConvert.DeserializeObject<GasPriceResponse>(restSharpResponse.Content);
            }
            else
            {
                //throw new ApplicationException("Failed to get response from API");
            }

            return Task.FromResult(response);
            //return Mediator.Send(request);
        }
        [HttpPost("event")]
        [AllowAnonymous]

        public Task<EventResponse> PostEvent(EventRequest request)
        {
            EventResponse response = new EventResponse();

           
            return Task.FromResult(response);
            //return Mediator.Send(request);
        }

    }
}