using DnsClient.Internal;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Application.Behaviors;
using StrengthSync.Application.Features.CalendarAppointments.DTOs;
using StrengthSync.Domain.Exceptions;
using StrengthSync.Domain.Features.CalendarAppointments;
using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Application.Features.CalendarAppointments.Handlers
{
    public class CalendarAppointmentCollection
    {
        public class Query : IRequestWithResult<IQueryable<CalendarAppointmentDto>>
        {
            public int Month { get; set; }

            public int Day { get; set; }

            public long UserId { get; set; }

            public ValidationResult Validate() => new Validator().Validate(this);

            public class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(x => x.UserId)
                        .NotEmpty().WithMessage("O Id do usuario não pode ser vazio")
                        .GreaterThan(1).WithMessage("O Id do usuario precisa ser maior que 1");

                    RuleFor(x => x.Month)
                        .NotEmpty().WithMessage("O Mês é obrigatório")
                        .GreaterThanOrEqualTo(1).WithMessage("O Mês tem que ser maior que 1")
                        .LessThanOrEqualTo(12).WithMessage("O Mês não poder ser maior que 12");

                    RuleFor(x => x.Day)
                        .NotEmpty().WithMessage("O Dia é obrigatório")
                        .GreaterThanOrEqualTo(1).WithMessage("O Dia tem que ser maior que 1")
                        .LessThanOrEqualTo(31).WithMessage("O Dia não pode ser maior que 31");
                }
            }
        }

        public class Handler(ICalendarAppointmentRepository repositoryCalendar,IUserRepository repositoryUser, ILogger<Handler> logger) : IRequestHandler<Query, Result<Exception, IQueryable<CalendarAppointmentDto>>>
        {
            public async Task<Result<Exception, IQueryable<CalendarAppointmentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var resultUser = await repositoryUser.GetUserById(request.UserId);

                if (resultUser.IsFailure)
                    return new BusinessException(ErrorCodes.Unhandled, resultUser.Failure.Message);

                if (resultUser.Success == null)
                {
                    logger.LogError("Falha, usuario não encontrado");

                    return new BusinessException(ErrorCodes.NotFound, "Usuario não encontrado");
                }

                var user = resultUser.Success;

                logger.LogInformation($"Buscando o calendario para o usuario {user.Username}");

                var resultCalendar = await repositoryCalendar.GetCalendarByMonth(request.Month, request.Month, user.Id);

                if (resultCalendar.IsFailure)
                {
                    logger.LogError($"Falha ao buscar o calendario para o mês {request.Month} e para o usuario {user.Username}. Erro: {resultCalendar.Failure.Message}");

                    return new Exception(resultCalendar.Failure.Message);
                }

                if (!resultCalendar.Success.Any())
                {
                    logger.LogError($"Falha calendario não encontrado para o mês {request.Month} e para o usuario {user.Username}");

                    return new BusinessException(ErrorCodes.NotFound, "Calendario não encontrado");
                }

                var calendars = resultCalendar.Success;

                var CalendarAppointmentDtos = calendars.Select(calendar => new CalendarAppointmentDto
                {
                    Id = calendar.Id,
                    Date = calendar.Date,
                    Trained = calendar.Trained,
                }).ToList();

                return CalendarAppointmentDtos.AsQueryable().AsResult();
            }
        }
    }
}
