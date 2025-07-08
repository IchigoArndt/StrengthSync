using MediatR;
using StrenghtSync.Infra.SharedKernel;

namespace StrengthSync.Application.Features.Users.Commands
{
    public class UserLoginCommand : IRequest<Result<Exception, string>>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
