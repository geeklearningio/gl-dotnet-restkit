namespace GeekLearning.RestKit.Core
{
    using Polly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProvideErrorHandlingPolicy
    {
        Policy Policy { get; }
    }
}
