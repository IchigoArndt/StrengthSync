using MediatR;
using StrenghtSync.Infra.SharedKernel;

namespace StrengthSync.Application.Behaviors
{
    public interface IRequestWithResult<TResponse> : IRequest<Result<Exception, TResponse>>
    {
    }
}
