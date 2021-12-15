using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;
using System.Net;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/contratos")]
    public class ContratosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ContratosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Contrato>>> Get()
        {
            return await context.Contratos.Include(contrato => contrato.EtapaCreacionContrato).ToListAsync();
        }

        [HttpGet("existe/{codigoContrato}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> GetContratoYaExiste(string codigoContrato)
        {

            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.CodigoContrato == codigoContrato);
            return existe;
        } 
        
        [HttpGet("paso-uno-completado")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> GetContratoPasoUnoCompletado(string codigoContrato)
        {   


            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.CodigoContrato == codigoContrato);
            return existe;
        }
        
        [HttpGet("paso-dos-completado")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> GetContratoPasoDosCompletado(string codigoContrato)
        {

            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.CodigoContrato == codigoContrato);
            return existe;
        }
        
        [HttpGet("paso-tres-completado")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> GetContratoPasoTresCompletado(string codigoContrato)
        {

            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.CodigoContrato == codigoContrato);
            return existe;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Contrato contrato)
        {

            //EmpresaContrato empresaContrato = new EmpresaContrato
            //{
            //    ContratoId = contrato.Id,
            //    EmpresaId  = contrato.
                
            //};

            context.Add(contrato);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Contrato contrato, int id)
        {
            if (contrato.Id != id)
            {
                return BadRequest("El id del contrato no coincide con el id de la URL");
            }

            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(contrato);
            await context.SaveChangesAsync();
            return Ok();
        } 
        
        [HttpPut("cambiar-etapa-creacion/{id:int}")]
        public async Task<ActionResult> CambiarEtapaCreacion(int idEtapa, int id)
        {

            var contrato = await context.Contratos.FirstOrDefaultAsync(contratoS => contratoS.Id == id);
            if (contrato == null)
            {
                return NotFound("Contrato No encontrado");
            }

            var etapaCreacion = await context.EtapasCreacionContrato.FirstOrDefaultAsync(etapa => etapa.id == idEtapa);

            if (etapaCreacion == null)
            {
                return NotFound("Orden No encontrada");
            }

            contrato.EtapaCreacionContratoId = idEtapa;
            context.Entry(contrato).State = EntityState.Modified;
            context.Update(contrato);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Contratos.AnyAsync(contrato => contrato.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Contrato() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
