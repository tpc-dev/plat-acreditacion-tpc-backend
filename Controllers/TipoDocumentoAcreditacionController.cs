using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/tipo-documento-acreditacion")]
    public class TipoDocumentoAcreditacionController : ControllerBase 
    {
        private readonly ApplicationDbContext context;
        public TipoDocumentoAcreditacionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TipoDocumentoAcreditacion>>> Get()
        {
            return await context.TiposDocumentosAcreditacion.ToListAsync();
        }

        // TODO verificar rol asignado al token 

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoDocumentoAcreditacion>> Get(int id)
        {
            var tipoDocumentoAcreditacion = await context.TiposDocumentosAcreditacion.FirstOrDefaultAsync(x => x.Id == id);
            if (tipoDocumentoAcreditacion == null)
            {
                return NotFound();
            }

            return tipoDocumentoAcreditacion;
        } 
        
        [HttpGet("item-carpeta-arranque/{id:int}")]
        public async Task<ActionResult<List<TipoDocumentoAcreditacion>>> GetTipoDocumentosPorItemCarpetaArranque(int id)
        {
            bool existe = await context.ItemsCarpetaArranque.AnyAsync(item => item.Id == id);
            if (!existe)
            {
                return NotFound($"No existe item de la carpeta de arranque con id {id}");
            }
            

            return await context.TiposDocumentosAcreditacion.Include(tipodoc=> tipodoc.DocumentoClasificacion).Where(x=> x.ItemCarpetaArranqueId == id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(TipoDocumentoAcreditacion tipoDocumentoAcreditacion)
        {
            bool existeItemCarpetArranque = await context.ItemsCarpetaArranque.AnyAsync(itemCarpetaArranque => itemCarpetaArranque.Id == tipoDocumentoAcreditacion.ItemCarpetaArranqueId);
            if (!existeItemCarpetArranque)
            {
                return NotFound($"No existe un item en la carpeta de arranque con id {tipoDocumentoAcreditacion.ItemCarpetaArranqueId}");
            }
            context.Add(tipoDocumentoAcreditacion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TipoDocumentoAcreditacion tipoDocumentoAcreditacion, int id)
        {
            if (tipoDocumentoAcreditacion.Id != id)
            {
                return BadRequest("El id del tipo de rol no coincide con el id de la URL");
            }

            bool existe = await context.TiposDocumentosAcreditacion.AnyAsync(tipoDocumentoAcreditacion => tipoDocumentoAcreditacion.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(tipoDocumentoAcreditacion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.TiposDocumentosAcreditacion.AnyAsync(tipoDocumentoAcreditacion => tipoDocumentoAcreditacion.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TipoDocumentoAcreditacion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
