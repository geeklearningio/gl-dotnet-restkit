using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Logging;
using GeekLearning.D64;

namespace GeekLearning.Http.Logging
{
    public class HttpRequestLogger : DelegatingHandler
    {
        private ILogger logger;
        TimebasedId timebaseId;

        public HttpRequestLogger(HttpMessageHandler innerHandler, ILogger logger) : base(innerHandler)
        {
            this.timebaseId = new TimebasedId(false);
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = timebaseId.NewId();

            if (request.Content != null)
            {
                await request.Content.LoadIntoBufferAsync();
                var requestBody = request.Content.ReadAsStringAsync();
                this.logger.LogInformation(
                    "`{0}` to `{1}` with correlationId `{2}`:\n{3}\n\n{4}",
                    request.Method,
                    request.RequestUri,
                    correlationId,
                    string.Join("\n", request.Headers.Select(h => $"{h.Key}: {h.Value}")),
                    requestBody);
            }
            else
            {
                this.logger.LogInformation(
                   "`{0}` to `{1}` with correlationId `{2}`:\n{3}",
                   request.Method,
                   request.RequestUri,
                   correlationId,
                   string.Join("\n", request.Headers.Select(h => $"{h.Key}: {h.Value}")));
            }

            var response = await base.SendAsync(request, cancellationToken);
            await response.Content.LoadIntoBufferAsync();
            var responseBody = response.Content.ReadAsStringAsync();

            this.logger.LogInformation(
               "`RECEIVE` `{0}` `{1}` with correlationId `{2}`:\n{3}\n\n{4}",
               response.StatusCode,
               response.ReasonPhrase,
               correlationId,
               string.Join("\n", response.Headers.Select(h => $"{h.Key}: {h.Value}")),
               responseBody);

            return response;
        }

    }

    public class HttpRequestLogger<TInnerHandler> : HttpRequestLogger
        where TInnerHandler : HttpMessageHandler, new()
    {
        public HttpRequestLogger(ILogger logger) : base(new TInnerHandler(), logger)
        {
        }
    }
}
