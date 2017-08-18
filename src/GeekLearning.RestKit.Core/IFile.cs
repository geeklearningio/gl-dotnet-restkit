using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public interface IFile: IFormData
    {
        string FileName { get; }
        HttpContent CreateContent();
    }
}
