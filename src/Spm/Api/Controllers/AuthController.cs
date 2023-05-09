using Application.Features.Auths.Commands;
using Application.Features.Users.Queries;
using Core.Application.Requests;
using Core.Ldap;
using Core.LDAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        GeneralBusiness _generalBusiness;

        public AuthController()
        {
            _generalBusiness = new GeneralBusiness();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var loginResult = await Mediator.Send(loginUserCommand);

            return Ok(loginResult);
        }

        //Yeni kayıt ekleme...
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var registerResult = await Mediator.Send(command);
            return Created("", registerResult);
        }
    }
}
