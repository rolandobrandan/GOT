using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;

namespace GOT.Panel.Infrastructure.Api
{
    public class KingdomService : IKingdomService
    {

        private const string url = "api/v1/kingdom";
        private readonly HttpClient _httpClient;

        public KingdomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<object>> CreateAsync(KingdomDto kingdomDto)
        {
            var response = await _httpClient.PostAsJsonAsync(url, kingdomDto);
            return await BuildResponseAsync<object>(response);
        }

        public async Task<HttpResponseWrapper<string>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{url}/{id}");
            return await BuildResponseAsync<string>(response);
        }

        public async Task<HttpResponseWrapper<KingdomDto>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{url}/{id}");
            return await BuildResponseAsync<KingdomDto>(response);
        }

        public async Task<HttpResponseWrapper<List<KingdomDto>>> GetListAsync(string baseUrl)
        {
            var response = await _httpClient.GetAsync(baseUrl);
            return await BuildResponseAsync<List<KingdomDto>>(response);
        }

        public async Task<HttpResponseWrapper<List<KingdomDto>>> GetPaginatedAsync(string? name, PaginatedRequest paginated)
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

        public Task<HttpResponseWrapper<List<KingdomDto>>> Search(string? name, PaginatedRequest paginated)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseWrapper<object>> UpdateAsync(KingdomDto kingdomDto)
        {
            throw new NotImplementedException();
        }


        // 
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
