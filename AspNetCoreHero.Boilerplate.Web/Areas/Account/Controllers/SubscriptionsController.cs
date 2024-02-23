using AspNetCoreHero.Boilerplate.Application.DTOs.Integration;
using AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Controllers
{
    public class SubscriptionViewModel
    {

    }

    // Assuming UserPurchasedSubscriptions has properties like Subscriptions, UserId, etc.
    public class UserSubscriptionViewModel
    {
        public UserPurchasedSubscriptions Subscriptions { get; set; }
    }


    [Area("Account")]
    public class SubscriptionsController : BaseController<SubscriptionsController>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscriptionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> IndexAsync()
        {
            _notify.Information("Hi There!");
               var response = await Get(); // Assuming Get() is an action method in the same controller

            return View(response);
        }

        public class JwtTokenResponse
        {
            public string Token { get; set; }
        }
        public async Task<string> GetJwtToken()
        {
            // Use the HttpClientFactory to create and manage HttpClient instances
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                // Replace with your actual endpoint for obtaining the JWT token
                string tokenUrl = "https://demo.miningrace.com/gateway/auth/api/partner/login";

                // Replace with your actual partner authentication header
                string partnerAuthHeader = "Basic b4eed0047100e7eeb7cd";

                // Replace with your actual credentials
                var credentials = new
                {
                    email = "saad.volxom@gmail.com",
                    password = "20a7e83927cb7a2bd9b48df5d3d52b4612f9d7b426c5064fbe2d696dbe4ea3f4c7d66e0516ea4132f8db98dc193c5d71ff1c1bba005624ee4ceee643da30cc1e"
                };

                // Convert credentials to JSON string
                string jsonCredentials = JsonConvert.SerializeObject(credentials);

                // Create request content
                var content = new StringContent(content: jsonCredentials, Encoding.UTF8, "application/json");

                // Add partner authentication header
                httpClient.DefaultRequestHeaders.Add("x-partner-auth", partnerAuthHeader);

                // Make the API call to obtain the JWT token
                var response = await httpClient.PostAsync(tokenUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response to get the JWT token
                    var tokenResponse = JsonConvert.DeserializeObject<JwtTokenResponse>(await response.Content.ReadAsStringAsync());

                    return tokenResponse.Token;
                }
                else
                {
                    // Handle error
                    Console.WriteLine($"Error obtaining JWT token. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }

        public Task<List<SubscriptionTypes>> Get()
        {
            ApiResponse response = new ApiResponse();

            // Replace with your Etherscan API key
            string url = "https://demo.miningrace.com/gateway/agents/api/mlm-bo/subscription-types";

            // Add your JWT token to the Authorization header
            string jwtToken = GetJwtToken().Result;

            RestClientConfiguration restClientConfiguration = new RestClientConfiguration();
            restClientConfiguration.Url = url;
            restClientConfiguration.EnableTls = false;

            // Create headers and add Authorization header
            restClientConfiguration.RequestHeaders = new RestClientRequestHeaders();
            restClientConfiguration.RequestHeaders.Add("Authorization", "Bearer " + jwtToken);

            RestClientFactory factory = new RestClientFactory(restClientConfiguration);

            var restSharpResponse = factory.GetCall();

            if (restSharpResponse.IsSuccessful)
            {
                response = JsonConvert.DeserializeObject<ApiResponse>(restSharpResponse.Content);
            }
            else
            {
                // Handle error
                Console.WriteLine($"Error: {restSharpResponse.ErrorMessage}");
            }

            return Task.FromResult(response.Payload);
        }

    }
}