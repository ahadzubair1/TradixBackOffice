using AspNetCoreHero.Boilerplate.Application.DTOs.Integration;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetById;
using AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;
using AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VM.WebApi.Application.App.UserMemberships;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class SubscriptionController : BaseApiController<SubscriptionController>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscriptionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public class JwtTokenResponse
        {
            public string Token { get; set; }
        }

        [ApiExplorerSettings(IgnoreApi = true)] // Add this attribute to exclude from Swagger
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

        [HttpGet]
        [AllowAnonymous]
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery() { Id = id });
            return Ok(product);
        }

        // POST api/<controller>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            PurchaseUserMembershipRequest request = new PurchaseUserMembershipRequest();
            request.UserId = Guid.Parse("8df30000-1b58-04bf-bd77-08dc136efe81");
            request.MembershipId = Guid.Parse("88A62F9B-92EC-435F-62BA-08DC136EFEE5");
            request.WalletId = Guid.NewGuid();
            return Ok(await _mediator.Send(request));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand { Id = id }));
        }


    }

}