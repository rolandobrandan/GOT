using GOT.Panel.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;

        }

        public async Task<IActionResult> Season()
        {
            var result = await _seasonService.GetListAsync();

            return View(result.Response);
        }
    }
}
