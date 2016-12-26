namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;


    public class NotFoundApiException : ApiException
    {
        public NotFoundApiException()
        {
        }

        public NotFoundApiException(HttpResponseMessage message) : base(message)
        {
        }
    }

    public class NotFoundApiException<TResponse> : BadRequestApiException, IApiException<TResponse>
    {
        public NotFoundApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
