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
    [Route("api/etapa-creacion-contrato")]
    public class EtapaCreacionContratoController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EtapaCreacionContratoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EtapaCreacionContrato>>> Get()
        {
            return await context.EtapasCreacionContrato.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EtapaCreacionContrato>> Get(int id)
        {
            var tipoRol = await context.EtapasCreacionContrato.FirstOrDefaultAsync(x => x.Id == id);
            if (tipoRol == null)
            {
                return NotFound();
            }

            return tipoRol;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EtapaCreacionContrato etapaCreacionContrato)
        {
           // var nuevoTipoRolMapped = mapper.Map<TipoRol>(nuevoTipoRolDTO);
            context.Add(etapaCreacionContrato);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(EtapaCreacionContrato etapaCreacionContrato, int id)
        {
            if (etapaCreacionContrato.Id != id)
            {
                return BadRequest("El id del etapaCreacionContrato no coincide con el id de la URL");
            }

            bool existe = await context.EtapasCreacionContrato.AnyAsync(etapaCreacionContrato => etapaCreacionContrato.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(etapaCreacionContrato);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.EtapasCreacionContrato.AnyAsync(etapaCreacionContrato => etapaCreacionContrato.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new EtapaCreacionContrato() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
