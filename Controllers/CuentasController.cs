using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;
using PlatAcreditacionTPCBackend.Models;
using PlatAcreditacionTPCBackend.Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMailService mailService;
        private readonly IMapper mapper;
        private readonly ISharePointService sharePointService;

        public CuentasController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, IMailService mailService, IMapper mapper, ISharePointService sharePointService)
        {
            this.context = context;
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mailService = mailService;
            this.mapper = mapper;
            this.sharePointService = sharePointService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuario.Email, Email = credencialesUsuario.Email };
            var resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);

            if (resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

        }

        [HttpPost("crear-cuenta")]
        public async Task<ActionResult<RespuestaAutenticacion>> CrearCuenta(NuevoUsuarioDTO nuevoUsuarioDTO)
        {
            var existeEmpresa = await context.Empresas.AnyAsync(empresa => empresa.Id == nuevoUsuarioDTO.EmpresaId);

            if (!existeEmpresa)
            {
                return BadRequest($"No existe una empresa con id {nuevoUsuarioDTO.EmpresaId}");
            }

            var existeRol = await context.TipoRoles.AnyAsync(x => x.Id == nuevoUsuarioDTO.TipoRolId);

            if (!existeRol)
            {
                return BadRequest($"No existe el rol con id{nuevoUsuarioDTO.TipoRolId}");
            }

            var existeUsuarioEmail = await context.Usuarios.AnyAsync(usuario => usuario.Email == nuevoUsuarioDTO.Email);

            if (existeUsuarioEmail)
            {
                return BadRequest("Ya existe un usuario con este correo");
            }

            var existeUsuarioRut = await context.Usuarios.AnyAsync(usuario => usuario.Rut == nuevoUsuarioDTO.Rut);

            if (existeUsuarioRut)
            {
                return BadRequest("Ya existe un usuario con este rut");
            }

            var newUsuario = new IdentityUser { UserName = nuevoUsuarioDTO.Email, Email = nuevoUsuarioDTO.Email };
            var randomPassword = GeneratePassword();
            var resultado = await userManager.CreateAsync(newUsuario, randomPassword);

            var usuarioMapead = mapper.Map<Usuario>(nuevoUsuarioDTO);
            context.Add(usuarioMapead);
            await context.SaveChangesAsync();


            if (resultado.Succeeded)
            {
                MailRequest mailRequest = new MailRequest
                {
                    ToEmail = nuevoUsuarioDTO.Email,
                    Subject = "Bienvenido a la Plataforma de Acreditación TPC",
                    //Body = "Se ha creado un usuario para esta cuenta en la plataforma tpc, puede accedor usando como usuario " +
                    //"este correo " + nuevoUsuarioDTO.Email + " y su contraseña es :" + randomPassword,
                    Body = @"<html>
                                <h1>
                                    Bienvenido a la Plataforma de Acreditacion Acceso TPC
                                </h1>
                                <p>
                                    Has sido invitado a tener acceso a la plataforma. <br>
                                    Tu usuario  es : <strong>" + nuevoUsuarioDTO.Email + @"</strong> <br>
                                    Tu contraseña es : <strong>" + randomPassword + @"</strong>
                                </p>
                                <p>
                                    Saludos.
                                </p>
                            </html>"
                };

                try
                {
                    await mailService.SendEmailAsync(mailRequest);
                    return Ok();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacionLogin>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email, credencialesUsuario.Password,
                isPersistent: false, lockoutOnFailure: false);


            if (resultado.Succeeded)
            {
                return await ConstruirTokenLogin(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }


        [HttpPost("reestablecer-cambiar-password")]
        public async Task<ActionResult> ReestablecerCambiarPassword(ReestablecerPasswordParams reestablecerPasswordParams)
        {
            var user = await userManager.FindByEmailAsync(reestablecerPasswordParams.Email);
            //var decodedToken = HttpUtility.UrlDecode(reestablecerPasswordParams.Token);
            //var resetRes = await userManager.ResetPasswordAsync(user, decodedToken, reestablecerPasswordParams.NewPassword);
            var resetRes = await userManager.ResetPasswordAsync(user, reestablecerPasswordParams.Token, reestablecerPasswordParams.NewPassword);

            if (resetRes.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(resetRes.Errors);
            }

        }



        [HttpPost("solicitar-recuperar-password")]
        public async Task<ActionResult> RecuperarPassword(string email)
        {
            var cuentaBuscada = await context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);

            if(cuentaBuscada == null)
            {
                return BadRequest("No existe esta cuenta");
            }

            var user = await userManager.FindByEmailAsync(email);
            var tokenReset = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(tokenReset);
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = email,
                Subject = "Recuperar Contraseña",
                //Body = "Se ha creado un usuario para esta cuenta en la plataforma tpc, puede accedor usando como usuario " +
                //"este correo " + nuevoUsuarioDTO.Email + " y su contraseña es :" + randomPassword,
                Body = @"<html>
                                <h1>
                                   Se ha solicitado reinicar su contraseña
                                </h1>
                                <p>
                                    Si tu no has sido , ignora este correo. <br>
                                    Ingresa a este link para cambiar tu contraseña: <strong> <a href="+ @"http://localhost:4200/resetpassword?token=" + encodedToken + @"&email="+user.Email + @">Presiona Aqui</a></strong> <br>
                                </p>
                                <p>
                                    Saludos.
                                </p>
                            </html>"
            };

            try
            {
                await mailService.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

            return Ok();
        }


        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<RespuestaAutenticacion> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var credencialesUsuario = new CredencialesUsuario()
            {
                Email = email,
            };

            return ConstruirToken(credencialesUsuario);
        }


        [HttpGet("Sharepoint")]
        public async Task<SharePointToken> SharePoint()
        {
            try
            {
                SharePointAccessRequest sharePointAccessRequest = new SharePointAccessRequest();
                var response = await sharePointService.GetTokenAccessHttpResponseMessage(sharePointAccessRequest);
                return response;
                //return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private RespuestaAutenticacion ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",credencialesUsuario.Email),
                new Claim("prueba", "asd")
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion,
                signingCredentials: creds);

            return new RespuestaAutenticacion
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion,
            };
        }

        private async Task<RespuestaAutenticacionLogin> ConstruirTokenLogin(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",credencialesUsuario.Email),
                new Claim("TPC", "AJSKJKASJ*@J123")
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion,
                signingCredentials: creds);

            Usuario usuario = await context.Usuarios.Include(x => x.TipoRol).Include(x => x.Empresa).FirstOrDefaultAsync(usuarioDB => usuarioDB.Email == credencialesUsuario.Email);

            return new RespuestaAutenticacionLogin
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion,
                Usuario = usuario
            };
        }

        private string GeneratePassword()
        {
            var options = userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

    }
}
