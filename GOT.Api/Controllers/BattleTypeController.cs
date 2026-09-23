using GOT.Api.Code;
using GOT.Api.Infrastructure.Data;
using GOT.Entities.DTOs;
using GOT.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GOT.Api.Controllers
{
    

    public class BattleTypeController : ServiceControllerBase
    {
        // Generamos una variable global de la db para poder usarla

        private readonly GotDbContext _dbContext;

        public BattleTypeController(GotDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet()]
        public async Task<ActionResult<List<BattleTypeDto>>> GetPaginated([FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.BattleTypes
                .AsNoTracking()         // Trae los datos pero no los guarda en memoria, puede no usarse 
                .OrderByDescending(b => b.Id)       // Me traigo la info agregada recientemente
                .Skip((paginated.PageNumber - 1) * paginated.PageSize) // Calculo la cantidad de elementos de la tabla y los muestra de 10 en 10
                .Take(paginated.PageSize)       // Muestro la cantidad de vistas, 10 en cada pagina
                .ToListAsync();         // Ejecuto la consulta


            var resultDto = query.Select(b => new BattleTypeDto
            {
                Id = b.Id,
                BattleType1 = b.BattleType1,
            }).ToList();

            return Ok(resultDto);
        }

        // Busca tipo de batallas por su nombre
        [HttpGet()]
        [Route("search")]
        public async Task<ActionResult<List<BattleTypeDto>>> Search([FromQuery] string? battleType1, [FromQuery] PaginatedRequest paginated)
        {
            var query = await _dbContext.BattleTypes
                .AsNoTracking()
                .Where(b => string.IsNullOrEmpty(battleType1) || b.BattleType1.Contains(battleType1))  // Me fijo si name esta vacio o si tiene info y me la trae  
                .OrderByDescending(b => b.Id)
                .Skip((paginated.PageNumber - 1) * paginated.PageSize)
                .Take(paginated.PageSize)
                .ToListAsync();

            var resultDto = query.Select(b => new BattleTypeDto
            {
                Id = b.Id,
                BattleType1 = b.BattleType1,

            }).ToList();


            return Ok(resultDto);
        }


        // busco un tipo de batalla por id
        [HttpGet()]
        [Route("{id:int}")]
        public async Task<ActionResult<BattleTypeDto>> GetById(int id)
        {
            var query = await _dbContext.BattleTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);


            if (query == null)
            {
                return NotFound($"El tipo de batalla {id} no existe");
            }

            var resultDto = new BattleTypeDto
            {
                Id = query.Id,
                BattleType1 = query.BattleType1,

            };


            return Ok(resultDto);

        }


        // creamos un nuevo tipo de batalla
        [HttpPost()]
        [Route("")]
        public async Task<ActionResult<BattleTypeDto>> Create([FromBody] BattleTypeDto battleTypeDto)
        {
            var query = await _dbContext.BattleTypes
                .FirstOrDefaultAsync(b => b.BattleType1 == battleTypeDto.BattleType1);

            if (query != null)
            {
                return BadRequest($"Ya existe un tipo de batalla registrado con ese nombre!");
            }

            var BattleType = new BattleType
            {

                BattleType1 = battleTypeDto.BattleType1,

            };

            _dbContext.BattleTypes.Add(BattleType);

            await _dbContext.SaveChangesAsync();

            return Ok(BattleType);
        }


        // Editamos o actualizamos un tipo de batalla existente
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<BattleTypeDto>> Update(int id, [FromBody] BattleTypeDto battleTypeDto)
        {
            var query = await _dbContext.BattleTypes
                .FirstOrDefaultAsync(b => b.Id == id);

            if (query == null)
            {
                return NotFound($"El tipo de batalla {id} no se encontro");
            }

            query.BattleType1 = battleTypeDto.BattleType1;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        // eliminamos un tipo de batalla
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var query = await _dbContext.BattleTypes
                .FirstOrDefaultAsync(b => b.Id == id);


            if (query == null)
            {
                return NotFound($"El tipo de batalla {id} no existe");
            }

            _dbContext.Remove(query);
            await _dbContext.SaveChangesAsync();

            return Ok();

        }


    }
}
