using Application.Features.Auths.Commands;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Core.Application.Requests;
using Core.Ldap;
using Core.LDAP;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Kullanıcı İşlemleri")]
    public class UserController : BaseController
    {
        GeneralBusiness _generalBusiness;

        public UserController()
        {
            _generalBusiness = new GeneralBusiness();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Guid _id = new Guid(id);
            GetUserByIdQuery getUserByIdQuery = new() { Id = _id };
            var result = await Mediator.Send(getUserByIdQuery);
            return Ok(result);
        }
        

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest, [FromQuery] OrderByRequest orderByRequest, string? search)
        {
            GetUserListQuery getUserListQuery = new() { PageRequest = pageRequest, OrderByRequest = orderByRequest, Search = search == null ? "" : search };
            var result = await Mediator.Send(getUserListQuery);
            return Ok(result);
        }


        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] UserForRegisterDto userForRegisterDto)
        // {
        //     CreateUserCommand createUserCommand = new() { RegisterDto = userForRegisterDto };
        //     var result = await Mediator.Send(createUserCommand);
        //     return Ok(result);
        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Guid _id = new Guid(id);
            DeleteUserCommand deleteUserCommand = new() { Id = _id };
            var result = await Mediator.Send(deleteUserCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserRegistrationUpdateDto _user)
        {
            UpdateUserCommand updateUserCommand = new() { UserRegistrationUpdateDto = _user };
            var result = await Mediator.Send(updateUserCommand);
            return Ok(result);
        }
    }
}
