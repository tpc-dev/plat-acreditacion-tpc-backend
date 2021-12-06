using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/estado-acreditacion")]
    public class EstadoAcreditacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EstadoAcreditacionController(ApplicationDbContext context,  IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EstadoAcreditacion>>> Get()
        {
            return await context.EstadosAcreditacion.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(NuevoEstadoAcreditacionDTO nuevoEstadoAcreditacionDTO)
        {
            var nuevoEstadoAcreditacionDTOMapped = mapper.Map<EstadoAcreditacion>(nuevoEstadoAcreditacionDTO);
            context.Add(nuevoEstadoAcreditacionDTOMapped);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(EstadoAcreditacion estadoAcreditacion, int id)
        {
            if (estadoAcreditacion.Id != id)
            {
                return BadRequest("El id del estadoAcreditacion no coincide con el id de la URL");
            }

            bool existe = await context.EstadosAcreditacion.AnyAsync(estadoAcreditacionS => estadoAcreditacionS.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(estadoAcreditacion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.EstadosAcreditacion.AnyAsync(estadoAcreditacion => estadoAcreditacion.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new EstadoAcreditacion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
