using GOT.Api.Code;
using GOT.Api.Infrastructure.Data;
using GOT.Entities.DTOs;
using GOT.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GOT.Api.Controllers
{
    

    public class KingdomController : ServiceControllerBase
    {
        // Generamos una variable global de la db para poder usarla

        private readonly GotDbContext _dbContext;

        public KingdomController(GotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet()]
        public async Task<ActionResult<List<KingdomDto>>> GetPaginated([FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.Kingdoms
                .AsNoTracking()         // Trae los datos pero no los guarda en memoria, puede no usarse 
                .OrderByDescending(k => k.Id)       // Me traigo la info agregada recientemente
                .Skip((paginated.PageNumber - 1) * paginated.PageSize) // Calculo la cantidad de elementos de la tabla y los muestra de 10 en 10
                .Take(paginated.PageSize)       // Muestro la cantidad de vistas, 10 en cada pagina
                .ToListAsync();         // Ejecuto la consulta


            var resultDto = query.Select(k => new KingdomDto
            {
                Id = k.Id,
                Name = k.Name,
                Summary = k.Summary,
                Url = k.Url,
            }).ToList();

            return Ok(resultDto);
        }


        // Busca reino por su nombre
        [HttpGet()]
        [Route("search")]
        public async Task<ActionResult<List<KingdomDto>>> Search([FromQuery] string? name, [FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.Kingdoms
                .AsNoTracking()
                .Where(k => string.IsNullOrEmpty(name) || k.Name.Contains(name))  // Me fijo si name esta vacio o si tiene info y me la trae  
                .OrderByDescending(k => k.Id)
                .Skip((paginated.PageNumber - 1) * paginated.PageSize)
                .Take(paginated.PageSize)
                .ToListAsync();

            var resultDto = query.Select(k => new KingdomDto
            {
                Id = k.Id,
                Name = k.Name,
                Summary = k.Summary,
                Url = k.Url,

            }).ToList();


            return Ok(resultDto);
        }


        [HttpGet()]
        [Route("{id:int}")]
        public async Task<ActionResult<KingdomDto>> GetById(int id)
        {
            var query = await _dbContext.Kingdoms
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);


            if (query == null)
            {
                return NotFound($"El reino {id} no existe");
            }

            var resultDto = new KingdomDto
            {
                Id = query.Id,
                Name = query.Name,
                Summary = query.Summary,
                Url = query.Url,

            };


            return Ok(resultDto);

        }


        // creamos un nuevo reino
        [HttpPost()]
        [Route("")]
        public async Task<ActionResult<KingdomDto>> Create([FromBody] KingdomDto kingdomDto)
        {
            var query = await _dbContext.Kingdoms
                .FirstOrDefaultAsync(k => k.Name == kingdomDto.Name);

            if (query != null)
            {
                return BadRequest($"Ya existe un reino registrado con ese nombre!");
            }

            var Kingdom = new Kingdom
            {

                Name = kingdomDto.Name,
                Summary = kingdomDto.Summary,
                Url = kingdomDto.Url,

            };

            _dbContext.Kingdoms.Add(Kingdom);

            await _dbContext.SaveChangesAsync();

            return Ok(Kingdom);
        }


        // Editamos o actualizamos un reino existente
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<KingdomDto>> Update(int id, [FromBody] KingdomDto kingdomDto)
        {
            var query = await _dbContext.Kingdoms
                .FirstOrDefaultAsync(k => k.Id == id);

            if (query == null)
            {
                return NotFound($"El reino {id} no se encontro");
            }

            query.Name = kingdomDto.Name;
            query.Summary = kingdomDto.Summary;
            query.Url = kingdomDto.Url;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        // eliminamos un reino 
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var query = await _dbContext.Kingdoms
                .FirstOrDefaultAsync(k => k.Id == id);


            if (query == null)
            {
                return NotFound($"El reino {id} no existe");
            }

            _dbContext.Remove(query);
            await _dbContext.SaveChangesAsync();

            return Ok();

        }

    }
}
