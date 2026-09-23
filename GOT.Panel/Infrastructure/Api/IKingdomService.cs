using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Infrastructure.Api
{
    public interface IKingdomService
    {
        
            Task<HttpResponseWrapper<object>> CreateAsync(KingdomDto kingdomDto);
            Task<HttpResponseWrapper<string>> DeleteAsync(int id);
            Task<HttpResponseWrapper<KingdomDto>> GetByIdAsync(int id);
            Task<HttpResponseWrapper<List<KingdomDto>>> GetPaginatedAsync(string? name, PaginatedRequest paginated);
            Task<HttpResponseWrapper<List<KingdomDto>>> Search(string? name, PaginatedRequest paginated);
            Task<HttpResponseWrapper<List<KingdomDto>>> GetListAsync(string baseUrl);

            Task<HttpResponseWrapper<object>> UpdateAsync(KingdomDto kingdomDto);
        
    }
}
