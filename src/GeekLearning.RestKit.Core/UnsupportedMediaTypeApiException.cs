using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class UnsupportedMediaTypeApiException: ApiException
    {
        public UnsupportedMediaTypeApiException()
        {

        }

        public UnsupportedMediaTypeApiException(MediaTypeHeaderValue header)
        {
            this.MediaType = header.MediaType;
        }

        public UnsupportedMediaTypeApiException(string mediaType)
        {
            this.MediaType = mediaType;
        }

        public string MediaType { get; private set; }
    }
}
