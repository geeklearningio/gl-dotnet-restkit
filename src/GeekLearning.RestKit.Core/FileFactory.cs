using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class FileFactory
    {
        public IFile Get(byte[] data, string fileName, string mimeType = null)
        {
            return Get(new MemoryStream(data), fileName, mimeType);
        }

        public IFile Get(Stream stream, string fileName, string mimeType = null)
        {
            return new Internal.StreamFormFile(stream, fileName, mimeType);
        }

        public IFile Get(FileStream stream, string mimeType = null)
        {
            return Get(stream, stream.Name, mimeType);
        }
    }
}
