using System.Security.Cryptography.X509Certificates;

namespace AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;
public class RestClientConfiguration
{
    public RestClientConfiguration()
    {

    }
    public RestClientConfiguration(
        string url,
        bool enableTls = false,
        RestClientRequestHeaders? requestHeaders = null,
        RestClientCertificateConfiguration? certificateConfiguration = null)
    {
        Url = url;
        EnableTls = enableTls;
        RequestHeaders = requestHeaders;
        CertificateConfiguration = certificateConfiguration;
    }

    public string Url { get; set; }
    public bool EnableTls { get; set; }
    public RestClientRequestHeaders? RequestHeaders { get; set; }
    public RestClientCertificateConfiguration? CertificateConfiguration { get; set; }
}

public class RestClientCertificateConfiguration
{
    public bool BypassSslErrorGlobally { get; set; }
    public bool BypassRestClientSslError { get; set; }
    public StoreName StoreName { get; set; }
    public StoreLocation StoreLocation { get; set; }
    public string? FindValue { get; set; }
    public X509FindType FindType { get; set; }

}
