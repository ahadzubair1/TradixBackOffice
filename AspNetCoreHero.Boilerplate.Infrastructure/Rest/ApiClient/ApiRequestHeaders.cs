namespace AspNetCoreHero.Boilerplate.Infrastructure.ApiClient;

public class ApiRequestHeaders
{
    private Dictionary<string, string> headers;
    public Dictionary<string, string> Headers => headers;

    public ApiRequestHeaders()
    {
        headers = new Dictionary<string, string>();
    }

    public string GetUniqueReferenceCode()
    {
        headers.TryGetValue("unique-reference-code", out string urc);
        return urc;
    }
    public bool Add(string key, string value)
    {
        bool result = false;
        try
        {
            if (!headers.ContainsKey(key))
            {
                headers.Add(key, value);
            }
            else
            {
                headers[key] = value;
            }
            result = true;
        }
        catch (Exception)
        {
            result = false;
        }
        return result;

    }
}
