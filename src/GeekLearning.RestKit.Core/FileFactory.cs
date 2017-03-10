using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class FileFactory
    {
        public IFile Get(byte[] data, string fileName)
        {
            return Get(new MemoryStream(data), fileName);
        }

        public IFile Get(Stream stream, string fileName)
        {
            return new Internal.StreamFormFile(stream, fileName);
        }

        public IFile Get(FileStream stream)
        {
            return Get(stream, stream.Name);
        }
    }
}
