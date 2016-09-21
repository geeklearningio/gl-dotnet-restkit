

namespace GeekLearning.RestKit.Json
{
    using System;
    using System.Collections.Generic;
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

        public HttpContent Format(object data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, JsonMediaType);
        }

        public bool Supports(string contentType)
        {
            return contentType.Equals(JsonMediaType, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<TTarget> TransformAsync<TTarget>(HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TTarget>(stringContent);
        }
    }
}
