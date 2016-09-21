using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public interface IRestKitServicesBuilder
    {
        IServiceCollection Services { get; }

        IRestKitServicesBuilder AddMediaFormater<TMediaFormatter>() where TMediaFormatter : class, IMediaFormatter;
    }
}
