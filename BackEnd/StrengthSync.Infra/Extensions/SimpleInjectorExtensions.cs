using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace StrengthSync.Infra.Extensions
{
    /// <summary>
    /// Fornece métodos de extensão para integrar o container do SimpleInjector com a coleção de serviços padrão do ASP.NET Core.
    /// Essa integração pode ser configurada para suportar funcionalidades de aplicações web, como a ativação de controllers.
    /// </summary>
    public static class SimpleInjectorExtensions
    {
        /// <summary>
        /// Adiciona e configura o SimpleInjector na coleção de serviços <see cref="IServiceCollection"/>, 
        /// integrando-o com o ASP.NET Core e, opcionalmente, registrando os componentes necessários para aplicações web.
        /// </summary>
        /// <param name="services">
        /// A coleção de serviços do .NET na qual o SimpleInjector será integrado.
        /// </param>
        /// <param name="container">
        /// A instância do <see cref="Container"/> do SimpleInjector que gerenciará as dependências da aplicação.
        /// </param>
        /// <param name="isWebApp">
        /// Indica se a aplicação é uma aplicação web. Se definido como <c>true</c>,
        /// o método registra serviços adicionais, como o MVC Core e a ativação de controllers, necessários para aplicações web.
        /// Caso contrário, a integração se limita ao básico.
        /// </param>
        public static void AddSimpleInjector(this IServiceCollection services, Container container, bool isWebApp = false)
        {
            // Se o projeto for uma aplicação web, registra componentes essenciais para o MVC Core
            if (isWebApp)
                services.AddMvcCore();

            // Integra o SimpleInjector com a IServiceCollection, permitindo configurações adicionais via lambda.
            // Se for uma aplicação web, configura a integração com o ASP.NET Core para ativação de controllers.
            services.AddSimpleInjector(container, options =>
            {
                if (isWebApp)
                    options.AddAspNetCore().AddControllerActivation();
            });
        }
    }
}
