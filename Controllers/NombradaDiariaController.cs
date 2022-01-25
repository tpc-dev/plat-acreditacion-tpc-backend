using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;
using System.Text.Json.Nodes;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/nombrada-diaria")]
    public class NombradaDiariaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public NombradaDiariaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<NombradaDiaria>>> Get()
        {
            return await context.NombradasDiaria
                .ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<NombradaDiaria>> Get(int id)
        {
            var nombrada = await context.NombradasDiaria.FirstOrDefaultAsync(x => x.Id == id);
            if (nombrada == null)
            {
                return NotFound();
            }

            return nombrada;
        }

        [HttpGet("encargado/{id:int}")]
        public async Task<ActionResult<List<NombradaDiaria>>> GetNombradasPorUsuarioId(int id)
        {
            bool existeUsuario = await context.Usuarios.AnyAsync(u => u.Id == id);

            if (!existeUsuario)
            {
                return NotFound("Usuario no encontrado");
            }

            return await context.NombradasDiaria
                .Where(x => x.UsuarioId == id)
                .OrderByDescending(x => x.Fecha)
                .ToListAsync();
        }

        [HttpGet("{id:int}/trabajadores")]
        public async Task<ActionResult<List<NombradaDiariaTrabajadorFrecuente>>> GetTrabajadoresPorNombrada(int id)
        {
            bool existeNombrada = await context.NombradasDiaria.AnyAsync(n => n.Id == id);

            if (!existeNombrada)
            {
                return NotFound("Nombrada no encontrado");
            }

            return await context.NombradasDiariasTrabajadoresFrecuente
                    .Include(n=>n.TrabajadorFrecuente) 
                    .Where(x => x.NombradaDiariaId == id)
                    .ToListAsync();
        }
        
        [HttpGet("hoy")]
        public async Task<ActionResult<List<NombradaDiariaTrabajadorFrecuente>>> GetTrabajadoresPorNombradaHoy()
        {
            return await context.NombradasDiariasTrabajadoresFrecuente
                    .Include(n=>n.TrabajadorFrecuente) 
                    .Where(x => x.NombradaDiaria.Fecha.DayOfYear == DateTime.Now.DayOfYear && x.NombradaDiaria.Fecha.Year == DateTime.Now.Year)
                    .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(NombradaDiaria nombradaDiaria)
        {
            context.Add(nombradaDiaria);
            await context.SaveChangesAsync();
            return Ok(nombradaDiaria);
        }

        //[HttpPost]
        //public async Task<ActionResult> PostIngresoTrabajador(NombradaDiariaTrabajadorFrecuente nombradaDiariaTrabajadorFrecuente)
        //{
        //    context.Add(nombradaDiaria);
        //    await context.SaveChangesAsync();
        //    return Ok(nombradaDiaria);
        //}

        [HttpPost("registro-acceso")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<RegistroAccesoTrabajadorFrecuente>>> GetRegistroAccesos(NombradaDiariaTrabajadorFrecuente nombradaDiariaTrabajadorFrecuente)
        {
            return await context.RegistroAccesosTrabajadoresFrecuente
                .Where(r => r.NombradaDiariaId == nombradaDiariaTrabajadorFrecuente.NombradaDiariaId && r.TrabajadorFrecuenteId == nombradaDiariaTrabajadorFrecuente.TrabajadorFrecuenteId)
                .OrderByDescending(r => r.FechaEvento)
                .Take(5)
                .ToListAsync();
        }

        [HttpPost("registro-acceso/{tipo}")]
        public async Task<ActionResult> PostRegistroAcceso(NombradaDiariaTrabajadorFrecuente nombradaTrabajador, string tipo)
        {

            RegistroAccesoTrabajadorFrecuente registroAccesoTrabajadorNombrada = new RegistroAccesoTrabajadorFrecuente
            {
                TipoEvento = tipo,
                FechaEvento = DateTime.Now,
                NombradaDiariaId = nombradaTrabajador.NombradaDiariaId,
                TrabajadorFrecuenteId = nombradaTrabajador.TrabajadorFrecuenteId
            };

            context.Add(registroAccesoTrabajadorNombrada);
            await context.SaveChangesAsync();
            return Ok(registroAccesoTrabajadorNombrada);
        }


        [HttpPost("trabajador-frecuente")]
        public async Task<ActionResult> PostCrearNombradaDiariaTrabajadorFrecuente(NombradaDiariaTrabajadorFrecuente nombradaDiariaTrabajadorFrecuente)
        {

            context.Add(nombradaDiariaTrabajadorFrecuente);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{nombradaDiariaId:int}/list-trabajador-frecuente")]
        public async Task<ActionResult> PostCrearNombradaDiariaListaTrabajadoresFrecuente(
            //int nombradaDiariaId, JsonObject Trabajadores)
            int nombradaDiariaId, int[] Trabajadores)
        {
            foreach (int id in Trabajadores)
            {
                NombradaDiariaTrabajadorFrecuente nombradaDiariaTrabajadorFrecuente = new NombradaDiariaTrabajadorFrecuente
                {
                    NombradaDiariaId = nombradaDiariaId,
                    TrabajadorFrecuenteId = id
                };

                context.Add(nombradaDiariaTrabajadorFrecuente);
            }

            await context.SaveChangesAsync();
            return Ok(Trabajadores);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(NombradaDiaria nombradaDiaria, int id)
        {
            if (nombradaDiaria.Id != id)
            {
                return BadRequest("El id del nombradaDiaria no coincide con el id de la URL");
            }

            bool existe = await context.NombradasDiaria.AnyAsync(pais => pais.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(nombradaDiaria);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.NombradasDiaria.AnyAsync(t => t.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new NombradaDiaria() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
