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
            services.AddHttpClient();
            services.TryAddSingleton<IMediaFormatterProvider, Internal.MediaFormatterProvider>();

            return new Internal.RestKitServicesBuilder(services);
        }
    }
}
