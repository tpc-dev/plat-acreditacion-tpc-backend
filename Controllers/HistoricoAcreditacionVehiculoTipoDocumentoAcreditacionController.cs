using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/historico-acreditacion-vehiculo-documento")]
    public class HistoricoAcreditacionVehiculoTipoDocumentoAcreditacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HistoricoAcreditacionVehiculoTipoDocumentoAcreditacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion>>> Get()
        {
            return await context.HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion>> Get(int id)
        {
            var historico = await context.HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion.FirstOrDefaultAsync(x => x.Id == id);
            if (historico == null)
            {
                return NotFound();
            }

            return historico;
        }

        [HttpPost]
        public async Task<ActionResult> Post(HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion historico)
        {
            historico.Fecha = DateTime.Now;
            context.Add(historico);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{vehiculoTipoDocumentoId}/historico")]
        public async Task<ActionResult<List<HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion>>> GetHistoricoContratoTipoDocumento(int vehiculoTipoDocumentoId)
        {
            var historico = await context.VehiculoTiposDocumentosAcreditacion.FirstOrDefaultAsync(x => x.Id == vehiculoTipoDocumentoId);
            if (historico == null)
            {
                return NotFound();
            }

            return await context.HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion
                .Where(h => h.VehiculoTipoDocumentoAcreditacionId == vehiculoTipoDocumentoId)
                .Include(h => h.EstadoAcreditacion)
                .OrderByDescending(h => h.Fecha)
                .Take(5)
                .ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion historico, int id)
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
            bool existe = await context.HistoricosAcreditacionVehiculoTipoDocumentoAcreditacion.AnyAsync(historico => historico.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
