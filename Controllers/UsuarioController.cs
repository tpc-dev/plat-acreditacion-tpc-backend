using AutoMapper;
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
        private readonly IMapper mapper;


        public UsuarioController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await context.Usuarios.Include(x => x.TipoRol).ToListAsync();
        }

        [HttpGet("tiporol/{id:int}")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Usuario>>> GetUsuariosPorRol(int id)
        {
            var existeRol = await context.TipoRoles.AnyAsync(x => x.Id == id);
            if (!existeRol)
            {
                return NotFound("El id de rol no existe");
            }

            return await context.Usuarios.Include(x => x.TipoRol).Where(x => x.TipoRolId == id).ToListAsync();
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

            return await context.Empresas.Where(x => x.Id == existeUsuario.EmpresaId).FirstOrDefaultAsync();
        }


        //[HttpGet("tipo-rol/{tipoRol:int}")]
        //public async Task<ActionResult<Usuario>> GetUsuarioPorTipoRol(int tipoRol)
        //{
        //    return await context.Usuarios.Include(x => x.TipoRol).FirstOrDefaultAsync(x => x.TipoRolId == tipoRol);
        //}


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            return await context.Usuarios.Include(x => x.TipoRol).FirstOrDefaultAsync(x => x.Id == id);
        } 
        
        [HttpGet("{id:int}/contratos")]
        public async Task<ActionResult<List<ContratoUsuario>>> GetContratosUsuarioByUsuarioId(int id)
        {
            bool existe = await context.Usuarios.AnyAsync(x =>  x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            return await context.ContratosUsuarios.Include(x=>x.Contrato).Where(x => x.UsuarioId == id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(NuevoUsuarioDTO nuevoUsuarioDTO)
        {
            var existeRol = await context.TipoRoles.AnyAsync(x => x.Id == nuevoUsuarioDTO.TipoRolId);

            if (!existeRol)
            {
                return BadRequest($"No existe el rol de Id: {nuevoUsuarioDTO.TipoRolId}");
            }

            var nuevoUsuario = mapper.Map<Usuario>(nuevoUsuarioDTO);

            context.Add(nuevoUsuario);
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

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> CambiarEstado(int id, int id)
        //{

        //    bool existe = await context.Usuarios.AnyAsync(usuario => usuario.Id == id);
        //    if (!existe)
        //    {
        //        return NotFound();
        //    }

        //    //if (usuario.Id != id)
        //    //{
        //    //    return BadRequest("El id del usuario no coincide con el id de la URL");
        //    //}
       

        //    context.Update(usuario);
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}

        /* 
         * Actualizar datos usuario desde la plataforma web
         */
        [HttpPut("usuarioplataforma/{idUsuario:int}")]
        public async Task<ActionResult> ActualizarUsuario(ActualizarUsuarioPlataformaDTO actualizarUsuarioPlataformaDTO, int idUsuario)
        {
            if (actualizarUsuarioPlataformaDTO.Id != idUsuario)
            {
                return BadRequest("El id del usuario no coincide con el id de la URL");
            }


            var usuarioBuscado = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == idUsuario);

            if (usuarioBuscado == null)
            {
                return NotFound();
            }

            usuarioBuscado.Nombre = actualizarUsuarioPlataformaDTO.Nombre;
            usuarioBuscado.Apellido1 = actualizarUsuarioPlataformaDTO.Apellido1;
            usuarioBuscado.Apellido2= actualizarUsuarioPlataformaDTO.Apellido2;
            usuarioBuscado.Telefono = actualizarUsuarioPlataformaDTO.Telefono;

            context.Entry(usuarioBuscado).State = EntityState.Modified;

            await context.SaveChangesAsync();

            Usuario usuario = await context.Usuarios.Include(x => x.TipoRol).Include(x => x.Empresa).FirstOrDefaultAsync(x => x.Id == idUsuario);


            return Ok(usuario);
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
