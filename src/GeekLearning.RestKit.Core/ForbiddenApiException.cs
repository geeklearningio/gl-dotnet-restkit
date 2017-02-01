namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ForbiddenApiException : ApiException
    {
        public ForbiddenApiException()
        {
        }

        public ForbiddenApiException(HttpResponseMessage message) : base(message)
        {
        }
    }

    public class ForbiddenApiException<TResponse> : ForbiddenApiException, IApiException<TResponse>
    {
        public ForbiddenApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
