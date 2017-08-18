namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;


    public class UnauthorizedApiException : ApiException
    {
        public UnauthorizedApiException()
        {
        }

        public UnauthorizedApiException(HttpResponseMessage message) : base(message)
        {
        }
    }

    public class UnauthorizedApiException<TResponse> : UnauthorizedApiException, IApiException<TResponse>
    {
        public UnauthorizedApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
