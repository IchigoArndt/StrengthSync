using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StrenghSync.Api.Base;
using StrengthSync.Application.Features.Users.Commands;
using StrengthSync.Application.Features.Users.Commands.Create;
using StrengthSync.Application.Features.Users.Handlers;
using System.ComponentModel;
using System.Threading.Tasks;

namespace StrenghSync.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    [Tags("User")]
    public class UserController(IMapper mapper, IMediator mediator) : ApiControllerBase(mapper)
    {
        [HttpPost("Login")]
        [Description("Realiza o login de um usuario")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
        {
            var response = await mediator.Send(command);

            return HandleCommand(response);
        }

        [HttpPost("Create")]
        [Description("Realiza a criação de um usuario no sistema")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand command)
        {
            var response = await mediator.Send(command);

            return HandleCommand(response);
        }
    }
}
