using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StrenghSync.Api.Base;

namespace StrenghSync.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    [Tags("User")]
    public class UserController(IMapper mapper, IMediator mediator) : ApiControllerBase(mapper)
    {
    }
}
