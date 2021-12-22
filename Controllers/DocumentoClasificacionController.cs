using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/documentos-acreditacion")]
    public class DocumentoClasificacionController :ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public DocumentoClasificacionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<DocumentoClasificacion>>> Get()
        {
            return await context.DocumentosClasificacion.ToListAsync();
        }

        [HttpGet("activos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<DocumentoClasificacion>>> GetActivos()
        {
            return await context.DocumentosClasificacion.Where(tipoDocumento => tipoDocumento.Activo == true).ToListAsync();
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
        public async Task<ActionResult> Post(NuevoDocumentoClasificacionDTO nuevoDocumentoClasificacionDTO)
        {
            var nuevoDocumentoClasificacionMapped = mapper.Map<DocumentoClasificacion>(nuevoDocumentoClasificacionDTO);
            context.Add(nuevoDocumentoClasificacionMapped);
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
