using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekLearning.RestKit.Core;

namespace GeekLearning.RestKit.Json
{
    public static class RestKitJsonExtensions
    {
        public static IRestKitServicesBuilder AddJson(this IRestKitServicesBuilder restKitServicesBuilder)
        {
            return restKitServicesBuilder.AddMediaFormater<JsonMediaFormatter>();
        }
    }
}
