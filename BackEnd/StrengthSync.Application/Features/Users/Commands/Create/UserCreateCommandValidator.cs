using FluentValidation;

namespace StrengthSync.Application.Features.Users.Commands.Create
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        {
            #region Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O Email é um campo obrigatorio e não pode ser vazio")
                .MinimumLength(2).WithMessage("O Email deve ter no minimo 2 caracteres")
                .Must(email => email.Contains("@") && email.Contains(".")).WithMessage("O Email precisa conter os caracteres '@' e '.'");
            #endregion

            #region Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A Senha é um campo obrigatorio e não pode ser vazio")
                .MinimumLength(8).WithMessage("A Senha deve ter no minimo 8 caracteres");
            #endregion

            #region UserName
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("O Nome de usuario é um campo obrigatorio e não pode ser vazio");
            #endregion
        }
    }
}
