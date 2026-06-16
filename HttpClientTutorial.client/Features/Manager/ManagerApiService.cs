using HttpClient.domain.Features.Manager.ReqResModel;
using HttpClient.shared.Commons;
using System.Net.Http;


namespace HttpClientTutorial.client.Features.Manager
{
    public class ManagerApiService
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public ManagerApiService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //GetAllManager
        public async Task<Result<ManagersResponse>> GetAllManagersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<Result<ManagersResponse>>("/api/managers");
            return response!;
        }
    }
}
