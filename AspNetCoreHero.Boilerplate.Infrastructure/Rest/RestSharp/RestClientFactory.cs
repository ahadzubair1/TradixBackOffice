using AspNetCoreHero.Boilerplate.Infrastructure.Helpers;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace AspNetCoreHero.Boilerplate.Infrastructure.RestSharp;

public class RestClientFactory
{
    readonly RestClient? _client;
    public RestClientConfiguration Configuration { get; private set; }
    private readonly ILogger? _logger;
    public RestClientFactory(RestClientConfiguration configuration, ILogger logger = null)
    {
        _logger = logger;

        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        Configuration = configuration;
        RestClientCertificateConfiguration? certificateConfig = configuration.CertificateConfiguration;
        try
        {
            RestClientOptions options = new RestClientOptions(configuration.Url);

            if (configuration.EnableTls)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
                /*
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                }
                catch (Exception)
                {

                }
                */
                if (certificateConfig.BypassSslErrorGlobally)
                {
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) =>
                    {
                        _logger?.LogTrace("\nCertificate:\n{0}\nSslPolicyErros:\n{1}",
                            JsonConvert.SerializeObject(cert, Formatting.None),
                            sslPolicyErrors.ToString());
                        return true;
                    };
                }

                _logger?.LogTrace("Certificate Find Type: [{0}], Find Value: [{1}]", certificateConfig.FindType, certificateConfig.FindValue);

                if (!string.IsNullOrEmpty(certificateConfig.FindValue))
                {
                    _logger?.LogTrace(
                        "Finding Certificate from StoreName: [{0}], StoreLocation: [{1}], FindType: [{2}] and FindValue: [{3}]",
                        certificateConfig.StoreName.ToString(),
                        certificateConfig.StoreLocation.ToString(),
                        certificateConfig.FindType.ToString(),
                        certificateConfig.FindValue);

                    X509Certificate2 certificate = CertificateHelper.GetSystemCertificate(certificateConfig.StoreName, certificateConfig.StoreLocation, certificateConfig.FindType, certificateConfig.FindValue);
                    if (certificate == null)
                    {
                        _logger?.LogTrace("Certificate Not found for [{0}: {1}]", certificateConfig.FindType.ToString(), certificateConfig.FindValue);
                        _logger?.LogTrace("Fetching all certificate from StoreName: [{0}], StoreLocation: [{1}]", certificateConfig.StoreName.ToString(), certificateConfig.StoreLocation.ToString());

                        options.ClientCertificates = CertificateHelper.GetX509Certificates(certificateConfig.StoreName, certificateConfig.StoreLocation);
                    }
                    else
                    {
                        options.ClientCertificates = new X509CertificateCollection() { certificate };
                    }
                }
                else
                {
                    _logger?.LogTrace("Certificate Thumbprint not provided");
                    _logger?.LogTrace("Fetching all certificate from StoreName: [{0}], StoreLocation: [{1}]", certificateConfig.StoreName.ToString(), certificateConfig.StoreLocation.ToString());
                    options.ClientCertificates = CertificateHelper.GetX509Certificates(certificateConfig.StoreName, certificateConfig.StoreLocation);
                }

                if (certificateConfig.BypassRestClientSslError)
                {
                    options.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    {
                        _logger?.LogTrace("SslPolicyErrors: [{0}]", sslPolicyErrors.ToString());
                        return true;
                    };
                }



                options.Proxy = new WebProxy();
            }

            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
            _logger?.LogTrace("Url: {0}, Tls: {1}", configuration.Url, configuration.EnableTls);

        }
        catch (Exception ex)
        {

            _logger?.LogError(ex.ToString());

        }
    }

    private RestResponse? Execute(RestRequest request, bool logResponse = false)
    {
        var configuration = Configuration;
        var response = _client?.Execute(request);
        if (response?.ErrorException != null)
        {
            _logger?.LogError(response.ErrorException.ToString());
        }

        _logger?.LogTrace("Request:\n{0}", JsonConvert.SerializeObject(response?.Request, Formatting.Indented));
        _logger?.LogTrace("Response Uri: {0}", response.ResponseUri);
        _logger?.LogTrace("Response Headers:\n{0}", JsonConvert.SerializeObject(response?.Headers.ToList(), Formatting.Indented));
        _logger?.LogTrace("Response Code:[{0}] - [{1}]", (int)response.StatusCode, response?.StatusDescription);
        return response;
    }
    private RestResponse? TExecute(RestRequest request, bool logResponse = false)
    {
        var configuration = Configuration;

        var response = _client?.Execute(request);

        if (response?.ErrorException != null)
        {
            _logger?.LogError("{0}", response.ErrorException.ToString());
        }

        _logger?.LogTrace("Request:\n{0}", JsonConvert.SerializeObject(response?.Request, Formatting.Indented));
        _logger?.LogTrace("Response Uri: {0}", response?.ResponseUri);
        _logger?.LogTrace("Response Headers:\n{0}", JsonConvert.SerializeObject(response?.Headers?.ToList(), Formatting.Indented));
        _logger?.LogTrace("Response Code:[{0}] - [{1}]", (int)response?.StatusCode, response?.StatusDescription);

        return response;
    }

    public RestResponse GetCall(bool logResponse = true)
    {
        var request = new RestRequest();
        request.Method = Method.Get;
        if (Configuration.RequestHeaders != null)
        {
            var headers = Configuration.RequestHeaders.Headers.ToList();
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }

        return Execute(request, logResponse);
    }

    public RestResponse? OAuthCall(string encodedBody, object? requestObject = null, bool logResponse = false)
    {
        var request = new RestRequest();
        request.Method = Method.Post;

        request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);
        request.AddParameter("Content-Type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);

        if (requestObject != null)
        {
            request.AddJsonBody(requestObject);
        }

        if (Configuration?.RequestHeaders != null)
        {
            var headers = Configuration.RequestHeaders.Headers.ToList();
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }

        return TExecute(request, logResponse);
    }

    public RestResponse? PostCall(object requestObject, Dictionary<string, string> queryParameters = null, bool logResponse = false)
    {
        var request = new RestRequest();
        request.Method = Method.Post;

        if (queryParameters != null)
        {
            foreach (var kv in queryParameters)
            {
                request.AddQueryParameter(kv.Key, kv.Value);
            }
        }

        if (Configuration.RequestHeaders != null)
        {
            var headers = Configuration.RequestHeaders.Headers.ToList();
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }

        request.AddJsonBody(requestObject);
        _logger?.LogTrace("Request:\n{0}", JsonConvert.SerializeObject(requestObject, Formatting.Indented));

        return TExecute(request, logResponse);
    }

    public RestResponse PutCall(object requestObject, Dictionary<string, string> queryParameters = null, bool logResponse = true)

    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        var request = new RestRequest();
        request.Method = Method.Put;

        if (queryParameters != null)
        {
            foreach (var kv in queryParameters)
            {
                request.AddQueryParameter(kv.Key, kv.Value);
            }
        }
        if (Configuration.RequestHeaders != null)
        {
            var headers = Configuration.RequestHeaders.Headers.ToList();
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }


        request.AddJsonBody(requestObject);
        _logger?.LogTrace("Request:\n{0}", JsonConvert.SerializeObject(requestObject, Formatting.Indented));

        return TExecute(request, logResponse);
    }



}