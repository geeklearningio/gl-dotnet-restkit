using System;
using System.Collections.Generic;
using System.Net.Http;

namespace GeekLearning.Http
{
    public class RequestCookieHandler: DelegatingHandler
    {
        private readonly string backingProperty ;

        public RequestCookieHandler(HttpMessageHandler innerHandler): base(innerHandler)
        {
            this.backingProperty = "x-cookies";
        }

        public RequestCookieHandler(HttpMessageHandler innerHandler, string backingProperty) : base(innerHandler)
        {
            this.backingProperty = backingProperty;
        }

        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var cookies = request.Properties[this.backingProperty] as IDictionary<string, string>;
            request.PutCookiesOnRequest(cookies);
            var response = await base.SendAsync(request, cancellationToken);
            response.ExtractCookiesFromResponse(cookies);
            return response;
        }
    }
}
