namespace organizer_backend_NET.Service.Interfaces
{
    public interface IHttpClientService
    {
        public Task<TD?> Get<TD>(string url);
    }
}
