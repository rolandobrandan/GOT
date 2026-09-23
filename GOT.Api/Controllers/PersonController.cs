using GOT.Api.Code;
using GOT.Api.Infrastructure.Data;
using GOT.Entities.DTOs;
using GOT.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GOT.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]

    public class PersonController : ServiceControllerBase
    {

        // Generamos una variable global de la db para poder usarla

        private readonly GotDbContext _dbContext;

        public PersonController(GotDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet()]
        public async Task<ActionResult<List<PersonDto>>> GetPaginated([FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.People
                .AsNoTracking()         // Trae los datos pero no los guarda en memoria, puede no usarse 
                .OrderByDescending(p => p.Id)       // Me traigo la info agregada recientemente
                .Skip((paginated.PageNumber - 1) * paginated.PageSize) // Calculo la cantidad de elementos de la tabla y los muestra de 10 en 10
                .Take(paginated.PageSize)       // Muestro la cantidad de vistas, 10 en cada pagina
                .ToListAsync();         // Ejecuto la consulta


            var resultDto = query.Select(p => new PersonDto
            {
                Id = p.Id,
                Name = p.Name,

            }).ToList();

            return Ok(resultDto);

        }


        // busco por nombre de la persona
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<PersonDto>>> Search([FromQuery] string? name, [FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.People
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name))  // Me fijo si name esta vacio o si tiene info y me la trae  
                .OrderByDescending(p => p.Id)
                .Skip((paginated.PageNumber - 1) * paginated.PageSize)
                .Take(paginated.PageSize)
                .ToListAsync();

            var resultDto = query.Select(p => new PersonDto
            {
                Id = p.Id,
                Name = p.Name,

            }).ToList();

            return Ok(resultDto);

        }


        // busco la persona por id
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var query = await _dbContext.People
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (query == null)
            {
                return NotFound($"La persona {id} no existe");
            }

            var resultDto = new PersonDto
            {
                Id = query.Id,
                Name = query.Name,
            };

            return Ok(resultDto);
        }



        // no anda 
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<PersonDto>> Create([FromBody] PersonDto personDto)
        {
            var query = _dbContext.People
                .FirstOrDefaultAsync(p => p.Name == personDto.Name);

            if (query != null)
            {
                return BadRequest($"Ya existe una persona registrada con ese nombre!");
            }

            var person = new Person
            {
                // Id = personDto.Id,
                Name = personDto.Name,
            };

            _dbContext.People.Add(person); // agrego la nueva persona al Modelo (model)

            await _dbContext.SaveChangesAsync();

            return Ok(person);
        }


        // actualiza los campos de la persona por su id
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<PersonDto>> Update(int id, [FromBody] PersonDto personDto)
        {
            var query = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (query == null)
            {
                return NotFound($"La persona {id} no existe!");
            }

            query.Name = personDto.Name;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        // borra una persona por su id
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var query = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (query == null)
            {
                return NotFound($"La persona {id} no existe");
            }

            _dbContext.Remove(query);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }



    }
}
