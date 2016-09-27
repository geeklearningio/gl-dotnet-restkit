namespace GeekLearning.RestKit.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class ClientOptionsBase : IProvideRequestFilters
    {
        private List<InjectionDescriptor> requestfilters = new List<InjectionDescriptor>();

        public void AddFilter<TRequestFilter>(params object[] arguments)
            where TRequestFilter: IRequestFilter
        {
            this.requestfilters.Add(new InjectionDescriptor {
                Type = typeof(TRequestFilter),
                Arguments = arguments
            });
        }

        public IEnumerable<InjectionDescriptor> RequestFilters
        {
            get
            {
                return requestfilters;
            }
        }
    }
}
