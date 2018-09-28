using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core.Internal
{
    public class StreamFormFile : IFile
    {
        public StreamFormFile(Stream stream, string fileName, string mimeType)
        {
            this.FileName = fileName;
            this.Stream = stream;
            this.MimeType = null;
        }

        public object Data => this.Stream;

        public string MimeType { get; }

        public string FileName { get; }

        public Stream Stream { get; }

        public HttpContent CreateContent()
        {
            return new StreamContent(this.Stream);
        }
    }
}
