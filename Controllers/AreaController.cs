using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreaController: ControllerBase
    {
       
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AreaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Area>>> Get()
        {
            return await context.Areas.ToListAsync();
        }

        [HttpGet("activos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Area>>> GetAreasActivas()
        {
            return await context.Areas.Where(area => area.Activo == true).ToListAsync();
        }   

        [HttpPost]
        public async Task<ActionResult> Post(NuevaAreaDTO nuevaAreaDTO)
        {
            var nuevaAreaDTOMapped = mapper.Map<Area>(nuevaAreaDTO);
            context.Add(nuevaAreaDTOMapped);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Area area, int id)
        {
            if (area.Id != id)
            {
                return BadRequest("El id del area no coincide con el id de la URL");
            }

            bool existe = await context.Areas.AnyAsync(area => area.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(area);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Areas.AnyAsync(area => area.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Area() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
