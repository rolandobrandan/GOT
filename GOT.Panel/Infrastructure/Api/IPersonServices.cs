using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Infrastructure.Api
{
    public interface IPersonServices
    {
        Task<HttpResponseWrapper<List<PersonDto>>> GetPaginatedAsync(string? name, PaginatedRequest paginated);
        Task<HttpResponseWrapper<List<PersonDto>>> GetListAsync(string baseUrl);
        Task<HttpResponseWrapper<PersonDto>> GetByIdAsync(int id);
        Task<HttpResponseWrapper<object>> CreateAsync(PersonDto personDto);
        Task<HttpResponseWrapper<object>> UpdateAsync(PersonDto personDto);
        Task<HttpResponseWrapper<string>> DeleteAsync(int id);
    }
}
