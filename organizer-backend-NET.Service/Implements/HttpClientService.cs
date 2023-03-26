using organizer_backend_NET.Service.Interfaces;
using System.Text.Json;

namespace organizer_backend_NET.Service.Implements
{
    public class HttpClientService : IHttpClientService
    {
        private readonly JsonSerializerOptions _options;

        public HttpClientService()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TD?> Get<TD>(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TD>(content, _options);
        }
    }
}
