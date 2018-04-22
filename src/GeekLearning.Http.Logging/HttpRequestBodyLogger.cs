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
    public class HttpRequestBodyLogger : DelegatingHandler
    {
        private readonly ILogSink sink;

        public HttpRequestBodyLogger(HttpMessageHandler innerHandler, ILogSink sink) : base(innerHandler)
        {
            this.sink = sink;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestLog = new RequestLog
            {
                RequestHeaders = request.Headers,
                Uri = request.RequestUri.ToString(),
                Method = request.Method.ToString(),
                ActivityId = Activity.Current.Id,
                ParentActivityId = Activity.Current.ParentId,
            };

            if (request.Content != null)
            {
                await request.Content.LoadIntoBufferAsync();
                requestLog.RequestBody = await request.Content.ReadAsStringAsync();
            }
            var response = await base.SendAsync(request, cancellationToken);
            await response.Content.LoadIntoBufferAsync();
            requestLog.ResponseBody = await response.Content.ReadAsStringAsync();

            this.sink.Post(requestLog);
            return response;
        }

    }
}
