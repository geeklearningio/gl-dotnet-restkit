namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class BadRequestApiException: ApiException
    {
        public BadRequestApiException()
        {
        }

        public BadRequestApiException(HttpResponseMessage message): base(message)
        {
        }
    }

    public class BadRequestApiException<TResponse> : BadRequestApiException, IApiException<TResponse>
    {
        public BadRequestApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
