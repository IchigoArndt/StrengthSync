using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using StrengthSync.Application;
using StrengthSync.Infra.Behaviours;

namespace StrengthSync.Infra.Extensions
{
    public static class MediatorExtensions
    {
        /// <summary>
        /// Registra os handlers do projeto Application (principal).
        /// </summary>
        /// <param name="services">
        /// Coleção de serviços para injeção de dependências.
        /// </param>
        /// <param name="container">
        /// Container do SimpleInjector utilizado para gerenciar as dependências.
        /// </param>
        public static void AddMediator(this IServiceCollection services, Container container)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(container);

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
            container.Collection.Register(typeof(IPipelineBehavior<,>),
                [typeof(ValidationPipeline<,>)]);

            var applicationAssembly = typeof(ApplicationModule).Assembly;
            container.Register(typeof(IRequestHandler<,>), [applicationAssembly], Lifestyle.Scoped);
        }
    }
}
