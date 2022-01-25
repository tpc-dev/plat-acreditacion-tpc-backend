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
        public async Task<ActionResult> Put(Gerencia gerencia, int id)
        {
            if (gerencia.Id != id)
            {
                return BadRequest("El id del gerencia no coincide con el id de la URL");
            }

            bool existe = await context.Gerencias.AnyAsync(gerencia => gerencia.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(gerencia);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Gerencias.AnyAsync(gerencia => gerencia.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Gerencia() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
