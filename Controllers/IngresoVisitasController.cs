using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/ingreso-visitas")]
    public class IngresoVisitasController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public IngresoVisitasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<IngresoVisitas>>> Get()
        {
            return await context.IngresosVisitas.ToListAsync();
        }

        //visitas/${visitaid}/ingresos-historico

        [HttpGet("{visitaid}/ingresos-historico")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<IngresoVisitas>>> GetIngresosHistoricos(int visitaid)
        {
            return await context.IngresosVisitas.Where(ingresoVisita => ingresoVisita.VisitaId == visitaid).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IngresoVisitas>> Get(int id)
        {
            var registroIngresoVisita = await context.IngresosVisitas.FirstOrDefaultAsync(x => x.Id == id);
            if (registroIngresoVisita == null)
            {
                return NotFound();
            }

            return registroIngresoVisita;
        }

        [HttpPost]
        public async Task<ActionResult> Post(IngresoVisitaDTO ingresoVisitaDTO)
        {
            var existeVisita = await context.Visitas.AnyAsync(x => x.Id == ingresoVisitaDTO.VisitaId);
            if (!existeVisita)
            {
                return NotFound("El id de visita no existe");
            }

            var visitaMapead = mapper.Map<IngresoVisitas>(ingresoVisitaDTO);
            context.Add(visitaMapead);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(IngresoVisitas ingresoVisita, int id)
        {
            if (ingresoVisita.Id != id)
            {
                return BadRequest("El id del ingresoVisita no coincide con el id de la URL");
            }

            bool existe = await context.IngresosVisitas.AnyAsync(ingresoVisita => ingresoVisita.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(ingresoVisita);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.IngresosVisitas.AnyAsync(ingresoVisita => ingresoVisita.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new IngresoVisitas() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
