using MediatR;
using Microsoft.Extensions.Logging;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Application.Features.Users.Commands.Create;
using StrengthSync.Domain.Exceptions;
using StrengthSync.Domain.Features.Users;
using System.Text.Json;

namespace StrengthSync.Application.Features.Users.Handlers
{
    public class UserCreate(IUserRepository repository, ILogger<UserCreate> logger) : IRequestHandler<UserCreateCommand, Result<Exception, long>>
    {
        public async Task<Result<Exception, long>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Iniciado criação do usuario : {JsonSerializer.Serialize(request)}");

            UserCreateCommandValidator validator = new UserCreateCommandValidator();

            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
            {
                var messageError = validatorResult.Errors.First().ErrorMessage;

                logger.LogError($"Falha durante a validação do commando para a criação do usuario. Erro: {messageError}");

                return new BusinessException(ErrorCodes.InvalidObject, messageError);
            }

            logger.LogInformation($"Iniciando a busca por outro usuario com o nome {request.UserName}");

            var existUserName = await repository.ExisteUserName(request.UserName);

            if (existUserName)
            {
                logger.LogError("Usuario com o mesmo nome já cadastrado");

                return new BusinessException(ErrorCodes.AlreadyExists, "Já existe um usuario cadastrado com esse nome");
            }

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

            logger.LogInformation("Realizado a criação do usuario");

            var userResult = await repository.AddUser(newUser);

            if (userResult.IsFailure)
            {
                logger.LogError($"Falha ao criar o usuario. Erro: {userResult.Failure.Message}");

                return new Exception(userResult.Failure.Message);
            }

            return userResult.Success.Id;
        }
    }
}
