

namespace GeekLearning.RestKit.Json
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using GeekLearning.RestKit.Core;
    using Newtonsoft.Json;

    public class JsonMediaFormatter : IMediaFormatter
    {
        public const string JsonMediaType = "application/json";
        public JsonMediaFormatter()
        {
        }

        public HttpContent Format(object body, IDictionary<string, IFormData> formData)
        {
            return new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, JsonMediaType);
        }

        public bool Supports(ParsedMediaType mediaType)
        {
            return string.Equals(mediaType.Type, "application", StringComparison.OrdinalIgnoreCase)
                && string.Equals(mediaType.StructuredType, "json", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<TTarget> TransformAsync<TTarget>(HttpContent content)
        {
            using (var streamReader = new StreamReader(await content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<TTarget>(jsonReader);
            }
        }
    }
}
