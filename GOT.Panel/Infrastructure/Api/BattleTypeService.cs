using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace GOT.Panel.Infrastructure.Api
{
    public class BattleTypeService : IBattleTypeService
    {

        private const string url = "api/v1/battleType";
        private readonly HttpClient _httpClient;

        public BattleTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<HttpResponseWrapper<object>> CreateAsync(BattleTypeDto battleTypeDto)
        {
            var response = await _httpClient.PostAsJsonAsync(url, battleTypeDto);
            return await BuildResponseAsync<object>(response);
        }

        public async Task<HttpResponseWrapper<string>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{url}/{id}");
            return await BuildResponseAsync<string>(response);
        }

        public async Task<HttpResponseWrapper<BattleTypeDto>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{url}/{id}");
            return await BuildResponseAsync<BattleTypeDto>(response);
        }

        public async Task<HttpResponseWrapper<List<BattleTypeDto>>> GetListAsync(string baseUrl)
        {
            var response = await _httpClient.GetAsync(baseUrl);
            return await BuildResponseAsync<List<BattleTypeDto>>(response);
        }

        public async Task<HttpResponseWrapper<List<BattleTypeDto>>> GetPaginatedAsync(string? battleType1, PaginatedRequest paginated)
        {
            string baseUrl;

            if (string.IsNullOrWhiteSpace(battleType1))
            {
                baseUrl = $"{url}?PageNumber={paginated.PageNumber}&PageSize={paginated.PageSize}";
            }
            else
            {
                baseUrl = $"{url}/search?name={battleType1}&PageNumber={paginated.PageNumber}&PageSize={paginated.PageSize}";
            }

            return await GetListAsync(baseUrl);
        }

        public Task<HttpResponseWrapper<List<BattleTypeDto>>> Search(string? battleType1, PaginatedRequest paginated)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseWrapper<BattleTypeDto>> UpdateAsync(BattleTypeDto battleTypeDto)
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
