using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public interface IMediaFormatterProvider
    {
        IMediaFormatter GetMediaFormatter(MediaTypeHeaderValue contentType);
        IMediaFormatter GetMediaFormatter(string mediaType);
    }
}
