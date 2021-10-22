using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/visitas")]
    public class VisitasController : ControllerBase

    {

        private readonly ApplicationDbContext context;

        public VisitasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Visita>>> Get()
        {
            return await context.Visitas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Visita visita)
        {
            var existeEncargado = await context.Usuarios.AnyAsync(x => x.Id == visita.UsuarioId);

            if (!existeEncargado)
            {
                return BadRequest($"No existe el encargado de Id: {visita.UsuarioId}");
            }

            context.Add(visita);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("encargado/{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Visita>>> GetVisitasPorEncargado(int id)
        {
            var existeEncargado = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existeEncargado)
            {
                return NotFound("El id de encargado no existe");
            }

            return await context.Visitas.Include(x=> x.Usuario).Where(x => x.UsuarioId == id).ToListAsync();
        }



    }
}
