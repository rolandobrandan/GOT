using GOT.Entities.DTOs;
using GOT.Panel.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace GOT.Panel.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonServices _personService;

        public PersonController(IPersonServices personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Person(string name, PaginatedRequest paginated)
        {
            //var result = await _personService.GetListAsync();

            //if (result.Error)
            //{
            //    ViewBag.Error = await result.GetErrorMessageAsync();
            //    return View(new List<GOT.Entities.DTOs.PersonDto>());
            //}

            //return View(result.Response);

            var result = await _personService.GetPaginatedAsync(name, paginated);

            return View(result.Response);
        }
    }
}
