using Application.Features.Permissions.Commands.AddPermission;
using Application.Features.Permissions.Commands.DeletePermission;
using Application.Features.Permissions.Commands.UpdatePermission;
using Application.Features.Permissions.Dtos;
using Application.Features.Permissions.Models;
using Application.Features.Permissions.Queries.GetAllPermission;
using Application.Features.Permissions.Queries.GetByIdPermission;
using Application.Features.Permissions.Queries.GetPermissionByPersonId;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PermitRegistrationSystem.Validation;

namespace PermitRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PermissionToCreateDto permissionToCreateDto)
        {
            AddPermissionCommand command = new() { PermissionToCreateDto = permissionToCreateDto };
            AddedPermissionDto result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletePermissionCommand command = new() { Id = id };
            DeletedPermissionDto result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] PermissionToUpdateDto permissionToUpdateDto)
        {
            UpdatePermissionCommand command = new() { PermissionToUpdateDto = permissionToUpdateDto };
            UpdatedPermissionDto result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            GetAllPermissionQuery query = new();
            GetAllPermissionsModel model = await Mediator.Send(query);
            return Ok(model);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdPermissionQuery query = new GetByIdPermissionQuery() { Id = id };
            GetByIdPermissionDto result = await Mediator.Send(query);
            return Ok(result);
        }

        

        [HttpGet("getByPersonId/{personId}")]
        public async Task<IActionResult> GetByPersonId([FromRoute] int personId)
        {
            GetPermissionsByPersonIdQuery query = new GetPermissionsByPersonIdQuery() { PersonId = personId };
            GetPermissionsByPersonIdModel model = await Mediator.Send(query);
            return Ok(model);
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
