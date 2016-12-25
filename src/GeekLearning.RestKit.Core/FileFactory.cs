using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class FileFactory
    {
        IFile Get(byte[] data, string fileName)
        {
            return Get(new MemoryStream(data), fileName);
        }

        IFile Get(Stream stream, string fileName)
        {
            return new Internal.StreamFormFile(stream, fileName);
        }

        IFile Get(FileStream stream)
        {
            return Get(stream, stream.Name);
        }
    }
}
