using MediatR;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Application.Features.Users.DTOs;

namespace StrengthSync.Application.Features.Users.Commands
{
    public class UserLoginCommand : IRequest<Result<Exception, UserLoginDto>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
