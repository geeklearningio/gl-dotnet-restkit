namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ConflictApiException : ApiException
    {
        public ConflictApiException()
        {
        }

        public ConflictApiException(HttpResponseMessage message) : base(message)
        {
        }
    }

    public class ConflictApiException<TResponse> : ConflictApiException, IApiException<TResponse>
    {
        public ConflictApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
