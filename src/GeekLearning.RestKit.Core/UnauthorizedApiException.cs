using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class UnauthorizedApiException: ApiException
    {
    }

    public class UnauthorizedApiException<TResponse> : ApiException<TResponse>
    {
    }
}
