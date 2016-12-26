namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class InternalServerErrorApiException: ApiException
    {
        public InternalServerErrorApiException()
        {
        }

        public InternalServerErrorApiException(HttpResponseMessage message): base(message)
        {
        }
    }

    public class InternalServerErrorApiException<TResponse> : BadRequestApiException, IApiException<TResponse>
    {
        public InternalServerErrorApiException(HttpResponseMessage message, TResponse response) : base(message)
        {
            this.Response = response;
        }

        public TResponse Response { get; }
    }
}
