using MediatR;
using Microsoft.IdentityModel.Tokens;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Application.Features.Users.Commands;
using StrengthSync.Application.Features.Users.DTOs;
using StrengthSync.Domain.Exceptions;
using StrengthSync.Domain.Features.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StrengthSync.Application.Features.Users.Handlers
{
    public class UserLogin
    {
        public class UserLoginHandler(IUserRepository repository) : IRequestHandler<UserLoginCommand, Result<Exception, UserLoginDto>>
        {
            public async Task<Result<Exception, UserLoginDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
            {
                var user = await repository.GetUserLoginAsync(request.Username);

                if (user == null)
                    return new BusinessException(ErrorCodes.NotFound, "Usuario não cadastrado");

                var correctPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

                if (!correctPassword)
                    return new BusinessException(ErrorCodes.Unauthorized, "Senha Incorreta");

                var permissions = await repository.GetPermissionsByUserId(user._id);

                if (!permissions.Any())
                    return new BusinessException(ErrorCodes.InvalidObject, "Nenhuma permissão cadastrada");

                var tokenResult = await GenerateToken(user.Username, permissions);

                if (tokenResult.IsFailure)
                    return new Exception(tokenResult.Failure.Message);

                return new UserLoginDto
                {
                    Token = tokenResult.Success
                };
            }

            private async Task<Result<Exception, string>> GenerateToken(string UserName, List<string> Claims)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var secretKey = Environment.GetEnvironmentVariable("Secret") ?? throw new InvalidOperationException("Secret não configurado.");
                    var key = Encoding.UTF8.GetBytes(secretKey);

                    var claimList = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserName),
                    };

                    foreach (var claim in Claims)
                    {
                        claimList.Add(new Claim("custom_claim", claim));
                    }

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claimList),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                        Issuer = "StrengthSync",
                        Audience = "UserStrengthSync"
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return tokenHandler.WriteToken(token);
                }
                catch (Exception ex)
                {
                    return new Exception(ex.Message);
                }

            }
        }

    }
}
