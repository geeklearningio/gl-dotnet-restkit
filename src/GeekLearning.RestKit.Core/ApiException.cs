﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public abstract class ApiException: Exception
    {
        public ApiException()
        {
        }

        public ApiException(HttpResponseMessage response)
        {
            this.ResponseMessage = response;
        }

        public HttpResponseMessage ResponseMessage { get; protected set; }
    }
}
