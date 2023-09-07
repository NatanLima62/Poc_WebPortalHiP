using AutoMapper;
using Poc_WebPortalHiP.Api.Application.DTOs.Usuario;
using Poc_WebPortalHiP.Api.Domain.Entities;

namespace Poc_WebPortalHiP.Api.Application.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Usuario, UsuarioDto>().ReverseMap();
        CreateMap<Usuario, AdicionarUsuarioDto>().ReverseMap();
        CreateMap<Usuario, AtualizarUsuarioDto>().ReverseMap();

    }
}