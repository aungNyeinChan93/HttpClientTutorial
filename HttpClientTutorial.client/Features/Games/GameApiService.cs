using HttpClient.GameStoreDb.Models;

namespace HttpClientTutorial.client.Features.Games
{
    public class GameApiService
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public GameApiService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Get All Games
        public async Task<List<Game>> GetAllGamesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Game>>("/api/games");
            return response!;
        }
        
    }
}
