using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/item-carpeta-arranque")]
    public class ItemCarpetaArranqueController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ItemCarpetaArranqueController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ItemCarpetaArranque>>> Get()
        {
            return await context.ItemsCarpetaArranque.Include(itemCarpetaArranque => itemCarpetaArranque.TiposDocumentosAcreditacion).ToListAsync();
        }

        // TODO verificar rol asignado al token 

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemCarpetaArranque>> Get(int id)
        {
            var itemCarpetaArranque = await context.ItemsCarpetaArranque.FirstOrDefaultAsync(x => x.Id == id);
            if (itemCarpetaArranque == null)
            {
                return NotFound();
            }

            return itemCarpetaArranque;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ItemCarpetaArranque itemCarpetaArranque)
        {
            context.Add(itemCarpetaArranque);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ItemCarpetaArranque itemCarpetaArranque, int id)
        {
            if (itemCarpetaArranque.Id != id)
            {
                return BadRequest("El id del tipo de rol no coincide con el id de la URL");
            }

            bool existe = await context.ItemsCarpetaArranque.AnyAsync(itemCarpetaArranque => itemCarpetaArranque.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(itemCarpetaArranque);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.ItemsCarpetaArranque.AnyAsync(itemCarpetaArranque => itemCarpetaArranque.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new ItemCarpetaArranque() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
