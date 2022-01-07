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

        [HttpGet("{contratoId}/empresa-contratadas")]
        //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EmpresaContrato>>> GetEmpresasContratadaPorContradoId(int contratoId)
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
                .Include(ec => ec.ListadoHistoricoAcreditacionEmpresaContrato)
                .ToListAsync();
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
                .Include(c => c.ListHistoricosAcreditacionContratoTipoDocumentoAcreditacion)
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


            bool existeContrato = await context.Contratos.AnyAsync(x => x.Id == contratoId);
            if (!existeContrato)
            {
                return NotFound();
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

            return await context.EmpresasContratos.Include(x => x.ListadoHistoricoAcreditacionEmpresaContrato).Include(x => x.Empresa).Include(x => x.Contrato).Where(x => x.ContratoId == id).ToListAsync();
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
