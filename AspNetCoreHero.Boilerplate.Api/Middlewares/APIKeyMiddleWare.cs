namespace AspNetCoreHero.Boilerplate.Api.Middlewares
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration["ApiKey"]; // Replace with your configuration method
        }

        public async Task Invoke(HttpContext context)
        {
            var providedApiKey = context.Request.Headers["ApiKey"];

            if (!string.IsNullOrEmpty(providedApiKey) && providedApiKey == _apiKey)
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API key");
            }
        }
    }

}
