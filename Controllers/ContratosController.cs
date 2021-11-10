using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

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
            return await context.Contratos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Contrato contrato)
        {
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
