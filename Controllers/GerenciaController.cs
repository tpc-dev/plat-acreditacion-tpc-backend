using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/gerencias")]
    public class GerenciaController : ControllerBase 
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GerenciaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Gerencia>>> Get()
        {
            return await context.Gerencias.ToListAsync();
        }

        [HttpGet("activos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Gerencia>>> GetAreasActivas()
        {
            return await context.Gerencias.Where(gerencia => gerencia.Activo == true).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(NuevaGerenciaDTO nuevaGerenciaDTO)
        {
            var nuevaGerenciaDTOMapped = mapper.Map<Gerencia>(nuevaGerenciaDTO);
            context.Add(nuevaGerenciaDTOMapped);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Area area, int id)
        {
            if (area.Id != id)
            {
                return BadRequest("El id del area no coincide con el id de la URL");
            }

            bool existe = await context.Areas.AnyAsync(area => area.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(area);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Areas.AnyAsync(area => area.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Area() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
