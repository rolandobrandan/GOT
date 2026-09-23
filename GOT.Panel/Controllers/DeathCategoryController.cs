using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Controllers
{
    public class DeathCategoryController : Controller
    {
        private readonly IDeathCategoryService _deathCategoryService;

        public DeathCategoryController(IDeathCategoryService deathCategoryService)
        {
            _deathCategoryService = deathCategoryService;
        }

        public async Task<IActionResult> DeathCategory(string category, PaginatedRequest paginated)
        {

            var result = await _deathCategoryService.GetPaginatedAsync(category, paginated);

            return View(result.Response);
        }
    }
}
