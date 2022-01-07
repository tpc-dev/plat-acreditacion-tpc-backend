using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/historico-acreditacion-contrato-documento")]
    public class HistoricoAcreditacionContratoTipoDocumentoAcreditacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HistoricoAcreditacionContratoTipoDocumentoAcreditacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<HistoricoAcreditacionContratoTipoDocumentoAcreditacion>>> Get()
        {
            return await context.HistoricosAcreditacionContratoTipoDocumentoAcreditacion.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistoricoAcreditacionContratoTipoDocumentoAcreditacion>> Get(int id)
        {
            var historico = await context.HistoricosAcreditacionContratoTipoDocumentoAcreditacion.FirstOrDefaultAsync(x => x.Id == id);
            if (historico == null)
            {
                return NotFound();
            }

            return historico;
        }

        [HttpPost]
        public async Task<ActionResult> Post(HistoricoAcreditacionContratoTipoDocumentoAcreditacion historico)
        {
            historico.Fecha = DateTime.Now;
            context.Add(historico);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{contratoTipoDocumentoId}/historico")]
        public async Task<ActionResult<List<HistoricoAcreditacionContratoTipoDocumentoAcreditacion>>> GetHistoricoContratoTipoDocumento(int contratoTipoDocumentoId)
        {
            var historico = await context.ContratoTiposDocumentoAcreditacion.FirstOrDefaultAsync(x => x.Id == contratoTipoDocumentoId);
            if (historico == null)
            {
                return NotFound();
            }

            return await context.HistoricosAcreditacionContratoTipoDocumentoAcreditacion
                .Where(h => h.ContratoTipoDocumentoAcreditacionId == contratoTipoDocumentoId)
                .Include(h => h.EstadoAcreditacion)
                .OrderByDescending(h => h.Fecha)
                .ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(HistoricoAcreditacionContratoTipoDocumentoAcreditacion historico, int id)
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
            bool existe = await context.HistoricosAcreditacionContratoTipoDocumentoAcreditacion.AnyAsync(historico => historico.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new HistoricoAcreditacionContratoTipoDocumentoAcreditacion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
