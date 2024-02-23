using AspNetCoreHero.Boilerplate.Infrastructure.Helpers;

namespace AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;


public class ApiRequestHeadersBuilder
{
    public static ApiRequestHeaders CreateCommonHeadersJson(string ip, Guid channelId)
    {
        ApiRequestHeaders headers = new ApiRequestHeaders();

        //string token = AgentTokenHelper.GenerateToken( ip, channelId);
        //string token = TerminalTokenHelper.DummyToken();

        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");
        //headers.Add("x-agent-token", token);

        return headers;
    }

    public static ApiRequestHeaders CreateAuthHeadersJson(string token)
    {
        ApiRequestHeaders headers = new ApiRequestHeaders();

        //string token = AgentTokenHelper.GenerateToken( ip, channelId);
        //string token = TerminalTokenHelper.DummyToken();

        if(string.IsNullOrEmpty(token))
        {
            token = string.Format("Bearer {0}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiNjBkMWUyZGVjNjFlOGQzZjc4MDgyMzc2IiwiaWQiOiI2MTE3ZWZhYzlkMGRmYTU4NDMwMzZmNzQiLCJpYXQiOjE2OTg3NTA2NTEsImV4cCI6MTY5OTk1MDY1MX0.DXCKJZaw5WiRnO2sCvl14NgTEJW6snXpK07MyfoTp5Q");
        }

        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");
        headers.Add("Authorization", token);

        return headers;
    }

    public static ApiRequestHeaders CreateApiTokenHeadersJson(string token)
    {
        ApiRequestHeaders headers = new ApiRequestHeaders();

        //string token = AgentTokenHelper.GenerateToken( ip, channelId);
        //string token = TerminalTokenHelper.DummyToken();

        if (string.IsNullOrEmpty(token))
        {
            token = string.Format("Bearer {0}", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiNjBkMWUyZGVjNjFlOGQzZjc4MDgyMzc2IiwiaWQiOiI2MTE3ZWZhYzlkMGRmYTU4NDMwMzZmNzQiLCJpYXQiOjE2OTg3NTA2NTEsImV4cCI6MTY5OTk1MDY1MX0.DXCKJZaw5WiRnO2sCvl14NgTEJW6snXpK07MyfoTp5Q");
        }

        headers.Add("Accept", "application/json");
        headers.Add("Content-Type", "application/json");
        headers.Add("api-token", token);

        return headers;
    }


    public static ApiRequestHeaders CreateCommonHeadersJson()
    {

        return CreateCommonHeadersJson(GetLocalIp(), Guid.Empty);
    }
    public static ApiRequestHeaders CreateCommonHeadersJson(Guid channelId)
    {
        return CreateCommonHeadersJson(GetLocalIp(), channelId);
    }

    private static string GetLocalIp()
    {
        string localIp = NetworkHelper.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet);
        if (string.IsNullOrEmpty(localIp))
        {
            localIp = NetworkHelper.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211);
        }
        return localIp;
    }
}
