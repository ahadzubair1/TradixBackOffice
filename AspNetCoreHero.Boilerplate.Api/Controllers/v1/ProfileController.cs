using AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using AspNetCoreHero.Boilerplate.Application.DTOs.Integration;
using AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;


namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class ProfileController : BaseApiController<ProfileController>
    {
        private const string BaseUrl = "YOUR_BASE_URL"; // replace with your actual base URL
        private const string Token = "YOUR_LOGIN_TOKEN"; // replace with your actual token

        [HttpPost]
        [AllowAnonymous]
        public Task<ProfileResponse> Get()
        {
            // Define the API endpoint
            string url = $"{BaseUrl}/profile/api/profile";
            ProfileResponse response = new ProfileResponse();

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
                response = JsonConvert.DeserializeObject<ProfileResponse>(restSharpResponse.Content);
            }
            else
            {
                //throw new ApplicationException("Failed to get response from API");
            }

            return Task.FromResult(response);
        }
    }
}
