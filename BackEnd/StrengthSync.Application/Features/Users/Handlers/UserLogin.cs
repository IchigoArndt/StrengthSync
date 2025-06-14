using MediatR;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Application.Features.Users.Commands;
using StrengthSync.Application.Features.Users.DTOs;
using StrengthSync.Domain.Exceptions;
using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Application.Features.Users.Handlers
{
    public class UserLogin
    {
        public class UserLoginHandler(IUserRepository repository) : IRequestHandler<UserLoginCommand, Result<Exception, UserLoginDto>>
        {
            public async Task<Result<Exception, UserLoginDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
            {
                var user = repository.GetUserLogin(request.Email, request.Password);

                if (user == null)
                    return new BusinessException(ErrorCodes.NotFound, "Usuario não cadastrado");

                var permissions = repository.GetPermissionsByUserId(user.Id);

                if (!permissions.Any())
                    return new BusinessException(ErrorCodes.InvalidObject, "Nenhuma permissão cadastrada");



                throw new NotImplementedException();
            }
        }
    }
}
