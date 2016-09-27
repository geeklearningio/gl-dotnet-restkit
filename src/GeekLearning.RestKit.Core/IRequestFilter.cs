using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public interface IRequestFilter
    {
        HttpRequestMessage Apply(HttpRequestMessage requestMessage, string[] securityDefinitions);
    }
}
