using System.Security.Claims;
using Application.Features.Permissions.Commands.DeletePermission;
using Application.Features.Permissions.Queries.GetByIdPermission;
using Application.Features.Persons.Commands.AddPerson;
using Application.Features.Persons.Commands.DeletePerson;
using Application.Features.Persons.Commands.Login;
using Application.Features.Persons.Commands.UpdatePerson;
using Application.Features.Persons.Dtos;
using Application.Features.Persons.Models;
using Application.Features.Persons.Queries.GerByIdPerson;
using Application.Features.Persons.Queries.GetAllPersons;
using Application.Repositories;
using AutoMapper;
using Core.Security.JWT;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace PermitRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonToRegisterDto personToRegisterDto)
        {
            AddPersonCommand command = new AddPersonCommand() { PersonToRegisterDto = personToRegisterDto };
            AccessToken result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            DeletePersonCommand command = new DeletePersonCommand() { Id = id };
            DeletedPersonDto result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetPersonById([FromRoute] int id)
        {
            GetByIdPersonQuery query = new() { Id = id };
            GetByIdPersonDto result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePersonById([FromBody] PersonToUpdateDto personToUpdateDto)
        {
           UpdatePersonCommand command = new UpdatePersonCommand() { PersonToUpdateDto = personToUpdateDto };
           UpdatedPersonDto result = await Mediator.Send(command);
           return Ok(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            IList<Claim> xd = HttpContext.User.Claims.ToList();
           GetAllPersonsQuery query = new GetAllPersonsQuery();
           GetAllPersonsModel model = await Mediator.Send(query);
           return Ok(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] PersonForLoginDto personForLoginDto)
        {
            LoginCommand command = new LoginCommand() { PersonForLoginDto = personForLoginDto };
            AccessToken result = await Mediator.Send(command);
            if (result == null) return BadRequest("Not logged in");
            return Ok(result);
        }


    }

    public class PersonToRegister
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class PersonToUpdate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
