using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public interface IMediaFormatter
    {
        Task<TTarget> TransformAsync<TTarget>(HttpContent content);
        HttpContent Format(object data);
        bool Supports(string contentType);
    }
}
