using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace GeekLearning.Http.Logging
{
    public interface ILogSink
    {
        void Post(RequestLog request);
    }
}
