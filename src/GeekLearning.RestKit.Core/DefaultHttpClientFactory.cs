using System.Net.Http;

namespace GeekLearning.RestKit.Core
{
    public class DefaultHttpClientFactory: IHttpClientFactory
    {
        public HttpClient CreateClient(){
            return new HttpClient();
        }
    }
}
