using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/historico-acreditacion-trabajador-documento")]
    public class HistoricoAcreditacioTrabajadorTipoDocumentoAcreditacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HistoricoAcreditacioTrabajadorTipoDocumentoAcreditacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion>>> Get()
        {
            return await context.HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion>> Get(int id)
        {
            var historico = await context.HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion.FirstOrDefaultAsync(x => x.Id == id);
            if (historico == null)
            {
                return NotFound();
            }

            return historico;
        }

        [HttpPost]
        public async Task<ActionResult> Post(HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion historico)
        {
            historico.Fecha = DateTime.Now;
            context.Add(historico);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{trabajadorTipoDocumentoId}/historico")]
        public async Task<ActionResult<List<HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion>>> GetHistoricoContratoTipoDocumento(int trabajadorTipoDocumentoId)
        {
            var historico = await context.TrabajadorTiposDocumentoAcreditacion.FirstOrDefaultAsync(x => x.Id == trabajadorTipoDocumentoId);
            if (historico == null)
            {
                return NotFound();
            }

            return await context.HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion
                .Where(h => h.TrabajadorTipoDocumentoAcreditacionId == trabajadorTipoDocumentoId)
                .Include(h => h.EstadoAcreditacion)
                .OrderByDescending(h => h.Fecha)
                .Take(5)
                .ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion historico, int id)
        {
            if (historico.Id != id)
            {
                return BadRequest("El id del historico no coincide con el id de la URL");
            }

            bool existe = await context.Paises.AnyAsync(historico => historico.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(historico);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.HistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion.AnyAsync(historico => historico.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
