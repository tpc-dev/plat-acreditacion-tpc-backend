using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/contrato-trabajador")]
    public class ContratoTrabajadorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ContratoTrabajadorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ContratoTrabajador>>> Get()
        {
            return await context.ContratosTrabajadores
                .Include(ct => ct.Trabajador)
                .Include(ct => ct.Contrato)
                .Where(ct => ct.Contrato.TerminoContrato > DateTime.Now)
                //.Include(ct => ct.RegistrosAccesosTrabajadorContrato.Where(registro=> registro.ContratoTrabajadorTrabajadorId == ct.TrabajadorId && registro.ContratoTrabajadorContratoId == ct.ContratoId))
                .ToListAsync();
        }

        [HttpPost("registro-acceso")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<RegistroAccesoTrabajadorContrato>>> GetRegistroAccesos(ContratoTrabajador contratoTrabajador)
        {
            return await context.RegistroAccesosTrabajadoresContrato
                .OrderByDescending(r => r.FechaEvento)
                .ToListAsync();
        }

        [HttpPost("registro-acceso/{tipo}")]
        public async Task<ActionResult> PostRegistroAcceso(ContratoTrabajador contratoTrabajador, string tipo)
        {

            RegistroAccesoTrabajadorContrato registroAccesoTrabajadorContrato = new RegistroAccesoTrabajadorContrato
            {
                TipoEvento = tipo,
                FechaEvento = DateTime.Now,
                ContratoTrabajadorContratoId = contratoTrabajador.ContratoId,
                ContratoTrabajadorTrabajadorId = contratoTrabajador.TrabajadorId
            };

            context.Add(registroAccesoTrabajadorContrato);
            await context.SaveChangesAsync();
            return Ok(registroAccesoTrabajadorContrato);
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
