using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/documentos-acreditacion")]
    public class DocumentoClasificacionController :ControllerBase
    {
        private readonly ApplicationDbContext context;
        public DocumentoClasificacionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<DocumentoClasificacion>>> Get()
        {
            return await context.DocumentosClasificacion.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<DocumentoClasificacion>> Get(int id)
        {
            var documentoClasificacion = await context.DocumentosClasificacion.FirstOrDefaultAsync(x => x.Id == id);
            if (documentoClasificacion == null)
            {
                return NotFound();
            }

            return documentoClasificacion;
        }

        //[HttpGet("item-carpeta-arranque/{id:int}")]
        //public async Task<ActionResult<List<DocumentoClasificacion>>> GetTipoDocumentosPorItemCarpetaArranque(int id)
        //{
        //    bool existe = await context.ItemsCarpetaArranque.AnyAsync(item => item.Id == id);
        //    if (!existe)
        //    {
        //        return NotFound($"No existe item de la carpeta de arranque con id {id}");
        //    }


        //    return await context.DocumentosClasificacion.Where(x => x.documentoClasificacion == id).ToListAsync();
        //}

        [HttpPost]
        public async Task<ActionResult> Post(DocumentoClasificacion documentoClasificacion)
        {
            context.Add(documentoClasificacion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(DocumentoClasificacion documentoClasificacion, int id)
        {
            if (documentoClasificacion.Id != id)
            {
                return BadRequest("El id del documentoClasificacion  no coincide con el id de la URL");
            }

            bool existe = await context.DocumentosClasificacion.AnyAsync(documentoClasificacion => documentoClasificacion.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(documentoClasificacion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.DocumentosClasificacion.AnyAsync(documentoClasificacion => documentoClasificacion.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new DocumentoClasificacion() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
