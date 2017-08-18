using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Http.Logging
{
    public class HttpRequestLoggerOptions
    {
        public HttpRequestLoggerOptions() : this(measureRequestTime: true)
        {

        }

        public HttpRequestLoggerOptions(bool measureRequestTime)
        {
            this.MeasureRequestTime = measureRequestTime;
        }

        public bool MeasureRequestTime { get; set; }

        public int MaxSize { get; set; } = 512000;
    }
}
