namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;


    public class UnhandledApiException : ApiException
    {
        public UnhandledApiException()
        {
        }

        public UnhandledApiException(HttpResponseMessage message) : base(message)
        {
        }
    }

    public class UnhandledApiException<TResponse> : BadRequestApiException, IApiException<TResponse>
    {
        public UnhandledApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
