using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace GeekLearning.Http.Logging
{
    public class RequestLog
    {
        public string ParentActivityId { get; set; }
        public string ActivityId { get; set; }
        public string Method { get; set; }
        public string Uri { get; set; }
        public HttpHeaders RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public HttpHeaders ResponseHeaders { get; set; }
        public string ResponseBody { get; set; }
    }
}
