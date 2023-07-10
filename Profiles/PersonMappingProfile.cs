using AutoMapper;
using PermitRegistrationSystem.Controllers;
using PermitRegistrationSystem.Models;

namespace PermitRegistrationSystem.Profiles;

public class PersonMappingProfile : Profile
{
    public PersonMappingProfile()
    {
        CreateMap<Person, PersonToRegister>().ReverseMap();
        CreateMap<Person, PersonToUpdate>().ReverseMap();
    }
}