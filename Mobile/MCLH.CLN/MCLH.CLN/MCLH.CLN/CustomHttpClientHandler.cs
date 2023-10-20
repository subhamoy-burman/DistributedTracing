using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MCLH.CLN
{
    public class CustomHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Ignore SSL certificate validation errors
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
