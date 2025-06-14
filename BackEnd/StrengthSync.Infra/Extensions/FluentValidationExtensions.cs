using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using StrengthSync.Application;

namespace StrengthSync.Infra.Extensions
{
    /// <summary>
    /// Fornece métodos de extensão para registrar validadores do FluentValidation com o container SimpleInjector.
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Registra todas as implementações de <see cref="IValidator{T}"/> encontradas no assembly definido por <see cref="ApplicationModule"/>.
        /// </summary>
        /// <param name="services">
        /// A coleção de serviços da aplicação.
        /// </param>
        /// <param name="container">
        /// O container SimpleInjector utilizado para gerenciar as injeções de dependência.
        /// </param>
        public static void AddValidators(this IServiceCollection services, Container container)
        {
            container.Collection.Register(typeof(IValidator<>), typeof(ApplicationModule).Assembly);
        }
    }
}
