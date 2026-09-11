using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;

namespace GOT.Panel.Infrastructure.Api
{
    public class SeasonService : ISeasonService
    {
        private const string url = "api/v1/season";
        private readonly HttpClient _httpClient;

        public SeasonService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<HttpResponseWrapper<List<SeasonDto>>> GetPaginatedAsync(string? name, PaginatedRequest paginated)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseWrapper<List<SeasonDto>>> GetListAsync()
        {
            var response = await _httpClient.GetAsync(url);
            return await BuildResponseAsync<List<SeasonDto>>(response);

        }

        public async Task<HttpResponseWrapper<List<SeasonDto>>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseWrapper<object>> CreateAsync(SeasonDto seasonDto)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseWrapper<object>> UpdateAsync(SeasonDto seasonDto)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseWrapper<string>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpResponseWrapper<T>> BuildResponseAsync<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();
                return new HttpResponseWrapper<T>(data, false, response);

            }
            return new HttpResponseWrapper<T>(default, true, response, await response.Content.ReadAsStringAsync());

        }

    }
}
