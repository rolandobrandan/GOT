using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;
using System.Xml.Linq;

namespace GOT.Panel.Infrastructure.Api
{
    public class DeathCategoryService : IDeathCategoryService
    {

        private const string url = "api/v1/deathCategory";
        private readonly HttpClient _httpClient;

        public DeathCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<HttpResponseWrapper<object>> CreateAsync(DeathCategoryDto deathCategoryDto)
        {
            var response = await _httpClient.PostAsJsonAsync(url, deathCategoryDto);
            return await BuildResponseAsync<object>(response);
        }

        public async Task<HttpResponseWrapper<string>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{url}/{id}");
            return await BuildResponseAsync<string>(response);
        }

        public async Task<HttpResponseWrapper<DeathCategoryDto>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{url}/{id}");
            return await BuildResponseAsync<DeathCategoryDto>(response);
        }

        public async Task<HttpResponseWrapper<List<DeathCategoryDto>>> GetListAsync(string baseUrl)
        {
            var response = await _httpClient.GetAsync(baseUrl);
            return await BuildResponseAsync<List<DeathCategoryDto>>(response);
        }


        public async Task<HttpResponseWrapper<List<DeathCategoryDto>>> GetPaginatedAsync(string? category, PaginatedRequest paginated)
        {
            string baseUrl;

            if (string.IsNullOrWhiteSpace(category))
            {
                baseUrl = $"{url}?PageNumber={paginated.PageNumber}&PageSize={paginated.PageSize}";
            }
            else
            {
                baseUrl = $"{url}/search?name={category}&PageNumber={paginated.PageNumber}&PageSize={paginated.PageSize}";
            }

            return await GetListAsync(baseUrl);
        }

        public async Task<HttpResponseWrapper<List<DeathCategoryDto>>> Search(string? category, PaginatedRequest paginated)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseWrapper<object>> UpdateAsync(DeathCategoryDto deathCategoryDto)
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
