using System.Net.Http;

namespace GeekLearning.RestKit.Core
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}
