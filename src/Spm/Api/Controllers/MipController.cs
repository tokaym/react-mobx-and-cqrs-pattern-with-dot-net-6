using Application.Features.Auths.Commands;
using Application.Features.Mips.Commands;
using Application.Features.Mips.Queries;
using Application.Features.Users.Queries;
using Core.Application.Requests;
using Core.Ldap;
using Core.LDAP;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Mip")]
    public class MipController : BaseController
    {

        public MipController()
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Guid _id = new Guid(id);
            GetMipByIdQuery getMipByIdQuery = new() { id = _id };
            var result = await Mediator.Send(getMipByIdQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest, [FromQuery] OrderByRequest orderByRequest, string? search)
        {
            GetMipListQuery getMipListQuery = new() { PageRequest = pageRequest, OrderByRequest = orderByRequest, Search = search == null ? "" : search };
            var result = await Mediator.Send(getMipListQuery);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Mip _mip)
        {
            CreateMipCommand createMipCommand = new() { mip = _mip };
            var result = await Mediator.Send(createMipCommand);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Guid _id = new Guid(id);
            DeleteMipCommand deleteMipCommand = new() { id = _id };
            var result = await Mediator.Send(deleteMipCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Mip _mip)
        {
            UpdateMipCommand updateMipCommand = new() { mip = _mip };
            var result = await Mediator.Send(updateMipCommand);
            return Ok(result);
        }
    }
}
