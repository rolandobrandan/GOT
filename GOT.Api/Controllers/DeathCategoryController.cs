using GOT.Api.Code;
using GOT.Api.Infrastructure.Data;
using GOT.Entities.DTOs;
using GOT.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GOT.Api.Controllers
{
    

    public class DeathCategoryController : ServiceControllerBase
    {
        // Generamos una variable global de la db para poder usarla

        private readonly GotDbContext _dbContext;

        public DeathCategoryController(GotDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet()]
        public async Task<ActionResult<List<DeathCategoryDto>>> GetPaginated([FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.DeathCategories
                .AsNoTracking()         // Trae los datos pero no los guarda en memoria, puede no usarse 
                .OrderByDescending(d => d.Id)       // Me traigo la info agregada recientemente
                .Skip((paginated.PageNumber - 1) * paginated.PageSize) // Calculo la cantidad de elementos de la tabla y los muestra de 10 en 10
                .Take(paginated.PageSize)       // Muestro la cantidad de vistas, 10 en cada pagina
                .ToListAsync();         // Ejecuto la consulta


            var resultDto = query.Select(d => new DeathCategoryDto
            {
                Id = d.Id,
                Category = d.Category,

            }).ToList();

            return Ok(resultDto);
        }



        // Buscar categoria por su nombre
        [HttpGet()]
        [Route("search")]
        public async Task<ActionResult<List<DeathCategoryDto>>> Search([FromQuery] string? category, [FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.DeathCategories
                .AsNoTracking()
                .Where(c => string.IsNullOrEmpty(category) || c.Category.Contains(category))  // Me fijo si name esta vacio o si tiene info y me la trae  
                .OrderByDescending(c => c.Id)
                .Skip((paginated.PageNumber - 1) * paginated.PageSize)
                .Take(paginated.PageSize)
                .ToListAsync();

            var resultDto = query.Select(c => new DeathCategoryDto
            {
                Id = c.Id,
                Category = c.Category,

            }).ToList();


            return Ok(resultDto);
        }


        // busca una categoria de muerte por su id 
        [HttpGet()]
        [Route("{id:int}")]
        public async Task<ActionResult<DeathCategoryDto>> GetById(int id)
        {
            var query = await _dbContext.DeathCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);


            if (query == null)
            {
                return NotFound($"La categoria de muerte {id} no existe");
            }

            var resultDto = new DeathCategoryDto
            {
                Id = query.Id,
                Category = query.Category,

            };


            return Ok(resultDto);

        }


        // creamos una nueva categoria de muerte
        [HttpPost()]
        [Route("")]
        public async Task<ActionResult<DeathCategoryDto>> Create([FromBody] DeathCategoryDto deathCategoryDto)
        {
            var query = await _dbContext.DeathCategories
                .FirstOrDefaultAsync(c => c.Category == deathCategoryDto.Category);

            if (query != null)
            {
                return BadRequest($"Ya existe una categoria registrada con ese nombre!");
            }

            var DeathCategory = new DeathCategory
            {

                Category = deathCategoryDto.Category,

            };

            _dbContext.DeathCategories.Add(DeathCategory);

            await _dbContext.SaveChangesAsync();

            return Ok(DeathCategory);
        }



        // Editamos o actualizamos una categoria de muerte existente
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<DeathCategoryDto>> Update(int id, [FromBody] DeathCategoryDto deathCategoryDto)
        {
            var query = await _dbContext.DeathCategories
                .FirstOrDefaultAsync(d => d.Id == id);

            if (query == null)
            {
                return NotFound($"La categoria de muerte {id} no se encontro");
            }

            query.Category = deathCategoryDto.Category;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        // eliminamos una categoria de muerte
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var query = await _dbContext.DeathCategories
                .FirstOrDefaultAsync(d => d.Id == id);


            if (query == null)
            {
                return NotFound($"La categoria {id} no existe");
            }

            _dbContext.Remove(query);
            await _dbContext.SaveChangesAsync();

            return Ok();

        }

    }
}
