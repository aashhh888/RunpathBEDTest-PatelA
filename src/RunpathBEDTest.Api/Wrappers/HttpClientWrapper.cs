using System.Net.Http;

namespace RunpathBEDTest.Wrappers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public string Get(string requestUri)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync(requestUri).Result;
                var contentResult = result.Content.ReadAsStringAsync().Result;
                return contentResult;
            }
        }
    }
}
