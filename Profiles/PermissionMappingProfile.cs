using AutoMapper;
using PermitRegistrationSystem.Controllers;
using PermitRegistrationSystem.Models;

namespace PermitRegistrationSystem.Profiles;

public class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<PermissionToCreate, Permission>().ReverseMap();
        CreateMap<PermissionToUpdate, Permission>().ReverseMap();
    }
}