using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermitRegistrationSystem.Models;
using PermitRegistrationSystem.Repositories.Abstract;

namespace PermitRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult CreatePerson([FromBody] PersonToRegister personToRegister)
        {
            Person person = _mapper.Map<Person>(personToRegister);
            _personRepository.Create(person);
            return Ok();
        }
        [HttpPost("delete")]
        public IActionResult DeletePerson([FromBody] int id)
        {
            _personRepository.Delete(id);
            return Ok();
        }
        [HttpGet("get/by/id")]
        public IActionResult GetPersonById([FromQuery] int id)
        {
            Person result = _personRepository.GetById(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdatePersonById([FromBody] PersonToUpdate personToUpdate)
        {
            Person person = _mapper.Map<Person>(personToUpdate);
            _personRepository.Update(person);
            Person result = _personRepository.GetById(person.Id);
            return Ok(result);

        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            IList<Person> result = _personRepository.GetAll();
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
