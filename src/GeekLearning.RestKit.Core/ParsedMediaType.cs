using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.RestKit.Core
{
    public class ParsedMediaType
    {
        public ParsedMediaType(string mediaType)
        {
            var lastPlus = mediaType.LastIndexOf('+');
            var firstSlash = mediaType.IndexOf('/');

            Type = mediaType.Substring(0, firstSlash);
            if (lastPlus >= 0)
            {
                SubType = mediaType.Substring(firstSlash + 1, lastPlus - firstSlash - 1);
                Suffix = mediaType.Substring(lastPlus + 1);
            }
            else
            {
                SubType = mediaType.Substring(firstSlash + 1);
            }
        }

        public string Type { get; }

        public string SubType { get; }

        public string Suffix { get; }

        public string StructuredType => this.Suffix ?? this.SubType;
    }
}
