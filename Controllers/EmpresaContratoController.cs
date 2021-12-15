using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatAcreditacionTPCBackend.Entidades;
using System.Net;

namespace PlatAcreditacionTPCBackend.Controllers
{

    [ApiController]
    [Route("api/empresa-contrato")]
    public class EmpresaContratoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EmpresaContratoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("paso-uno-completado")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> GetContratoPasoUnoCompletado(string codigoContrato)
        {
            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.CodigoContrato == codigoContrato);
            return existe;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(EmpresaContrato empresaContrato)
        {
            context.Add(empresaContrato);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
