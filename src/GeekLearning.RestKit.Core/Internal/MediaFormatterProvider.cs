using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core.Internal
{
    public class MediaFormatterProvider: IMediaFormatterProvider
    {
        private IEnumerable<IMediaFormatter> mediaFormatters;

        public MediaFormatterProvider(IEnumerable<IMediaFormatter> mediaFormatters)
        {
            this.mediaFormatters = mediaFormatters;
        }

        public IMediaFormatter GetMediaFormatter(MediaTypeHeaderValue contentType)
        {
            return GetMediaFormatter(contentType.MediaType);
        }

        public IMediaFormatter GetMediaFormatter(string contentType)
        {
            return mediaFormatters.First(f=> f.Supports(contentType));
        }
    }
}
