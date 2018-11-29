using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Mde.Net.Reactive.Extensions;

namespace Mde.Net.Reactive
{
    class MediumApi
    {

        public IObservable<string> Call(string path, IDictionary<string, string> parameters) =>
            // Resolve the Uri
            GetUri(path, parameters)

                // Call service
                .SelectMany(uri => GetAsync(uri))

                // Read response content
                .SelectMany(response => response.Content.ReadAsStringAsync())

                // Removes initial string to prevent JSON Hijacking
                .Select(responseBody => responseBody.Replace("])}while(1);</x>", string.Empty));

        public IObservable<string> GetUri(string path, IDictionary<string, string> parameters) =>
            parameters
                .ToQueryString()
                .Select(q => new UriBuilder("https://medium.com")
                {
                    Path = path,
                    Query = q
                }.ToString());

        public Task<HttpResponseMessage> GetAsync(string uri)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "MdeApp/1.0.0");

            return httpClient.GetAsync(uri);
        }
    }
}