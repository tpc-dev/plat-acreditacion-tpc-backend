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
        }
    }
}
