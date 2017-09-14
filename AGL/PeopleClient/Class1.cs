using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.Client
{
    using System.Globalization;
    using System.Net.Http;

    public class PeopleClient
    {

        private readonly HttpClient _httpClient;

        private const string ServiceUri = "http://agl-developer-test.azurewebsites.net/people.json";
        private const int TimeoutSeconds = 60;

        public PeopleClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ServiceUri),
                Timeout = TimeSpan.FromSeconds(TimeoutSeconds)
            };
        }


        private async Task PingOneSitemapToOneLocation(string sitemapUrl)
        {
            var response = await _httpClient.GetAsync(sitemapUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("df");
            }
        }

    }
}
