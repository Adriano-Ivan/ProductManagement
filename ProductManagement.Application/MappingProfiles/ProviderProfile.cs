using AutoMapper;
using ProductManagement.Application.Features.Providers.Commands.CreateProvider;
using ProductManagement.Application.Features.Providers.Commands.UpdateProvider;
using ProductManagement.Domain;

namespace ProductManagement.Application.MappingProfiles;

public class ProviderProfile : Profile
{
    public ProviderProfile()
    {
        CreateMap<CreateProviderCommand, Provider>().ReverseMap();
        CreateMap<UpdateProviderCommand, Provider>().ReverseMap();  
    }
}
