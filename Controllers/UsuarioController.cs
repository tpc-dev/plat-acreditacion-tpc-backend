using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuarioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await context.Usuarios.Include(x => x.TipoRol).ToListAsync();
        }

        [HttpGet("tiporol/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Usuario>>> GetUsuariosPorRol(int id)
        {
            var existeRol = await context.TipoRoles.AnyAsync(x => x.Id == id);
            if (!existeRol)
            {
                return NotFound("El id de rol no existe");
            }

            return await context.Usuarios.Where(x => x.TipoRolId == id).ToListAsync();
        }

        [HttpGet("{id:int}/empresa")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Empresa>> GetEmpresaUsuario(int id)
        {
            var existeUsuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (existeUsuario == null)
            {
                return NotFound("El id de usuario no existe");
            }

            return await context.Empresas.Include(x=>x.EstadoAcreditacion).Where(x => x.Id == existeUsuario.EmpresaId).FirstOrDefaultAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            return await context.Usuarios.Include(x => x.TipoRol).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(NuevoUsuarioDTO nuevoUsuarioDTO)
        {
            var existeRol = await context.TipoRoles.AnyAsync(x => x.Id == nuevoUsuarioDTO.TipoRolId);

            if (!existeRol)
            {
                return BadRequest($"No existe el rol de Id: {nuevoUsuarioDTO.TipoRolId}");
            }

            context.Add(nuevoUsuarioDTO);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Usuario usuario, int id)
        {
            if (usuario.Id != id)
            {
                return BadRequest("El id del usuario no coincide con el id de la URL");
            }

            bool existe = await context.Usuarios.AnyAsync(usuario => usuario.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(usuario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Usuarios.AnyAsync(usuario => usuario.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Usuario() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
