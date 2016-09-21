using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class ConflictApiException: ApiException
    {
    }

    public class ConflictApiException<TResponse> : ApiException<TResponse>
    {
    }
}
