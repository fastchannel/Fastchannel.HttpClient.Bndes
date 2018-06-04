using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vertis.BndesClient
{
    public class RestClient : IDisposable
    {
        #region Constructor

        public RestClient(string baseEndpoint, string clientCertificateThumbprint, int? defaultTimeoutInSeconds)
        {
            if (string.IsNullOrWhiteSpace(baseEndpoint))
                throw new ArgumentNullException(nameof(baseEndpoint), "baseEndpoint is null or invalid.");

            if (string.IsNullOrWhiteSpace(clientCertificateThumbprint))
                throw new ArgumentNullException(nameof(clientCertificateThumbprint), "clientCertificateThumbprint is null or invalid.");

            _baseEndpoint = baseEndpoint;
            _clientCertificateThumbprint = clientCertificateThumbprint;
            _defaultTimeoutInSeconds = defaultTimeoutInSeconds;
        }

        #endregion

        #region Fields
        private const string DisableCachingName = @"TestSwitch.LocalAppContext.DisableCaching";
        private const string DontEnableSchUseStrongCryptoName = @"Switch.System.Net.DontEnableSchUseStrongCrypto";
        private HttpClient _httpClient;

        public string AcceptsContentType { get; set; } = "application/json";

        private HttpClient HttpClient => EnsureHttpClientCreated();
        private readonly string _baseEndpoint;
        private readonly string _clientCertificateThumbprint;
        private readonly int? _defaultTimeoutInSeconds;

        #endregion

        #region Methods

        #region HttpClient Context Methods

        public static void EnableAppContext()
        {
            AppContext.SetSwitch(DisableCachingName, true);
            AppContext.SetSwitch(DontEnableSchUseStrongCryptoName, true);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
        }

        private HttpClient EnsureHttpClientCreated()
        {
            if (_httpClient != null) return _httpClient;

            EnableAppContext();

            var httpHandler = new HttpClientHandler{UseCookies = false};
            httpHandler.ClientCertificates.Add(GetClientCertificate(_clientCertificateThumbprint));

            _httpClient = new HttpClient(httpHandler)
            {
                BaseAddress = new Uri(_baseEndpoint)
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AcceptsContentType));

            return _httpClient;
        }

        private static X509Certificate2 GetClientCertificate(string subjectName)
        {
            return GetCurrentUserCertificate(StoreName.My, subjectName);
        }

        private static X509Certificate2 GetCurrentUserCertificate(StoreName certStore, string subjectName)
        {
            var userCaStore = new X509Store(certStore, StoreLocation.CurrentUser);
            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                var certificatesInStore = userCaStore.Certificates;
                var findResult = certificatesInStore.Find(X509FindType.FindByThumbprint, subjectName, false);
                X509Certificate2 clientCertificate;
                if (findResult.Count == 1)
                {
                    clientCertificate = findResult[0];
                }
                else
                {
                    throw new Exception("Unable to locate the correct client certificate.");
                }
                return clientCertificate;
            }
            finally
            {
                userCaStore.Close();
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }


        #endregion

        public async Task<HttpResponseMessage> Execute(Request request)
        {
            return await HttpClient.SendAsync(CreateRequestMessage(request), CreateRequestTimeout(request)).ConfigureAwait(false);
        }

        private HttpRequestMessage CreateRequestMessage(Request request)
        {
            var message = new HttpRequestMessage(request.Method, CreateRequestUri(request));

            foreach (var c in request.Parameters.Where(cookies => cookies.Type == ParameterType.Cookie))
                message.Headers.TryAddWithoutValidation(c.Name, Convert.ToString(c.Value));

            message.Content = CreateMessageContent(request);

            return message;
        }

        private StringContent CreateMessageContent(Request request)
        {
            var body = request.Parameters.Find(b => b.Type == ParameterType.RequestBody);
            return body != null ? new StringContent(Convert.ToString(body.Value), Encoding.UTF8, AcceptsContentType) : null;
        }

        private string CreateRequestUri(Request request)
        {
            var url = _baseEndpoint + request.Resource;
            var result = request.Parameters == null ? url : request.Parameters.Where(parameter => parameter.Type == ParameterType.UrlSegment).Aggregate(url, (current, p) => string.Format(current, p.Value));
            result = request.Parameters == null ? result : request.Parameters.Where(parameter => parameter.Type == ParameterType.QueryString).Aggregate(result + "?", (current, p) => string.Format(current + "{0}&", p));
            if (result.EndsWith("&") || result.EndsWith("?"))
                result = result.Remove(result.Length - 1, 1);

            return result;
        }

        private CancellationToken CreateRequestTimeout(Request request)
        {
            if (!_defaultTimeoutInSeconds.HasValue && request?.TimeoutInSeconds == null)
                return CancellationToken.None;

            var defaultTimeout = _defaultTimeoutInSeconds ?? default(int);
            var requestTimeout = request?.TimeoutInSeconds ?? default(int);
            var lowestTimeoutInSeconds = Math.Min(defaultTimeout, requestTimeout);
            return lowestTimeoutInSeconds > default(int)
                ? new CancellationTokenSource(TimeSpan.FromSeconds(lowestTimeoutInSeconds)).Token
                : CancellationToken.None;
        }

        #endregion
    }
}