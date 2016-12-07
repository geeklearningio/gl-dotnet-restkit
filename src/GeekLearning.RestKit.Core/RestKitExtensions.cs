using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public static class RestKitExtensions
    {
        public static IRestKitServicesBuilder AddRestKit(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpClientFactory, DefaultHttpClientFactory>();
            services.TryAddSingleton<IMediaFormatterProvider, Internal.MediaFormatterProvider>();

            return new Internal.RestKitServicesBuilder(services);
        }

        public static IRestKitServicesBuilder AddRestKit<THttpClientFactory>(this IServiceCollection services)
            where THttpClientFactory : class, IHttpClientFactory
        {
            services.TryAddSingleton<IHttpClientFactory, THttpClientFactory>();
            services.TryAddSingleton<IMediaFormatterProvider, Internal.MediaFormatterProvider>();

            return new Internal.RestKitServicesBuilder(services);
        }
    }
}
