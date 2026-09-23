using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Controllers
{
    public class KingdomController : Controller
    {
        private readonly IKingdomService _kingdomService;

        public KingdomController(IKingdomService kingdomService)
        {
            _kingdomService = kingdomService;
        }

        public async Task<IActionResult> Kingdom(string name, PaginatedRequest paginated)
        {

            var result = await _kingdomService.GetPaginatedAsync(name, paginated);

            return View(result.Response);
        }
    }
}
