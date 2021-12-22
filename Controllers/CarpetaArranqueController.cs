using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/carpeta-arranque")]
    public class CarpetaArranqueController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CarpetaArranqueController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<CarpetaArranque>>> Get()
        {
            return await context.CarpetasArranques.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarpetaArranque>> Get(int id)
        {
            var carpetaArranque = await context.CarpetasArranques.FirstOrDefaultAsync(x => x.Id == id);
            if (carpetaArranque == null)
            {
                return NotFound();
            }

            return carpetaArranque;
        }
        
        [HttpGet("{id:int}/items")]
        public async Task<ActionResult<List<ItemCarpetaArranqueCarpetaArranque>>> GetItemsPorCarpetaArranqueId(int id)
        {
            bool existe = await context.CarpetasArranques.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var itemCarpetaArranques = await context.ItemsCarpetasArranqueCarpetasArranque
            return  await context.ItemsCarpetasArranqueCarpetasArranque
                .Where(x => x.CarpetaArranqueId == id)
                .Include(x=> x.ItemCarpetaArranque.TiposDocumentosAcreditacion)
                .ToListAsync();

            //List<ItemCarpetaArranque> itemCA = new List<ItemCarpetaArranque>();
            //itemCarpetaArranques.ForEach(async x =>
            //{
            //    ItemCarpetaArranque newItem = await context.ItemsCarpetaArranque.Include(item=>item.TiposDocumentosAcreditacion).FirstOrDefaultAsync();
            //    itemCA.Add(newItem);
            //});
            ////await context.ItemsCarpetasArranqueCarpetasArranque
            ////.Where(x => x.CarpetaArranqueId == id)
            ////.Include(item => item.ItemCarpetaArranque)
            ////.ThenInclude(itemca => itemca.TiposDocumentosAcreditacion)
            ////.ToListAsync();
            //return itemCA;
        }

        [HttpPost]
        public async Task<ActionResult> Post(NuevoCarpetaArranqueDTO nuevoCarpetaArranqueDTO)
        {   
            // TODO VERIFICAR QUE NO EXISTE UNA CARPETA DE ARRANQUE CON ESTE CONTRATO ID

            var existe = await context.CarpetasArranques.AnyAsync(x => x.ContratoId == nuevoCarpetaArranqueDTO.ContratoId));

            if (existe)
            {
                return BadRequest("Ya existe una carpeta de arranque asociada a ese contrato ");
            }

            nuevoCarpetaArranqueDTO.FechaInicio = DateTime.Now;
            nuevoCarpetaArranqueDTO.CreatedAt   = DateTime.Now;
            nuevoCarpetaArranqueDTO.UpdatedAt   = DateTime.Now;

            var nuevoCarpetaArranqueMapped = mapper.Map<CarpetaArranque>(nuevoCarpetaArranqueDTO);
            context.Add(nuevoCarpetaArranqueMapped);
            await context.SaveChangesAsync();
            return Ok(nuevoCarpetaArranqueMapped);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CarpetaArranque carpetaArranque, int id)
        {
            if (carpetaArranque.Id != id)
            {
                return BadRequest("El id del carpeta arranque no coincide con el id de la URL");
            }

            bool existe = await context.CarpetasArranques.AnyAsync(ca => ca.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(carpetaArranque);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.CarpetasArranques.AnyAsync(ca => ca.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new CarpetaArranque() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
