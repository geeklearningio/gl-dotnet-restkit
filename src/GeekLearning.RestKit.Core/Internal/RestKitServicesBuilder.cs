using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GeekLearning.RestKit.Core.Internal
{
    public class RestKitServicesBuilder : IRestKitServicesBuilder
    {
        public RestKitServicesBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; private set; }

        public IRestKitServicesBuilder AddMediaFormater<TMediaFormatter>() where TMediaFormatter : class, IMediaFormatter
        {
            this.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IMediaFormatter, TMediaFormatter>());
            return this;
        }
    }
}
