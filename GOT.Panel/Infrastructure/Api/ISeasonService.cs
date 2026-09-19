using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;

namespace GOT.Panel.Infrastructure.Api
{
    public interface ISeasonService
    {
        Task<HttpResponseWrapper<object>> CreateAsync(SeasonDto seasonDto);
        Task<HttpResponseWrapper<string>> DeleteAsync(int id);
        Task<HttpResponseWrapper<SeasonDto>> GetByIdAsync(int id);
        Task<HttpResponseWrapper<List<SeasonDto>>> GetListAsync(string url);
        Task<HttpResponseWrapper<List<SeasonDto>>> GetPaginatedAsync(string? name, PaginatedRequest paginated);
        Task<HttpResponseWrapper<object>> UpdateAsync(SeasonDto seasonDto);
    }
}