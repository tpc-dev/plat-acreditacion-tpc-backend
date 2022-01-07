using AutoMapper;
using PlatAcreditacionTPCBackend.DTOs;
using PlatAcreditacionTPCBackend.Entidades;

namespace PlatAcreditacionTPCBackend.Utilidades
{
    public class AutoMapperProfiles : Profile 
    {
        public AutoMapperProfiles ()
        {
            CreateMap<NuevoUsuarioDTO, Usuario>();
            CreateMap<IngresoVisitaDTO, IngresoVisitas>();
            CreateMap<NuevoTipoRolDTO, TipoRol>();
            CreateMap<NuevaEmpresaDTO, Empresa>();
            CreateMap<NuevoEstadoAcreditacionDTO, EstadoAcreditacion>();
            CreateMap<NuevaAreaDTO, Area>();
            CreateMap<NuevaGerenciaDTO, Gerencia>();
            CreateMap<NuevoContratoDTO, Contrato>();
            CreateMap<NuevaEmpresaContratoDTO, EmpresaContrato>();
            CreateMap<NuevoDocumentoClasificacionDTO, DocumentoClasificacion>();
            CreateMap<NuevoCarpetaArranqueDTO, CarpetaArranque>();
            CreateMap<NuevoTrabajadorTPCDTO, TrabajadorTPC>();
        }
    }
}
