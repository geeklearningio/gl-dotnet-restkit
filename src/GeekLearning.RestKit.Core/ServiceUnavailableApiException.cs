namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ServiceUnavailableApiException : ApiException
    {
        public ServiceUnavailableApiException()
        {
        }

        public ServiceUnavailableApiException(HttpResponseMessage message): base(message)
        {
        }
    }

    public class ServiceUnavailableApiException<TResponse> : BadRequestApiException, IApiException<TResponse>
    {
        public ServiceUnavailableApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
