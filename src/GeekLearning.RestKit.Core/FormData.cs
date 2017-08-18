using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class FormData: IFormData
    {
        public FormData(object data)
        {
            Data = data;
        }

        public object Data { get; set; }
    }
}
