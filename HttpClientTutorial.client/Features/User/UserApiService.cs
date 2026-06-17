using HttpClient.domain.Features.User.ReqResModels;
using HttpClient.shared.Commons;

namespace HttpClientTutorial.client.Features.User
{
    public class UserApiService
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public UserApiService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region
        public async Task<Result<UserListResponse>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<Result<UserListResponse>>("/api/users");
            return response!;
        }
        #endregion
    }
}
