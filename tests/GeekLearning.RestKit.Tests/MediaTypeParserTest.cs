using GeekLearning.RestKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GeekLearning.RestKit.Tests
{
    public class MediaTypeParserTest
    {

        [Theory(DisplayName = nameof(TestMediaTypes))]
        [InlineData("application/json", "application", "json", null, "json")]
        [InlineData("application/vnd.apptype+json", "application", "vnd.apptype", "json", "json")]
        [InlineData("application/vnd.apptype+xml", "application", "vnd.apptype", "xml", "xml")]
        [InlineData("application/xml+json", "application", "xml", "json", "json")]
        public void TestMediaTypes(string mediaType, string type, string subType, string suffix, string structured)
        {
            var parsedMediaType = new ParsedMediaType(mediaType);

            Assert.Equal(type, parsedMediaType.Type);
            Assert.Equal(subType, parsedMediaType.SubType);
            Assert.Equal(suffix, parsedMediaType.Suffix);
            Assert.Equal(structured, parsedMediaType.StructuredType);
        }
    }
}
