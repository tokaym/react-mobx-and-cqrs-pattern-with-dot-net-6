using Application.Features.MainReports.Queries;
using Application.Features.MaterialGroups.Commands;
using Application.Features.MaterialGroups.Queries;
using Core.Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Malzeme Grubu")]
    public class MaterialGroupController : BaseController
    {

        public MaterialGroupController()
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Guid _id = new Guid(id);
            GetMaterialGroupByIdQuery getMaterialGroupByIdQuery = new() { id = _id };
            var result = await Mediator.Send(getMaterialGroupByIdQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest, [FromQuery] OrderByRequest orderByRequest, string? search)
        {
            GetMaterialGroupListQuery getMaterialGroupListQuery = new() { PageRequest = pageRequest, OrderByRequest = orderByRequest, Search = search == null ? "" : search };
            var result = await Mediator.Send(getMaterialGroupListQuery);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MaterialGroup _materialGroup)
        {
            CreateMaterialGroupCommand createMaterialGroupCommand = new (){materialGroup = _materialGroup};
            var result = await Mediator.Send(createMaterialGroupCommand);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Guid _id = new Guid(id);
            DeleteMaterialGroupCommand deleteMaterialGroupCommand = new() { id = _id };
            var result = await Mediator.Send(deleteMaterialGroupCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Delete([FromBody] MaterialGroup _materialGroup)
        {
            UpdateMaterialGroupCommand updateMaterialGroupCommand = new (){materialGroup = _materialGroup};
            var result = await Mediator.Send(updateMaterialGroupCommand);
            return Ok(result);
        }
    }
}
