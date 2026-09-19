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

            string baseUrl;

            if (string.IsNullOrWhiteSpace(name))
            {
                baseUrl = $"{url}?PageNumber={paginated.PageNumber}&PageSize={paginated.PageSize}";
            }
            else
            {
                baseUrl = $"{url}/search?name={name}&PageNumber={paginated.PageNumber}&PageSize={paginated.PageSize}";
            }

            return await GetListAsync(baseUrl);

        }

        public async Task<HttpResponseWrapper<List<SeasonDto>>> GetListAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return await BuildResponseAsync<List<SeasonDto>>(response);

        }

        public async Task<HttpResponseWrapper<SeasonDto>> GetByIdAsync(int id)
        {
            var response = _httpClient.GetAsync($"{url}/{id}");
            return await BuildResponseAsync<SeasonDto>(await response);
        }

        public async Task<HttpResponseWrapper<object>> CreateAsync(SeasonDto seasonDto)
        {
            var response = _httpClient.PostAsJsonAsync(url, seasonDto);
            return await BuildResponseAsync<object>(await response);
        }

        public async Task<HttpResponseWrapper<object>> UpdateAsync(SeasonDto seasonDto)
        {
            var response = _httpClient.PutAsJsonAsync($"{url}/{seasonDto.Id}", seasonDto);
            return await BuildResponseAsync<object>(await response);

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
