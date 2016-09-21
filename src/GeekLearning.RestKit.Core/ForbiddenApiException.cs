using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class ForbiddenApiException: ApiException
    {

    }

    public class ForbiddenApiException<TResponse>: ApiException<TResponse>
    {
    }
}
