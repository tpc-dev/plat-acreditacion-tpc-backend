using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/historico-acreditacion-empresa-documento")]
    public class HistoricoAcreditacionEmpresaTipoDocumentoAcreditacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HistoricoAcreditacionEmpresaTipoDocumentoAcreditacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion>>> Get()
        {
            return await context.HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion>> Get(int id)
        {
            var historico = await context.HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion.FirstOrDefaultAsync(x => x.Id == id);
            if (historico == null)
            {
                return NotFound();
            }

            return historico;
        }

        [HttpPost]
        public async Task<ActionResult> Post(HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion historico)
        {
            historico.Fecha = DateTime.Now;
            context.Add(historico);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{empresaTipoDocumentoId}/historico")]
        public async Task<ActionResult<List<HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion>>> GetHistoricoContratoTipoDocumento(int empresaTipoDocumentoId)
        {
            var historico = await context.EmpresaTiposDocumentosAcreditacion.FirstOrDefaultAsync(x => x.Id == empresaTipoDocumentoId);
            if (historico == null)
            {
                return NotFound();
            }

            return await context.HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion
                .Where(h => h.EmpresaTipoDocumentoAcreditacionId == empresaTipoDocumentoId)
                .Include(h => h.EstadoAcreditacion)
                .OrderByDescending(h => h.Fecha)
                .Take(5)
                .ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion historico, int id)
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
            bool existe = await context.HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion.AnyAsync(historico => historico.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
