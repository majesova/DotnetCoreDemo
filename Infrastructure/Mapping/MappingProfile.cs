using AutoMapper;
using PDV.API.Controllers.Dto;
using PDV.API.Data.Entities;

namespace MadBugAPI.Infrastructure.Mapping
{
    /// <summary>
    /// Configura las conversiones entre tipos gestionadas por AutoMapper
    /// </summary>
    public class MappingProfile : Profile {
    public MappingProfile() {
            CreateMap<RegisterProductRequestDto, Product>();
            CreateMap<Product, RegisterProductResponseDto>(); 
        }
    }
}