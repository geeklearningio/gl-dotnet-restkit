using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public abstract class ApiException<TResponse>: ApiException
    {
        public ApiException()
        {
        }

    }
}
