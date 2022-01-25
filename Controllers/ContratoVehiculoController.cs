using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/contrato-vehiculo")]
    public class ContratoVehiculoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ContratoVehiculoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ContratoVehiculo>>> Get()
        {
            return await context.ContratosVehiculos
                .Include(ct => ct.Vehiculo)
                .ThenInclude(v => v.Chofer)
                .Include(ct => ct.Contrato)
                .Where(ct => ct.Contrato.TerminoContrato > DateTime.Now)
                //.Include(ct => ct.RegistrosAccesosTrabajadorContrato.Where(registro=> registro.ContratoTrabajadorTrabajadorId == ct.TrabajadorId && registro.ContratoTrabajadorContratoId == ct.ContratoId))
                .ToListAsync();
        }


        [HttpGet("acreditados")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ContratoVehiculo>>> GetVehiculosEnContratosAcreditados()
        {
            return await context.ContratosVehiculos
                .Include(ct => ct.Vehiculo)
                .Include(ct => ct.Vehiculo.Chofer)
                .Include(ct => ct.Contrato)
                .Where(ct => ct.Contrato.TerminoContrato > DateTime.Now && ct.Contrato.EstadoAcreditacionId == 1)
                //.Include(ct => ct.RegistrosAccesosTrabajadorContrato.Where(registro=> registro.ContratoTrabajadorTrabajadorId == ct.TrabajadorId && registro.ContratoTrabajadorContratoId == ct.ContratoId))
                .ToListAsync();
        }

        [HttpGet("{contratoId}/{vehiculoId}/documentos-creados")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<VehiculoTipoDocumentoAcreditacion>>> GetDocumentosVehiculosEnContratosAcreditados(int contratoId,int vehiculoId)
        {
            return await context.VehiculoTiposDocumentosAcreditacion
                .Include(ct => ct.TipoDocumentoAcreditacion)
                .Where(ct => ct.ContratoVehiculoContratoId == contratoId && ct.ContratoVehiculoVehiculoId == vehiculoId)
                //.Include(ct => ct.RegistrosAccesosTrabajadorContrato.Where(registro=> registro.ContratoTrabajadorTrabajadorId == ct.TrabajadorId && registro.ContratoTrabajadorContratoId == ct.ContratoId))
                .ToListAsync();
        }

        [HttpPost("registro-acceso")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<RegistroAccesoVehiculoContrato>>> GetRegistroAccesos(ContratoVehiculo contratoVehiculo)
        {
            return await context.RegistroAccesosVehiculosContrato
                .OrderByDescending(r => r.FechaEvento)
                .ToListAsync();
        }

        [HttpPost("registro-acceso/{tipo}")]
        public async Task<ActionResult> PostRegistroAcceso(ContratoVehiculo contratoVehiculo, string tipo)
        {

            RegistroAccesoVehiculoContrato registroAccesoVehiculoContrato = new RegistroAccesoVehiculoContrato
            {
                TipoEvento = tipo,
                FechaEvento = DateTime.Now,
                ContratoVehiculoContratoId = contratoVehiculo.ContratoId,
                ContratoVehiculoVehiculoId = contratoVehiculo.VehiculoId
            };

            context.Add(registroAccesoVehiculoContrato);
            await context.SaveChangesAsync();
            return Ok(registroAccesoVehiculoContrato);
        }


        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Chofer>> Get(int id)
        //{
        //    var chofer = await context.Choferes.FirstOrDefaultAsync(x => x.Id == id);
        //    if (chofer == null)
        //    {
        //        return NotFound();
        //    }

        //    return chofer;
        //}

        //[HttpPost]
        //public async Task<ActionResult> Post(Chofer chofer)
        //{
        //    context.Add(chofer);
        //    await context.SaveChangesAsync();
        //    return Ok(chofer);
        //}

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(Chofer chofer, int id)
        //{
        //    if (chofer.Id != id)
        //    {
        //        return BadRequest("El id del Choder no coincide con el id de la URL");
        //    }

        //    bool existe = await context.Choferes.AnyAsync(v => v.Id == id);
        //    if (!existe)
        //    {
        //        return NotFound();
        //    }

        //    context.Update(chofer);
        //    await context.SaveChangesAsync();
        //    return Ok(chofer);
        //}

        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    bool existe = await context.Choferes.AnyAsync(v => v.Id == id);
        //    if (!existe)
        //    {
        //        return NotFound();
        //    }

        //    context.Remove(new Chofer() { Id = id });
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}
