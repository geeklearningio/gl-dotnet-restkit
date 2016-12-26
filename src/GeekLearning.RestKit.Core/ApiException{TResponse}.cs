namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ApiException<TResponse>: ApiException, IApiException<TResponse>
    {
        public ApiException(HttpResponseMessage response, TResponse data) : base(response)
        {
            this.Response = data;
        }
             
        public TResponse Response { get; }
    }
}
