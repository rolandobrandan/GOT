using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Infrastructure.Api
{
    public interface IBattleTypeService
    {
        Task<HttpResponseWrapper<object>> CreateAsync(BattleTypeDto battleTypeDto);
        Task<HttpResponseWrapper<string>> DeleteAsync(int id);
        Task<HttpResponseWrapper<BattleTypeDto>> GetByIdAsync(int id);
        Task<HttpResponseWrapper<List<BattleTypeDto>>> GetPaginatedAsync(string? battleType1, PaginatedRequest paginated);
        Task<HttpResponseWrapper<List<BattleTypeDto>>> Search([FromQuery] string? battleType1, PaginatedRequest paginated);
        Task<HttpResponseWrapper<List<BattleTypeDto>>> GetListAsync(string baseUrl);

        Task<HttpResponseWrapper<BattleTypeDto>> UpdateAsync(BattleTypeDto battleTypeDto);
    }
}
