using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StrengthSync.Application;
using StrengthSync.Infra.Data;
using System.Reflection;

namespace StrengthSync.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registra o DbContext
        /// e os repositórios (via reflexão) usando a connection string lida de AppSettings.
        /// </summary>
        /// <param name="services">O container de DI.</param>
        /// <exception cref="Exception">Lançada se a seção AppSettings não estiver configurada.</exception>
        public static void AddDbServices(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("Connection_Mongo");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection String não definida");

            var mongoClient = new MongoClient(connectionString);

            // Registrando o cliente no contêiner de serviços
            services.AddSingleton<IMongoClient>(mongoClient);

            // Definindo o banco de dados específico
            services.AddScoped(sp => mongoClient.GetDatabase("StrengthSync"));

            AddRepositories(services);
        }

        /// <summary>
        /// Registra os repositórios automaticamente, escaneando o assembly que contém o marker InfraDataModule.
        /// </summary>
        /// <param name="services">O container de DI.</param>
        private static void AddRepositories(IServiceCollection services)
        {
            var repositoryAssembly = typeof(InfraDataModule).Assembly;
            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Name.EndsWith("Repository")
                from service in type.GetInterfaces()
                select new { service, implementation = type };

            foreach (var reg in registrations)
                services.AddTransient(reg.service, reg.implementation);
        }

        /// <summary>
        /// Realiza as configurações necessarias para o AutoMapper
        /// </summary>
        /// <typeparam name="TProgram">O tipo da classe Program da API que está chamando o método</typeparam>
        /// <param name="services">A coleção de serviços da API</param>
        public static void AddMapper<TProgram>(this IServiceCollection services)
        {
            var aplicationAssembly = Assembly.GetAssembly(typeof(ApplicationModule));
            var programAssembly = Assembly.GetAssembly(typeof(TProgram));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapMethod = _ => false;
                cfg.AddMaps(aplicationAssembly);
                cfg.AddMaps(programAssembly);
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
