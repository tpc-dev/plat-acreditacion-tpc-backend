using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/trabajadores-tpc")]
    public class TrabajadorTPCController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TrabajadorTPCController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TrabajadorTPC>>> Get()
        {
            return await context.TrabajadoresTPC
                .Include(t => t.Gerencia)
                .ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrabajadorTPC>> Get(int id)
        {
            var trabajador = await context.TrabajadoresTPC.FirstOrDefaultAsync(x => x.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return trabajador;
        }

        [HttpPost]
        public async Task<ActionResult> Post(NuevoTrabajadorTPCDTO nuevoTrabajadorTPCDTO)
        {
            bool existePais = await context.Paises.AnyAsync(p => p.Id == nuevoTrabajadorTPCDTO.PaisId);

            if (!existePais)
            {
                return NotFound("Pais no encontrado");
            }

            bool existeNivelEducacional = await context.NivelesEducacional.AnyAsync(n => n.Id == nuevoTrabajadorTPCDTO.NivelEducacionalId);

            if (!existeNivelEducacional)
            {
                return NotFound("Nivel Educacional no encontrado");
            }

            bool existeEstadoCivil = await context.EstadosCivil.AnyAsync(e => e.Id == nuevoTrabajadorTPCDTO.EstadoCivilId);

            if (!existeEstadoCivil)
            {
                return NotFound("Estado Civil no encontrado");
            }

            bool existeGerencia = await context.Gerencias.AnyAsync(g => g.Id == nuevoTrabajadorTPCDTO.GerenciaId);

            if (!existeGerencia)
            {
                return NotFound("Gerencia no encontrado");
            }

            bool existeGenero = await context.Generos.AnyAsync(g => g.Id == nuevoTrabajadorTPCDTO.GeneroId);

            if (!existeGenero)
            {
                return NotFound("Genero no encontrado");
            }

            bool existeTrabajadorRut =  await context.TrabajadoresTPC.AnyAsync(g => g.Rut == nuevoTrabajadorTPCDTO.Rut);

            if (existeTrabajadorRut)
            {
                return NotFound("Ya hay un trabajador con este RUT");
            }

            var nuevo = mapper.Map<TrabajadorTPC>(nuevoTrabajadorTPCDTO);
            context.Add(nuevo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TrabajadorTPC trabajador, int id)
        {
            if (trabajador.Id != id)
            {
                return BadRequest("El id del trabajador no coincide con el id de la URL");
            }

            bool existe = await context.TrabajadoresTPC.AnyAsync(pais => pais.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(trabajador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.TrabajadoresTPC.AnyAsync(t => t.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TrabajadorTPC() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
