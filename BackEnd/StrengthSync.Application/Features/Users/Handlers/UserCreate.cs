using MediatR;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Application.Features.Users.Commands.Create;
using StrengthSync.Domain.Exceptions;
using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Application.Features.Users.Handlers
{
    public class UserCreate(IUserRepository repository) : IRequestHandler<UserCreateCommand, Result<Exception, long>>
    {
        public async Task<Result<Exception, long>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            UserCreateCommandValidator validator = new UserCreateCommandValidator();

            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return new BusinessException(ErrorCodes.InvalidObject, validatorResult.Errors.First().ErrorMessage);

            var existUserName = await repository.ExisteUserName(request.UserName);

            if (existUserName)
                return new BusinessException(ErrorCodes.AlreadyExists, "Já existe um usuario cadastrado com esse nome");

            var cryptPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = cryptPassword;

            var newUser = new User()
            {
                Email = request.Email,
                Password = request.Password,
                Username = request.UserName,
                IsGymStudent = request.IsGymStudent,
                IsInstructor = request.IsInstructor,
            };

            var userResult = await repository.AddUser(newUser);

            if (userResult.IsFailure)
                return new Exception(userResult.Failure.Message);

            return userResult.Success.Id;
        }
    }
}
