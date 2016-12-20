using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Logging;
using GeekLearning.D64;
using System.Diagnostics;

namespace GeekLearning.Http.Logging
{
    public class HttpRequestLogger : DelegatingHandler
    {
        private ILogger logger;
        private HttpRequestLoggerOptions options;
        TimebasedId timebaseId;

        public HttpRequestLogger(HttpMessageHandler innerHandler, ILogger logger) : this(innerHandler, logger, new HttpRequestLoggerOptions())
        {
        }

        public HttpRequestLogger(HttpMessageHandler innerHandler, ILogger logger, HttpRequestLoggerOptions options) : base(innerHandler)
        {
            this.timebaseId = new TimebasedId(false);
            this.logger = logger;
            this.options = options;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = timebaseId.NewId();

            if (request.Content != null)
            {
                await request.Content.LoadIntoBufferAsync();
                var requestBody = await request.Content.ReadAsStringAsync();
                this.logger.LogInformation(
                    "`{0}` to `{1}` with correlationId `{2}`:\n{3}\n\n{4}",
                    request.Method,
                    request.RequestUri,
                    correlationId,
                    string.Join("\n", request.Headers.Select(h => $"{h.Key}: {string.Join(" ", h.Value)}")),
                    requestBody);
            }
            else
            {
                this.logger.LogInformation(
                   "`{0}` to `{1}` with correlationId `{2}`:\n{3}",
                   request.Method,
                   request.RequestUri,
                   correlationId,
                   string.Join("\n", request.Headers.Select(h => $"{h.Key}: {string.Join(" ", h.Value)}")));
            }
            Stopwatch stopwatch = null;
            if (this.options.MeasureRequestTime)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            var response = await base.SendAsync(request, cancellationToken);
            await response.Content.LoadIntoBufferAsync();
            var responseBody = await response.Content.ReadAsStringAsync();

            if (stopwatch != null)
            {
                stopwatch.Stop();
                this.logger.LogInformation(
                    "`REQUEST` `{0}` ran for `{1}` ms",
                    correlationId,
                    stopwatch.Elapsed.TotalMilliseconds.ToString(System.Globalization.CultureInfo.InvariantCulture)
                );
            }

            this.logger.LogInformation(
               "`RECEIVE` `{0}` `{1}` with correlationId `{2}`:\n{3}\n\n{4}",
               (int)response.StatusCode,
               response.ReasonPhrase,
               correlationId,
               string.Join("\n", response.Headers.Select(h => $"{h.Key}: {string.Join(" ", h.Value)}")),
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
