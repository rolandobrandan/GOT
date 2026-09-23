using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Infrastructure.Api
{
    public interface IDeathCategoryService
    {
        Task<HttpResponseWrapper<object>> CreateAsync(DeathCategoryDto deathCategoryDto);
        Task<HttpResponseWrapper<string>> DeleteAsync(int id);
        Task<HttpResponseWrapper<DeathCategoryDto>> GetByIdAsync(int id);
        Task<HttpResponseWrapper<List<DeathCategoryDto>>> GetPaginatedAsync(string? category, PaginatedRequest paginated);
        Task<HttpResponseWrapper<List<DeathCategoryDto>>> Search(string? category, PaginatedRequest paginated);
        Task<HttpResponseWrapper<object>> UpdateAsync(DeathCategoryDto deathCategoryDto);
    }
}
