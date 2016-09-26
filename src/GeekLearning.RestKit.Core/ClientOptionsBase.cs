using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public abstract class ClientOptionsBase : IProvideRequestFilters
    {
        private List<IRequestFilter> requestfilters = new List<IRequestFilter>();

        public void AddFilter(IRequestFilter filter)
        {
            this.requestfilters.Add(filter);
        }

        public IEnumerable<IRequestFilter> RequestFilters
        {
            get
            {
                return requestfilters;
            }
        }
    }
}
