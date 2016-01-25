namespace Go
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    public class HttpWrapper
    {
        private readonly Credentials credentials;

        public HttpWrapper(Credentials credentials)
        {
            this.credentials = credentials;
        }

        private HttpClient GetHttpClientWithBasicAuth()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(credentials.BasicAuthCredentials);
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            client.DefaultRequestHeaders.Authorization = header;
            return client;
        }

        public async Task<string> GetRawDataAsync(Uri uri)
        {
            using (var client = GetHttpClientWithBasicAuth())
            {
                var response = client.GetAsync(uri).Result;
                var rawData = await response.Content.ReadAsStringAsync();
                return rawData;
            }
        }

        public async Task<Stream> GetRawStreamAsync(Uri uri)
        {
            using (var client = GetHttpClientWithBasicAuth())
            {
                var response = client.GetAsync(uri).Result;
                return await response.Content.ReadAsStreamAsync();
            }
        }
    }
}
