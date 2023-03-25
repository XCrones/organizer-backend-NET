using organizer_backend_NET.Service.Interfaces;

namespace organizer_backend_NET.Service.Implements
{
    public class HttpClientService : IHttpClientService
    {
        public Task<TD?> Get<TD>(string url)
        {
            throw new NotImplementedException();
        }
    }
}
