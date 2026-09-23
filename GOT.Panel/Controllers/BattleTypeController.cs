using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Controllers
{
    public class BattleTypeController : Controller
    {
        private readonly IBattleTypeService _battleTypeService;

        public BattleTypeController(IBattleTypeService battleTypeService)
        {
            _battleTypeService = battleTypeService;
        }

        public async Task<IActionResult> BattleType(string battleType1, PaginatedRequest paginated)
        {

            var result = await _battleTypeService.GetPaginatedAsync(battleType1, paginated);

            return View(result.Response);
        }
    }
}
