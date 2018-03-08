namespace GeekLearning.Http
{
        using Microsoft.Net.Http.Headers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    // http://www.stefanhendriks.com/2016/05/11/integration-testing-your-asp-net-core-app-dealing-with-anti-request-forgery-csrf-formdata-and-cookies/
    public static class CookiesHelper
        {

            public static void ExtractCookiesFromResponse(this HttpResponseMessage response, IDictionary<string, string> cookies)
            {
                if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
                {
                    SetCookieHeaderValue.ParseList(values.ToList()).ToList().ForEach(cookie =>
                    {
                        cookies[cookie.Name.Value] =  cookie.Value.Value;
                    });
                }
            }

            public static HttpRequestMessage PutCookiesOnRequest(this HttpRequestMessage request, IDictionary<string, string> cookies)
            {
                cookies.Keys.ToList().ForEach(key =>
                {
                    request.Headers.Add("Cookie", new CookieHeaderValue(key, cookies[key]).ToString());
                });

                return request;
            }
        }
}
