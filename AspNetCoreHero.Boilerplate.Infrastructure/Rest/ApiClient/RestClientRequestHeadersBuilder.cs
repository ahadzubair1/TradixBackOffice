

using AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;

namespace AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;

public class RestClientRequestHeadersBuilder
{
    public static RestClientRequestHeaders CreateCommonHeadersJson()
    {
        RestClientRequestHeaders headers = new RestClientRequestHeaders();
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");
        return headers;
    }

    public static RestClientRequestHeaders CreateCommonHeadersJson(string ip, string? token = null)
    {
        RestClientRequestHeaders headers = new RestClientRequestHeaders();
        DateTime now = DateTime.UtcNow;
        headers.Add("ip", ip);
        headers.Add("machineName", Environment.MachineName);
        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");
        if (!string.IsNullOrEmpty(token))
            headers.Add("token", token);
        return headers;
    }

    public static RestClientRequestHeaders CreateCommonHeadersPlainText(string clientId, string? token = null)
    {
        RestClientRequestHeaders headers = new RestClientRequestHeaders();
        headers.Add("Content-Type", "application/x-www-form-urlencoded");
        if (token != null)
            headers.Add("Authorization", string.Format("Bearer {0}", token));
        headers.Add("ClientId", clientId);
        return headers;
    }

    public static RestClientRequestHeaders CreateOAuthHeaders()
    {
        RestClientRequestHeaders headers = new RestClientRequestHeaders();
        headers.Add("Content-Type", "application/x-www-form-urlencoded");
        return headers;
    }

}
