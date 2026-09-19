using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Api;
using GOT.Panel.Infrastructure.Services;
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

        public async Task<IActionResult> Season(string name, PaginatedRequest paginated)
        {
            var result = await _seasonService.GetPaginatedAsync(name, paginated);

            return View(result.Response);
        }

        public async Task<IActionResult> SeasonDetails(int Id)
        {
            SeasonDto model = new();
            ViewBag.Text = "Nueva Temporada";

            if (Id != 0)
            {
                var result = await _seasonService.GetByIdAsync(Id);
                if (!result.Error)
                {
                    ViewBag.Text = "Modificando Temporada";
                    model = result.Response ?? new SeasonDto();
                }

            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> SaveChanges(SeasonDto model)
        {
            HttpResponseWrapper<object> result;
            if (model.Id != 0)
            {
                result = await _seasonService.UpdateAsync(model);
            }
            else
            {
                result = await _seasonService.CreateAsync(model);

            }

            if (!result.Error)
            {
                return RedirectToAction("Season");
            }
            else
            {
                return NotFound();
            }
        }
    }
}

