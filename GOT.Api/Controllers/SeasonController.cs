using GOT.Api.Code;
using GOT.Api.Infrastructure.Data;
using GOT.Entities.DTOs;
using GOT.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GOT.Api.Controllers
{
    public class SeasonController : ServiceControllerBase
    {
        // Generamos una variable global de la db para poder usarla
        private readonly GotDbContext _dbContext;

        public SeasonController(GotDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet()]
        public async Task<ActionResult<List<SeasonDto>>> GetPaginated([FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.Seasons
                .AsNoTracking()         // Trae los datos pero no los guarda en memoria, puede no usarse 
                .OrderByDescending(s => s.Id)       // Me traigo la info agregada recientemente
                .Skip((paginated.PageNumber - 1) * paginated.PageSize) // Calculo la cantidad de elementos de la tabla y los muestra de 10 en 10
                .Take(paginated.PageSize)       // Muestro la cantidad de vistas, 10 en cada pagina
                .ToListAsync();         // Ejecuto la consulta


            var resultDto = query.Select(s => new SeasonDto
            {
                Id = s.Id,
                Name = s.Name,
                Year = s.Year,
            }).ToList();

            return Ok(resultDto);
        }


        // Busca una temporada por su nombre
        [HttpGet()]
        [Route("search")]
        public async Task<ActionResult<List<SeasonDto>>> Search([FromQuery] string? name, [FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.Seasons
                .AsNoTracking()
                .Where(s => string.IsNullOrEmpty(name) || s.Name.Contains(name))  // Me fijo si name esta vacio o si tiene info y me la trae  
                .OrderByDescending(s => s.Id)       
                .Skip((paginated.PageNumber - 1) * paginated.PageSize) 
                .Take(paginated.PageSize)       
                .ToListAsync();

            var resultDto = query.Select(s => new SeasonDto
            {
                Id = s.Id,
                Name = s.Name,
                Year = s.Year,

            }).ToList();


            return Ok(resultDto);
        }



        [HttpGet()]
        [Route("{id:int}")]
        public async Task<ActionResult<SeasonDto>> GetById(int id)
        {
            var query = await _dbContext.Seasons
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            
            
            if(query == null)
            {
                return NotFound($"La temporada {id} no existe");
            }

            var resultDto = new SeasonDto
            {
                Id = query.Id,
                Name = query.Name,
                Year = query.Year,

            };


            return Ok(resultDto);

        }


        [HttpPost()]
        [Route("")]
        public async Task<ActionResult<SeasonDto>> Create([FromBody] SeasonDto seasonDto)
        {
            var query = await _dbContext.Seasons
                .FirstOrDefaultAsync(s => s.Name == seasonDto.Name);

            if(query != null)
            {
                return BadRequest($"Ya existe una temporada registrada con ese nombre!");
            }

            var Season = new Season
            {
               
                Name = seasonDto.Name,
                Year = seasonDto.Year,
            };

            _dbContext.Seasons.Add(Season);

           await _dbContext.SaveChangesAsync();

            return Ok(Season);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<SeasonDto>> Update(int id, [FromBody] SeasonDto seasonDto)
        {
            var query = await _dbContext.Seasons
                .FirstOrDefaultAsync (s => s.Id == id);

            if(query == null)
            {
                return NotFound($"La temporada {id} no se encontro");
            }

            query.Name = seasonDto.Name;
            query.Year = seasonDto.Year;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }



        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var query = await _dbContext.Seasons
                .FirstOrDefaultAsync (s => s.Id == id);


            if(query == null)
            {
                return NotFound($"La temporada {id} no existe");
            }

            _dbContext.Remove(query);
            await _dbContext.SaveChangesAsync();

            return Ok();

        }

        // cambios
    }
}
