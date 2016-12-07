using System.Net.Http;

namespace GeekLearning.RestKit.Core
{
    public class CustomHandlerHttpClientFactory<THandler> : IHttpClientFactory
        where THandler: HttpMessageHandler, new() 
    {
        public HttpClient CreateClient(){
            return new HttpClient(new THandler());
        }
    }
}
