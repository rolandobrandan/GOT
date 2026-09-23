using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Infrastructure.Api
{
    public class PersonServices : IPersonServices
    {
        private const string url = "api/v1/person";
        private readonly HttpClient _httpClient;

        public PersonServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<List<PersonDto>>> GetPaginatedAsync(string? name, PaginatedRequest paginated)
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

        
        public async Task<HttpResponseWrapper<List<PersonDto>>> GetListAsync(string baseUrl)
        {
            var response = await _httpClient.GetAsync(baseUrl);
            return await BuildResponseAsync<List<PersonDto>>(response);
        }

        public async Task<HttpResponseWrapper<PersonDto>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{url}/{id}");
            return await BuildResponseAsync<PersonDto>(response);
        }

        public async Task<HttpResponseWrapper<object>> CreateAsync(PersonDto personDto)
        {
            var response = await _httpClient.PostAsJsonAsync(url, personDto);
            return await BuildResponseAsync<object>(response);
        }

        public async Task<HttpResponseWrapper<object>> UpdateAsync(PersonDto personDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{url}/{personDto.Id}", personDto);
            return await BuildResponseAsync<object>(response);
        }

        public async Task<HttpResponseWrapper<string>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{url}/{id}");
            return await BuildResponseAsync<string>(response);
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



