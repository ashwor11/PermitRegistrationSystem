using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PermitRegistrationSystem.Models;
using PermitRegistrationSystem.Repositories;
using PermitRegistrationSystem.Repositories.Abstract;
using PermitRegistrationSystem.Validation;

namespace PermitRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PermissionToCreate permissionToCreate)
        {
            Permission permission = _mapper.Map<Permission>(permissionToCreate);
            ValidatePermission(permission);
            _permissionRepository.Create(permission);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            _permissionRepository.Delete(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] PermissionToUpdate permissionToUpdate)
        {
            Permission permission = _mapper.Map<Permission>(permissionToUpdate);
            ValidatePermission(permission);
            _permissionRepository.Update(permission);
            return Ok();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            IList<Permission> permissions = _permissionRepository.GetAll();
            return Ok(permissions);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Permission permission = _permissionRepository.GetById(id);
            return Ok(permission);
        }

        

        [HttpGet("getByPersonId/{personId}")]
        public async Task<IActionResult> GetByPersonId([FromRoute] int personId)
        {
            PermissionByUserDto permissionByUserDto = _permissionRepository.GetByPersonId(personId);
            return Ok(permissionByUserDto);
        }


        private void ValidatePermission(Permission permission)
        {
            PermissionValidator validator = new PermissionValidator();
            ValidationResult result = validator.Validate(permission);
            if (result.IsValid)
                return;
            throw new ArgumentException(result.Errors.ToString());
        }

    }

    public class PermissionToCreate
    {
        public int PersonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
    }

    public class PermissionToUpdate
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Reason { get; set; }
    }

    public class NameSurname
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
