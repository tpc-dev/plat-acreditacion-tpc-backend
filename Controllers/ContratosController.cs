using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;
using PlatAcreditacionTPCBackend.Models;
using System.Net;
using System.Text.Json;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/contratos")]
    public class ContratosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ContratosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Contrato>>> Get()
        {
            return await context.Contratos.Include(contrato => contrato.EtapaCreacionContrato).ToListAsync();
        }

        [HttpGet("{idContrato}/carpeta-arranque")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CarpetaArranque>> GetCarpetaArranqueByContratoId()
        {   
                //.ForEachAsync(carpeta => carpeta.ItemsCarpetaArranqueCarpetaArranque.FirstOrDefault())
            return await context.CarpetasArranques.FirstOrDefaultAsync();
        }


        [HttpGet("existe/{codigoContrato}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> GetContratoYaExiste(string codigoContrato)
        {

            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.CodigoContrato == codigoContrato);
            return existe;
        } 
        
        [HttpGet("historico")]
      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<HistoricoAcreditacionEmpresaContrato>>> GetContratoPasoUnoCompletado(string codigoContrato)
        {   

            return await context.HistoricosAcreditacionEmpresaContratos.ToListAsync();
        }


        [HttpPost("completar-paso-uno")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CrearContratoPasoUno(NuevaEmpresaContratoDTO nuevaEmpresaContratoDTO)
        {
            
            bool existeContrato = await context.Contratos.AnyAsync(contrato => contrato.Id == nuevaEmpresaContratoDTO.ContratoId);

            if (!existeContrato)
            {
                return NotFound($"Contrado no encontrado {nuevaEmpresaContratoDTO.ContratoId}");
            }

            bool existeEmpresa = await context.Empresas.AnyAsync(empresa=> empresa.Id == nuevaEmpresaContratoDTO.EmpresaId);

            if (!existeEmpresa)
            {
                return NotFound($"Empresa no encontrado {nuevaEmpresaContratoDTO.EmpresaId}");
            }

            nuevaEmpresaContratoDTO.FechaCreacion = DateTime.Now;

            var nuevaEmpresaContratoMapeado = mapper.Map<EmpresaContrato>(nuevaEmpresaContratoDTO);
            context.Add(nuevaEmpresaContratoMapeado);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}/contrato-usuarios")]
        public async Task<ActionResult<List<ContratoUsuario>>> ObtenerUsuariosPorContratoId(int id)
        {
            bool existeContrato = await context.Contratos.AnyAsync(contrato => contrato.Id == id);

            if (!existeContrato)
            {
                return NotFound();
            }

            List<ContratoUsuario> contratoUsuarios = await context.ContratosUsuarios.Include(contratoUsuario=>contratoUsuario.Usuario)
                .Include(contratoUsuario => contratoUsuario.Usuario.TipoRol)
                .Where(contratoUsuario => contratoUsuario.ContratoId == id).ToListAsync();

            return contratoUsuarios;
        }

        [HttpPost("completar-paso-dos")]
        public async Task<ActionResult> CrearContratoPasoDos(NuevoContratoUsuarioDTO nuevoContratoUsuario)
        {   

            ContratoUsuario contratoUsuarioADCTPC1 = new ContratoUsuario
            {
                ContratoId      = nuevoContratoUsuario.contratoId,
                UsuarioId       = nuevoContratoUsuario.adctpc1Id,
                GerenciaId      = nuevoContratoUsuario.gerenciaId,
                AreaId          = nuevoContratoUsuario.areaId,
                FechaCreacion   = DateTime.Now
            };

            // SI EXISTE UN SEGUNDO ADCTPC ASOCIADO AL CONTRATO
            if (nuevoContratoUsuario.adctpc2Id>0)
            {
                ContratoUsuario contratoUsuarioADCTPC2 = new ContratoUsuario
                {
                    ContratoId    = nuevoContratoUsuario.contratoId,
                    UsuarioId     = nuevoContratoUsuario.adctpc2Id,
                    GerenciaId    = nuevoContratoUsuario.gerencia2Id,
                    AreaId        = nuevoContratoUsuario.area2Id,
                    FechaCreacion = DateTime.Now
                };

                context.Add(contratoUsuarioADCTPC2);
            }

            // LOS ADIMISTRADORES DE CONTRATO EXTERNOS TIENEN UN AREA Y GERENCIA FIJO DE NOMBRE EXTERNOS

            Gerencia idGerenciaExternos = await context.Gerencias.FirstOrDefaultAsync(gerencia => gerencia.Nombre == "Externos");    
            Area idAreaExternos = await context.Areas.FirstOrDefaultAsync(area => area.Nombre == "Externos");    

            ContratoUsuario contratoUsuarioADCEECC = new ContratoUsuario
            {
                ContratoId    = nuevoContratoUsuario.contratoId,
                UsuarioId     = nuevoContratoUsuario.adceeccId,
                FechaCreacion = DateTime.Now,
                AreaId        = idAreaExternos.Id,
                GerenciaId    = idGerenciaExternos.Id
            };

            context.Add(contratoUsuarioADCTPC1);
            context.Add(contratoUsuarioADCEECC);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("completar-paso-tres")]
        public async Task<ActionResult> CrearContratoPasoTres(CarpetaArranquePasoTres carpetaArranquePasoTres)
        {   
            foreach (var indice in carpetaArranquePasoTres.ListIdItemsCarpetaArranque)
            {
                ItemCarpetaArranqueCarpetaArranque itemCarpetaArranqueCarpetaArranque = new ItemCarpetaArranqueCarpetaArranque
                {
                    CarpetaArranqueId = carpetaArranquePasoTres.CarpetaArranqueId,
                    ItemCarpetaArranqueId = indice,
                    Obligatorio = true
                };

                context.Add(itemCarpetaArranqueCarpetaArranque);
            }

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}/empresas")]
        public async Task<ActionResult<List<EmpresaContrato>>> GetEmpresasPorContratoId(int id)
        {
            bool existe = await context.Contratos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            return await context.EmpresasContratos.Include(x=> x.ListadoHistoricoAcreditacionEmpresaContrato).Include(x => x.Empresa).Include(x=> x.Contrato).Where(x => x.ContratoId == id).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Contrato>> Post(Contrato contrato)
        {
            context.Add(contrato);
            await context.SaveChangesAsync();
            return Ok(contrato);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Contrato contrato, int id)
        {
            if (contrato.Id != id)
            {
                return BadRequest("El id del contrato no coincide con el id de la URL");
            }

            bool existe = await context.Contratos.AnyAsync(contratoS => contratoS.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(contrato);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("etapa-creacion/{id:int}")]
        public async Task<ActionResult<List<Contrato>>> GetContratosPorOrden(int id)
        {
            
            bool existeOrden = await context.EtapasCreacionContrato.AnyAsync(etapa => etapa.Id== id);
            
            if (!existeOrden)
            {
                return NotFound();
            }

            return await context.Contratos
                .Include(contrato => contrato.EtapaCreacionContrato)
                .Include(contrato => contrato.Area)
                .Include(contrato => contrato.EmpresaContrato.Empresa)
                .Where(contrato => contrato.EtapaCreacionContratoId == id).ToListAsync();
        }

        [HttpPut("{id:int}/cambiar-etapa-creacion/{idEtapa:int}")]
        public async Task<ActionResult> CambiarEtapaCreacion(int idEtapa, int id)
        {

            var contrato = await context.Contratos.FirstOrDefaultAsync(contratoS => contratoS.Id == id);
            if (contrato == null)
            {
                return NotFound("Contrato No encontrado");
            }

            var etapaCreacion = await context.EtapasCreacionContrato.FirstOrDefaultAsync(etapa => etapa.Id == idEtapa);

            if (etapaCreacion == null)
            {
                return NotFound("Orden No encontrada");
            }

            contrato.EtapaCreacionContratoId = idEtapa;
            context.Entry(contrato).State = EntityState.Modified;
            context.Update(contrato);
            await context.SaveChangesAsync();
            return Ok(contrato);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Contratos.AnyAsync(contrato => contrato.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Contrato() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
