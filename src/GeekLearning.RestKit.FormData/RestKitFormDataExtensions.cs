using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekLearning.RestKit.Core;

namespace GeekLearning.RestKit.FormData
{
    public static class RestKitFormDataExtensions
    {
        public static IRestKitServicesBuilder AddFormData(this IRestKitServicesBuilder restKitServicesBuilder)
        {
            return restKitServicesBuilder.AddMediaFormater<MultipartFormDataFormatter>();
        }
    }
}
