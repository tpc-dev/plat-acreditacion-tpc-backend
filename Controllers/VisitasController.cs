using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/visitas")]
    public class VisitasController : ControllerBase

    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VisitasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Visita>>> Get()
        {
            return await context.Visitas.Include(x => x.Usuario).ToListAsync();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Visita visita, int id)
        {
            if (visita.Id != id)
            {
                return BadRequest("El id del visita no coincide con el id de la URL");
            }

            bool existe = await context.Visitas.AnyAsync(visita => visita.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(visita);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Visita visita)
        {
            var existeEncargado = await context.Usuarios.AnyAsync(x => x.Id == visita.UsuarioId);

            if (!existeEncargado)
            {
                return BadRequest($"No existe el encargado de Id: {visita.UsuarioId}");
            }

            visita.CreatedAt = DateTime.Now;
            visita.UpdatedAt = DateTime.Now;    

            context.Add(visita);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("marcar-ingreso/{idVisita:int}")]
        public async Task<ActionResult> PostMarcarIngreso(int idVisita)
        {
            var existeVisita = await context.Visitas.AnyAsync(x => x.Id == idVisita);

            if (!existeVisita)
            {
                return BadRequest($"No existe la visita de Id: {idVisita}");
            }

            var visitaBuscada = await context.Visitas.FirstOrDefaultAsync(x => x.Id == idVisita);
            visitaBuscada.HaIngresado = true;
            context.Entry(visitaBuscada).State = EntityState.Modified;

            // creacion ingreso historico visita
            IngresoVisitaDTO ingresoVisitaDTO = new() { VisitaId = idVisita, FechaEvento = DateTime.Now, Tipo = "ENTRADA" };
            var visitaMapead = mapper.Map<IngresoVisitas>(ingresoVisitaDTO);
            context.Add(visitaMapead);
            //

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("marcar-salida/{idVisita:int}")]
        public async Task<ActionResult> PostMarcarSalida(int idVisita)
        {
            var existeVisita = await context.Visitas.AnyAsync(x => x.Id == idVisita);

            if (!existeVisita)
            {
                return BadRequest($"No existe la visita de Id: {idVisita}");
            }

            var visitaBuscada = await context.Visitas.FirstOrDefaultAsync(x => x.Id == idVisita);
            visitaBuscada.HaIngresado = false;
            context.Entry(visitaBuscada).State = EntityState.Modified;

            // creacion ingreso historico visita
            IngresoVisitaDTO ingresoVisitaDTO = new() { VisitaId = idVisita, FechaEvento = DateTime.Now, Tipo = "SALIDA" };
            var visitaMapead = mapper.Map<IngresoVisitas>(ingresoVisitaDTO);
            context.Add(visitaMapead);
            //

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

            return await context.Visitas.Include(x => x.Area).Include(x=> x.Usuario).Where(x => x.UsuarioId == id).ToListAsync();
        }

        [HttpGet("activas")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Visita>>> GetVisitasActivas()
        {
            return await context.Visitas.Include(x => x.Area).Include(x => x.Usuario).Where(x => x.HaIngresado == false).ToListAsync();
        }

        [HttpGet("hoy")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Visita>>> GetVisitasHoy()
        {   
            DateTime fechaHoy = DateTime.Now;
            return await context.Visitas.Include(x => x.Area).Include(x => x.Usuario).Where(x => x.FechaVisita.DayOfYear == fechaHoy.DayOfYear && x.FechaVisita.Month == fechaHoy.Month && x.FechaVisita.Year == fechaHoy.Year).ToListAsync();
        }

        [HttpGet("agendadas")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Visita>>> GetVisitasAgendadas()
        {

            // VISITAS AGENDADAS DESDE HOY EN ADELANTE 
            //DateTime fechaManana = DateTime.Now.AddDays(1);
            DateTime fechaManana = DateTime.Now;
            return await context.Visitas.Include(x => x.Area).Include(x => x.Usuario).Where(x => x.FechaVisita>= fechaManana).ToListAsync();
        }

        [HttpGet("historico")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Visita>>> GetVisitasHistorico()
        {
            DateTime fechaHoy = DateTime.Now;
            return await context.Visitas.Include(x => x.Area).Include(x => x.Usuario).Where(x => x.FechaVisita.DayOfYear <= fechaHoy.DayOfYear && x.FechaVisita.Month <= fechaHoy.Month && x.FechaVisita.Year <= fechaHoy.Year).ToListAsync();
        }



        [HttpDelete("{idVisita:int}")]
        public async Task<ActionResult> Delete(int idVisita)
        {
            var visitaBuscada = await context.Visitas.FirstOrDefaultAsync(x => x.Id == idVisita);
            if (visitaBuscada ==null)
            {
                return BadRequest($"No existe la visita de Id: {idVisita}");
            }

            context.Remove(visitaBuscada);
            await context.SaveChangesAsync();
            return Ok();
        }


    }
}
