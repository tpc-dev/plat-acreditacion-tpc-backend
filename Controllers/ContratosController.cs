using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;
using PlatAcreditacionTPCBackend.Models;
using PlatAcreditacionTPCBackend.Servicios;
using System.Net;
using System.Text.Json;

namespace PlatAcreditacionTPCBackend.Controllers
{
    [ApiController]
    [Route("api/contratos")]
    public class ContratosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        private readonly ISharePointService sharePointService;


        public ContratosController(ApplicationDbContext context, IMapper mapper, ISharePointService sharePointService)
        {
            this.context = context;
            this.mapper = mapper;
            this.sharePointService = sharePointService;

        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Contrato>>> Get()
        {
            return await context.Contratos.Include(contrato => contrato.EtapaCreacionContrato).ToListAsync();
        }


        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Contrato>> GetPorId(int id)
        {
            return await context.Contratos.Include(contrato => contrato.EtapaCreacionContrato).FirstOrDefaultAsync(c => c.Id == id);
        }

        [HttpGet("{idContrato}/carpeta-arranque")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CarpetaArranque>> GetCarpetaArranqueByContratoId()
        {
            //.ForEachAsync(carpeta => carpeta.ItemsCarpetaArranqueCarpetaArranque.FirstOrDefault())
            return await context.CarpetasArranques
                .Include(c=> c.Contrato)
                .FirstOrDefaultAsync();
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
        public async Task<ActionResult<List<HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion>>> GetContratoPasoUnoCompletado(string codigoContrato)
        {

            return await context.HistoricosAcreditacionEmpresaTipoDocumentoAcreditacion.ToListAsync();
        }

        [HttpGet("{contratoId}/empresa-contratadas")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EmpresaContrato>> GetEmpresasContratadaPorContradoId(int contratoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.EmpresasContratos
                .Where(ec => ec.ContratoId == contratoId)
                .Include(ec => ec.Empresa)
                .Include(ec => ec.Contrato)
                .Include(ec => ec.EstadoAcreditacion)
                .FirstOrDefaultAsync();
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

            bool existeEmpresa = await context.Empresas.AnyAsync(empresa => empresa.Id == nuevaEmpresaContratoDTO.EmpresaId);

            if (!existeEmpresa)
            {
                return NotFound($"Empresa no encontrado {nuevaEmpresaContratoDTO.EmpresaId}");
            }

            nuevaEmpresaContratoDTO.FechaCreacion = DateTime.Now;
            nuevaEmpresaContratoDTO.EstadoAcreditacionId = 2; // ESTADO PENDIENTE

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

            List<ContratoUsuario> contratoUsuarios = await context.ContratosUsuarios.Include(contratoUsuario => contratoUsuario.Usuario)
                .Include(contratoUsuario => contratoUsuario.Usuario.TipoRol)
                .Where(contratoUsuario => contratoUsuario.ContratoId == id).ToListAsync();

            return contratoUsuarios;
        }

        [HttpGet("{contratoId}/cargos")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Cargo>>> GetCargosPorContradoId(int contratoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.Cargos
                .Where(ec => ec.ContratoId == contratoId)
                //.Include(ec => ec.Empresa)
                //.Include(ec => ec.Contrato)
                //.Include(ec => ec.ListadoHistoricoAcreditacionEmpresaContrato)
                .ToListAsync();
        }

        [HttpGet("{contratoId}/turnos")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Turno>>> GetTurnosPorContradoId(int contratoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.Turnos
                .Where(ec => ec.ContratoId == contratoId)
                .Include(ec => ec.Jornada)
                .ToListAsync();
        }

        [HttpGet("{contratoId}/jornadas")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Jornada>>> GetJornadasPorContradoId(int contratoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.Jornadas
                .Where(ec => ec.ContratoId == contratoId)
                .ToListAsync();
        }

        [HttpGet("{contratoId}/trabajadores")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ContratoTrabajador>>> GetTrabajadoresPorContradoId(int contratoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.ContratosTrabajadores
                .Where(ct => ct.ContratoId == contratoId)
                .Include(ct => ct.Trabajador)
                .Include(ct => ct.Cargo)
                .Include(ct => ct.EstadoAcreditacion)
                .Include(ct => ct.ListTrabajadorTiposDocumentoAcreditacion.Where(doc=> doc.ContratoTrabajadorContratoId== contratoId))
                .ToListAsync();
        }

        [HttpPost("{contratoId}/empresas/documento")]
        public async Task<ActionResult> PostTipoDocumentoAcreditacionEmpresaAContratoId(int contratoId, EmpresaTipoDocumentoAcreditacion empresaTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            EmpresaContrato empresaContrato = await context.EmpresasContratos.Where(ec => ec.ContratoId == contratoId).FirstOrDefaultAsync();

            empresaTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            empresaTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;
            empresaTipoDocumentoAcreditacion.EmpresaContratoContratoId = empresaContrato.ContratoId;
            empresaTipoDocumentoAcreditacion.EmpresaContratoEmpresaId = empresaContrato.EmpresaId;
            context.Add(empresaTipoDocumentoAcreditacion);
            await context.SaveChangesAsync();


            HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion historico = new()
            {
                EmpresaTipoDocumentoAcreditacionId = empresaTipoDocumentoAcreditacion.Id,
                EstadoAcreditacionId = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("{contratoId}/trabajador/documento")]
        public async Task<ActionResult> PostTipoDocumentoAcreditacionTrabajadorAContratoId(int contratoId, TrabajadorTipoDocumentoAcreditacion trabajadorTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            ContratoTrabajador contratoTrabajador = await context.ContratosTrabajadores.Where(ec => ec.ContratoId == contratoId).FirstOrDefaultAsync();

            trabajadorTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            trabajadorTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;
            trabajadorTipoDocumentoAcreditacion.ContratoTrabajadorContratoId = contratoTrabajador.ContratoId;
            trabajadorTipoDocumentoAcreditacion.ContratoTrabajadorTrabajadorId = contratoTrabajador.TrabajadorId;
            context.Add(trabajadorTipoDocumentoAcreditacion);
            await context.SaveChangesAsync();


            HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion historico = new()
            {
                TrabajadorTipoDocumentoAcreditacionId = trabajadorTipoDocumentoAcreditacion.Id,
                EstadoAcreditacionId = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{contratoId}/vehiculo/documento")]
        public async Task<ActionResult> PostTipoDocumentoAcreditacionVehiculoAContratoId(int contratoId, VehiculoTipoDocumentoAcreditacion vehiculoTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            ContratoVehiculo contratoVehiculo = await context.ContratosVehiculos.Where(ec => ec.ContratoId == contratoId).FirstOrDefaultAsync();

            vehiculoTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            vehiculoTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;
            vehiculoTipoDocumentoAcreditacion.ContratoVehiculoContratoId = contratoVehiculo.ContratoId;
            vehiculoTipoDocumentoAcreditacion.ContratoVehiculoVehiculoId = contratoVehiculo.VehiculoId;
            context.Add(vehiculoTipoDocumentoAcreditacion);
            await context.SaveChangesAsync();


            HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion historico = new()
            {
                VehiculoTipoDocumentoAcreditacionId = vehiculoTipoDocumentoAcreditacion.Id,
                EstadoAcreditacionId = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{contratoId}/documento/{documentoId}/file")]
        public async Task<ActionResult> PostSubirArchivoToDocumentoContrato(int contratoId,int documentoId)
        {

            SharePointToken sharePointToken = await sharePointService.GetTokenAccess(null);
        //https://terminalpuertocoquimbo.sharepoint.com/sites/CapstonePruebas/_api/Web/GetFolderByServerRelativeUrl('Documentos%20compartidos')/Folders

            return Ok(sharePointToken);
        }


        [HttpPost("{contratoId}/documento")]
        public async Task<ActionResult> PostTipoDocumentoAcreditacionContratoAContratoId(int contratoId, ContratoTipoDocumentoAcreditacion contratoTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            contratoTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            contratoTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;



            context.Add(contratoTipoDocumentoAcreditacion);

            await context.SaveChangesAsync();

            HistoricoAcreditacionContratoTipoDocumentoAcreditacion historico = new()
            {
                ContratoTipoDocumentoAcreditacionId = contratoTipoDocumentoAcreditacion.Id,
                EstadoAcreditacionId                = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();
        }

        #region Editar Documento 

        [HttpPut("{contratoId}/documento")]
        public async Task<ActionResult> PutTipoDocumentoAcreditacionContratoAContratoId(int contratoId, ContratoTipoDocumentoAcreditacion contratoTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

          //  contratoTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            contratoTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;

            ContratoTipoDocumentoAcreditacion existe = await context.ContratoTiposDocumentoAcreditacion
                .FirstOrDefaultAsync(x => x.ContratoId == contratoTipoDocumentoAcreditacion.ContratoId && x.TipoDocumentoAcreditacionId == contratoTipoDocumentoAcreditacion.TipoDocumentoAcreditacionId);

            if(existe == null)
            {
                return NotFound();
            }

            existe.UpdatedAt = DateTime.Now;
            existe.EstadoAcreditacionId = 2;
            existe.UrlFile = contratoTipoDocumentoAcreditacion.UrlFile;
            existe.FechaInicio = contratoTipoDocumentoAcreditacion.FechaInicio;
            existe.FechaTermino = contratoTipoDocumentoAcreditacion.FechaTermino;

            context.Update(existe);

            await context.SaveChangesAsync();

            HistoricoAcreditacionContratoTipoDocumentoAcreditacion historico = new()
            {
                ContratoTipoDocumentoAcreditacionId = existe.Id,
                EstadoAcreditacionId = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{contratoId}/empresas/documento")]
        public async Task<ActionResult> PutTipoDocumentoAcreditacionEmpresaAContratoId(int contratoId, EmpresaTipoDocumentoAcreditacion empresaTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound($"No existe contrato de id {contratoId}");
            }

            //  contratoTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            empresaTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;

            EmpresaTipoDocumentoAcreditacion existe = await context.EmpresaTiposDocumentosAcreditacion
                .FirstOrDefaultAsync(x => x.EmpresaContratoContratoId == empresaTipoDocumentoAcreditacion.EmpresaContratoContratoId &&
                                          x.EmpresaContratoEmpresaId == empresaTipoDocumentoAcreditacion.EmpresaContratoEmpresaId &&
                                          x.TipoDocumentoAcreditacionId == empresaTipoDocumentoAcreditacion.TipoDocumentoAcreditacionId);

            if (existe == null)
            {
                return NotFound($"No existe EmpresaTipoDocumentoAcreditacion de id {empresaTipoDocumentoAcreditacion}");
            }

            existe.UpdatedAt = DateTime.Now;
            existe.EstadoAcreditacionId = 2;
            existe.UrlFile = empresaTipoDocumentoAcreditacion.UrlFile;
            existe.FechaInicio = empresaTipoDocumentoAcreditacion.FechaInicio;
            existe.FechaTermino = empresaTipoDocumentoAcreditacion.FechaTermino;

            context.Update(existe);

            await context.SaveChangesAsync();

            HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion historico = new()
            {
                EmpresaTipoDocumentoAcreditacionId = existe.Id,
                EstadoAcreditacionId = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{contratoId}/trabajador/documento")]
        public async Task<ActionResult> PutTipoDocumentoAcreditacionTrabajadorAContratoId(int contratoId, TrabajadorTipoDocumentoAcreditacion trabajadorTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            //  contratoTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            trabajadorTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;

            TrabajadorTipoDocumentoAcreditacion existe = await context.TrabajadorTiposDocumentoAcreditacion
                .FirstOrDefaultAsync(x => x.ContratoTrabajadorTrabajadorId == trabajadorTipoDocumentoAcreditacion.ContratoTrabajadorTrabajadorId &&
                                          x.ContratoTrabajadorContratoId == trabajadorTipoDocumentoAcreditacion.ContratoTrabajadorContratoId &&
                                          x.TipoDocumentoAcreditacionId == trabajadorTipoDocumentoAcreditacion.TipoDocumentoAcreditacionId);

            if (existe == null)
            {
                return NotFound();
            }

            existe.UpdatedAt = DateTime.Now;
            existe.EstadoAcreditacionId = 2;
            existe.UrlFile = trabajadorTipoDocumentoAcreditacion.UrlFile;
            existe.FechaInicio = trabajadorTipoDocumentoAcreditacion.FechaInicio;
            existe.FechaTermino = trabajadorTipoDocumentoAcreditacion.FechaTermino;

            context.Update(existe);

            await context.SaveChangesAsync();

            HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion historico = new()
            {
                TrabajadorTipoDocumentoAcreditacionId = existe.Id,
                EstadoAcreditacionId = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{contratoId}/vehiculo/documento")]
        public async Task<ActionResult> PutTipoDocumentoAcreditacionVehiculoAContratoId(int contratoId, VehiculoTipoDocumentoAcreditacion vehiculoTipoDocumentoAcreditacion)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            //  contratoTipoDocumentoAcreditacion.CreatedAt = DateTime.Now;
            vehiculoTipoDocumentoAcreditacion.UpdatedAt = DateTime.Now;

            VehiculoTipoDocumentoAcreditacion existe = await context.VehiculoTiposDocumentosAcreditacion
                .FirstOrDefaultAsync(x => x.ContratoVehiculoVehiculoId == vehiculoTipoDocumentoAcreditacion.ContratoVehiculoVehiculoId &&
                                          x.ContratoVehiculoContratoId == vehiculoTipoDocumentoAcreditacion.ContratoVehiculoContratoId &&
                                          x.TipoDocumentoAcreditacionId == vehiculoTipoDocumentoAcreditacion.TipoDocumentoAcreditacionId);

            if (existe == null)
            {
                return NotFound();
            }

            existe.UpdatedAt = DateTime.Now;
            existe.EstadoAcreditacionId = 2;
            existe.UrlFile = vehiculoTipoDocumentoAcreditacion.UrlFile;
            existe.FechaInicio = vehiculoTipoDocumentoAcreditacion.FechaInicio;
            existe.FechaTermino = vehiculoTipoDocumentoAcreditacion.FechaTermino;

            context.Update(existe);

            await context.SaveChangesAsync();

            HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion historico = new()
            {
                VehiculoTipoDocumentoAcreditacionId = existe.Id,
                EstadoAcreditacionId = 2, // ESTADO PENDIENTE
                Fecha = DateTime.Now,
            };

            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();
        }

        #endregion

        [HttpGet("{contratoId}/empresas/{empresaId}/documentos-requeridos")]
        public async Task<ActionResult<List<EmpresaTipoDocumentoAcreditacion>>> GetDocumentoAcreditacionEmpresaAContratoId(
            int contratoId, int empresaId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            bool existeEmpresa = await context.Empresas.AnyAsync(x => x.Id == empresaId);
            if (!existeEmpresa)
            {
                return NotFound();
            }


            bool existeContratoEmpresa = await context.EmpresasContratos.AnyAsync(ec => ec.ContratoId == contratoId && ec.EmpresaId == empresaId);
            return await context.EmpresaTiposDocumentosAcreditacion
                .Where(doc=>doc.EmpresaContratoContratoId == contratoId && doc.EmpresaContratoEmpresaId == empresaId)
                .Include(doc => doc.EstadoAcreditacion)
                .Include(c => c.Contrato)
                .Include(doc => doc.TipoDocumentoAcreditacion.DocumentoClasificacion)
                .ToListAsync();
        }

        [HttpGet("{contratoId}/documentos-requeridos")]
        public async Task<ActionResult<List<ContratoTipoDocumentoAcreditacion>>> GetDocumentoAcreditacionContratoId(
           int contratoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.ContratoTiposDocumentoAcreditacion
                .Where(doc => doc.ContratoId == contratoId )
                .Include(c => c.Contrato)
                .Include(c => c.ListHistoricosAcreditacionContratoTipoDocumentoAcreditacion.Take(2))
                .Include(c => c.EstadoAcreditacion)
                .Include(c => c.TipoDocumentoAcreditacion.DocumentoClasificacion)
                .ToListAsync();
        }


        #region CAMBIAR ESTADO DOCUMENTO ACREDITACION

        [HttpPut("documento/{contratoDocumentoId}/estado-acreditacion")]
        public async Task<ActionResult> CambiarEstadoAcreditacionDocumentoContrato (
            int contratoDocumentoId, HistoricoAcreditacionContratoTipoDocumentoAcreditacion contratoTipoDocumentoAcreditacion)
        {
            ContratoTipoDocumentoAcreditacion existe = await context.ContratoTiposDocumentoAcreditacion.FirstOrDefaultAsync(c => contratoTipoDocumentoAcreditacion.ContratoTipoDocumentoAcreditacionId == c.Id);
            //ContratoTipoDocumentoAcreditacion existe = await context.ContratoTiposDocumentoAcreditacion.FirstOrDefaultAsync(c => contratoDocumentoId == c.Id);
            if (existe == null)
            {
                return NotFound("Documento de contrato no encontrado");
            }

            existe.EstadoAcreditacionId = contratoTipoDocumentoAcreditacion.EstadoAcreditacionId;
            existe.UpdatedAt = DateTime.Now;
            context.Update(existe);

            contratoTipoDocumentoAcreditacion.Fecha = DateTime.Now;
            context.Add(contratoTipoDocumentoAcreditacion);

            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("documento/{empresaDocumentoId}/empresa/estado-acreditacion")]
        public async Task<ActionResult> CambiarEstadoAcreditacionDocumentoEmpresa(
            int empresaDocumentoId, HistoricoAcreditacionEmpresaTipoDocumentoAcreditacion historico)
        {
            EmpresaTipoDocumentoAcreditacion existe = await context.EmpresaTiposDocumentosAcreditacion
                .FirstOrDefaultAsync(c => empresaDocumentoId == c.Id);
            //ContratoTipoDocumentoAcreditacion existe = await context.ContratoTiposDocumentoAcreditacion.FirstOrDefaultAsync(c => contratoDocumentoId == c.Id);
            if (existe == null)
            {
                return NotFound("Documento de contrato no encontrado");
            }

            existe.EstadoAcreditacionId = historico.EstadoAcreditacionId;
            existe.UpdatedAt = DateTime.Now;
            context.Update(existe);

            historico.Fecha = DateTime.Now;
            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("documento/{trabajadorDocumentoId}/trabajador/estado-acreditacion")]
        public async Task<ActionResult> CambiarEstadoAcreditacionDocumentoTrabajador(
           int trabajadorDocumentoId, HistoricoAcreditacionTrabajadorTipoDocumentoAcreditacion historico)
        {
            TrabajadorTipoDocumentoAcreditacion existe = await context.TrabajadorTiposDocumentoAcreditacion
                .FirstOrDefaultAsync(c => trabajadorDocumentoId == c.Id);
            //ContratoTipoDocumentoAcreditacion existe = await context.ContratoTiposDocumentoAcreditacion.FirstOrDefaultAsync(c => contratoDocumentoId == c.Id);
            if (existe == null)
            {
                return NotFound("Documento de contrato no encontrado");
            }

            existe.EstadoAcreditacionId = historico.EstadoAcreditacionId;
            existe.UpdatedAt = DateTime.Now;
            context.Update(existe);

            historico.Fecha = DateTime.Now;
            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("documento/{vehiculoDocumentoId}/vehiculo/estado-acreditacion")]
        public async Task<ActionResult> CambiarEstadoAcreditacionDocumentoVehiculo(
          int vehiculoDocumentoId, HistoricoAcreditacionVehiculoTipoDocumentoAcreditacion historico)
        {
            VehiculoTipoDocumentoAcreditacion existe = await context.VehiculoTiposDocumentosAcreditacion
                .FirstOrDefaultAsync(c => vehiculoDocumentoId == c.Id);
            //ContratoTipoDocumentoAcreditacion existe = await context.ContratoTiposDocumentoAcreditacion.FirstOrDefaultAsync(c => contratoDocumentoId == c.Id);
            if (existe == null)
            {
                return NotFound("Documento de contrato no encontrado");
            }

            existe.EstadoAcreditacionId = historico.EstadoAcreditacionId;
            existe.UpdatedAt = DateTime.Now;
            context.Update(existe);

            historico.Fecha = DateTime.Now;
            context.Add(historico);

            await context.SaveChangesAsync();
            return Ok();

        }

        #endregion

        [HttpGet("{contratoId}/vehiculo/{vehiculoId}/documentos-requeridos")]
        public async Task<ActionResult<List<VehiculoTipoDocumentoAcreditacion>>> GetDocumentoAcreditacionVehiculoContratoId(
          int contratoId, int vehiculoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.VehiculoTiposDocumentosAcreditacion
                .Where(doc => doc.ContratoVehiculoContratoId == contratoId && doc.ContratoVehiculoVehiculoId == vehiculoId)
                 .Include(c => c.Contrato)
                .Include(c => c.ListHistoricosAcreditacionVehiculoTipoDocumentoAcreditacion)
                .Include(c => c.EstadoAcreditacion)
                .Include(c => c.TipoDocumentoAcreditacion.DocumentoClasificacion)
                .ToListAsync();
        }

        [HttpGet("{contratoId}/trabajador/{trabajadorId}/documentos-requeridos")]
        public async Task<ActionResult<List<TrabajadorTipoDocumentoAcreditacion>>> GetDocumentoAcreditacionTrabajadorContratoId(
         int contratoId, int trabajadorId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.TrabajadorTiposDocumentoAcreditacion
                .Where(doc => doc.ContratoTrabajadorContratoId == contratoId && doc.ContratoTrabajadorTrabajadorId == trabajadorId)
                 .Include(c => c.Contrato)
                .Include(c => c.ListHistoricosAcreditacionTrabajadorTipoDocumentoAcreditacion)
                .Include(c => c.EstadoAcreditacion)
                .Include(c => c.TipoDocumentoAcreditacion.DocumentoClasificacion)
                .ToListAsync();
        }

        [HttpPost("{contratoId}/vehiculos")]
        public async Task<ActionResult> PostVehiculosPorContratoId(int contratoId, Vehiculo vehiculo)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            context.Add(vehiculo);
            await context.SaveChangesAsync();

            ContratoVehiculo contratoVehiculo = new ContratoVehiculo
            {
                ContratoId = contratoId,
                VehiculoId = vehiculo.Id,
                EstadoAcreditacionId = 2,// ESTADO PENDIENTE
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            context.Add(contratoVehiculo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{contratoId}/vehiculos")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ContratoVehiculo>>> GetVehiculosPorContradoId(int contratoId)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            return await context.ContratosVehiculos
                .Where(ct => ct.ContratoId == contratoId)
                .Include(ct => ct.EstadoAcreditacion)
                .Include(ct => ct.Vehiculo)
                .ThenInclude(v => v.Chofer)
                .ToListAsync();
        }

        [HttpPost("{contratoId}/trabajadores")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostTrabajadoresPorContradoId(int contratoId, Trabajador trabajador)
        {

            bool existeTrabajador = await context.Trabajadores.AnyAsync(x => x.Rut == trabajador.Rut);
            if (existeTrabajador)
            {
                return BadRequest("Ya existe un trabajador con este Rut en la base de datos");
            }


            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            trabajador.CreatedAt = DateTime.Now;
            trabajador.UpdatedAt = DateTime.Now;

            context.Add(trabajador);
            await context.SaveChangesAsync();
            return Ok();


        }

        [HttpPost("{contratoId}/trabajadores/asignar")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostTrabajadoresAsignarAContradoId(int contratoId, ContratoTrabajador contratoTrabajador)
        {

            if (contratoId != contratoTrabajador.ContratoId)
            {
                return BadRequest("Los id de contrato no coinciden");
            }


            bool existeTrabajador = await context.Trabajadores.AnyAsync(x => x.Id == contratoTrabajador.TrabajadorId);
            if (!existeTrabajador)
            {
                return NotFound("Trabajador no encontrado");
            }


            Contrato existeContrato = await context.Contratos.FirstOrDefaultAsync(x => x.Id == contratoId);
            if (existeContrato == null)
            {
                return NotFound();
            }

            if (existeContrato.EstadoAcreditacionId != 2)
            {
                return BadRequest("Contrato ya acreditado, no puede asignar trabajadores");
            }

            contratoTrabajador.CreatedAt = DateTime.Now;
            contratoTrabajador.UpdatedAt = DateTime.Now;

            context.Add(contratoTrabajador);
            await context.SaveChangesAsync();
            return Ok();

        }


        [HttpPost("{contratoId}/turnos")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostTurnosPorContradoId(int contratoId, Turno turno)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            turno.CreatedAt = DateTime.Now;
            turno.UpdatedAt = DateTime.Now;

            context.Add(turno);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{contratoId}/jornadas")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostJornadasContratadaPorContradoId(int contratoId, Jornada jornada)
        {
            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
            }

            jornada.CreatedAt = DateTime.Now;
            jornada.UpdatedAt = DateTime.Now;

            context.Add(jornada);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{contratoId}/cargos")]
        public async Task<ActionResult> PostCargoPorContrato(Cargo cargo)
        {
            bool existeContrato = await context.Contratos.AnyAsync(contrato => contrato.Id == cargo.ContratoId);
            if (!existeContrato)
            {
                return NotFound();
            }
            // var nuevoTipoRolMapped = mapper.Map<TipoRol>(nuevoTipoRolDTO);
            cargo.CreatedAt = DateTime.Now;
            cargo.UpdatedAt = DateTime.Now;
            context.Add(cargo);
            await context.SaveChangesAsync();
            return Ok();
        }



        [HttpPost("completar-paso-dos")]
        public async Task<ActionResult> CrearContratoPasoDos(NuevoContratoUsuarioDTO nuevoContratoUsuario)
        {

            ContratoUsuario contratoUsuarioADCTPC1 = new ContratoUsuario
            {
                ContratoId = nuevoContratoUsuario.contratoId,
                UsuarioId = nuevoContratoUsuario.adctpc1Id,
                FechaCreacion = DateTime.Now
            };

            // SI EXISTE UN SEGUNDO ADCTPC ASOCIADO AL CONTRATO
            if (nuevoContratoUsuario.adctpc2Id > 0)
            {
                ContratoUsuario contratoUsuarioADCTPC2 = new ContratoUsuario
                {
                    ContratoId = nuevoContratoUsuario.contratoId,
                    UsuarioId = nuevoContratoUsuario.adctpc2Id,
                    FechaCreacion = DateTime.Now
                };

                context.Add(contratoUsuarioADCTPC2);
            }

            // LOS ADIMISTRADORES DE CONTRATO EXTERNOS TIENEN UN AREA Y GERENCIA FIJO DE NOMBRE EXTERNOS

            Gerencia idGerenciaExternos = await context.Gerencias.FirstOrDefaultAsync(gerencia => gerencia.Nombre == "Externos");
            Area idAreaExternos = await context.Areas.FirstOrDefaultAsync(area => area.Nombre == "Externos");

            ContratoUsuario contratoUsuarioADCEECC = new ContratoUsuario
            {
                ContratoId = nuevoContratoUsuario.contratoId,
                UsuarioId = nuevoContratoUsuario.adceeccId,
                FechaCreacion = DateTime.Now,
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

            return await context.EmpresasContratos.Include(x => x.Empresa).Include(x => x.Contrato).Where(x => x.ContratoId == id).ToListAsync();
        }

        
        [HttpPost("{contratoId}/acreditar")]
        public async Task<ActionResult> AcreditarContrato(int contratoId)
        {
            // VERIFICAR QUE NO HAYA ITEMS SIN ACREDITAR ASOCIADOS A ESTE CONTRATO  
            
            // OBTENER ITEMS NECESARIOS DE LA CARPETA DE ARRANQUE PARA ESTE CONTRATO

            // VERIFICAR DOCUMENTOS CONTRATO

            bool existeDocumentoContratoNoAcreditado = await context.ContratoTiposDocumentoAcreditacion
                .Where(doc => doc.ContratoId == contratoId && doc.EstadoAcreditacionId != 1)
                .AnyAsync();

            if (existeDocumentoContratoNoAcreditado)
            {
                return BadRequest("Existen documentos asociados al contrato que no estan acreditados");
            }

            //VERIFICAR EMPRESA

            bool existeDocumentoEmpresaNoAcreditado = await context.EmpresaTiposDocumentosAcreditacion
                .Where(doc => doc.EmpresaContratoContratoId == contratoId &&  doc.EstadoAcreditacionId != 1)
                .AnyAsync();

            if (existeDocumentoEmpresaNoAcreditado)
            {
                return BadRequest("Existen documentos asociados a la empresa que no estan acreditados");
            }

            //VERIFICAR TRABAJADORES
            bool existeDocumentoTrabajadorNoAcreditado = await context.TrabajadorTiposDocumentoAcreditacion
                .Where(doc => doc.ContratoTrabajadorContratoId == contratoId && doc.EstadoAcreditacionId != 1)
                .AnyAsync();

            if (existeDocumentoTrabajadorNoAcreditado)
            {
                return BadRequest("Existen documentos asociados al trabajador que no estan acreditados");
            }

            //VERIFICAR VEHICULOS
            bool existeDocumentoVehiculoNoAcreditado = await context.VehiculoTiposDocumentosAcreditacion
                .Where(doc => doc.ContratoVehiculoContratoId == contratoId && doc.EstadoAcreditacionId != 1)
                .AnyAsync();

            if (existeDocumentoVehiculoNoAcreditado)
            {
                return BadRequest("Existen documentos asociados al vehiculo que no estan acreditados");
            }

            var contrato = await context.Contratos.FirstOrDefaultAsync(c => c.Id == contratoId);

            contrato.EstadoAcreditacionId = 1;
            context.Update(contrato);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{idContrato}/empresas/{empresaId}/acreditar")]
        public async Task<ActionResult<EmpresaContrato>> PutEmpresasPorContratoId(int idContrato, int empresaId)
        {
            EmpresaContrato existe = await context.EmpresasContratos.FirstOrDefaultAsync(x => x.ContratoId == idContrato && x.EmpresaId == empresaId);
            if (existe == null)
            {
                return NotFound($"No existe empresa en contrato");
            }

            existe.EstadoAcreditacionId = 1;
            context.Update(existe);
            await context.SaveChangesAsync();
            return Ok(existe);
        }


        [HttpPut("{idContrato}/trabajador/{trabajadorId}/acreditar")]
        public async Task<ActionResult<ContratoTrabajador>> PutTrabajadorPorContratoId(int idContrato, int trabajadorId)
        {
            ContratoTrabajador existe = await context.ContratosTrabajadores.FirstOrDefaultAsync(x => x.ContratoId == idContrato && x.TrabajadorId == trabajadorId);
            if (existe == null)
            {
                return NotFound();
            }

            existe.EstadoAcreditacionId = 1;
            context.Update(existe);
            await context.SaveChangesAsync();
            return Ok(existe);
        }

        [HttpPut("{idContrato}/vehiculo/{vehiculoId}/acreditar")]
        public async Task<ActionResult<ContratoVehiculo>> PutVehiculoPorContratoId(int idContrato, int vehiculoId)
        {
            ContratoVehiculo existe = await context.ContratosVehiculos.FirstOrDefaultAsync(x => x.ContratoId == idContrato && x.VehiculoId == vehiculoId);
            if (existe == null)
            {
                return NotFound();
            }

            existe.EstadoAcreditacionId = 1;
            context.Update(existe);
            await context.SaveChangesAsync();
            return Ok(existe);
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

            bool existeOrden = await context.EtapasCreacionContrato.AnyAsync(etapa => etapa.Id == id);

            if (!existeOrden)
            {
                return NotFound();
            }

            return await context.Contratos
                .Include(contrato => contrato.EtapaCreacionContrato)
                .Include(contrato => contrato.Area)
                .Include(contrato => contrato.EstadoAcreditacion)
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
