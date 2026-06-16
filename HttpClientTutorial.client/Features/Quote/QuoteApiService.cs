using HttpClientTutorial.client.Models;

namespace HttpClientTutorial.client.Features.Quote
{
    public class QuoteApiService
    {
        private readonly HttpClient _httpClient;

        public QuoteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        #region GetAll
        public async Task<QuoteList> GetAllQuotes()
        {
            var response = await _httpClient.GetFromJsonAsync<QuoteList>("/quotes");
            return response!;
        }
        #endregion


    }
}
