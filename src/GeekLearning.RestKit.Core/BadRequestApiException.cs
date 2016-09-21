using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class BadRequestApiException: ApiException
    {
    }

    public class BadRequestApiException<TResponse> : ApiException<TResponse>
    {
    }
}
