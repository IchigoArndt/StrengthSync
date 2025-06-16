using MediatR;
using StrenghtSync.Infra.SharedKernel;

namespace StrengthSync.Application.Features.Users.Commands.Create
{
    public class UserCreateCommand : IRequest<Result<Exception, long>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public bool IsInstructor { get; set; }

        public bool IsGymStudent { get; set; }
    }
}
