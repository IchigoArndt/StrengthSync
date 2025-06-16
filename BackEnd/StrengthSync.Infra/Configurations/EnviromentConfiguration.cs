using Microsoft.Extensions.Configuration;

namespace StrengthSync.Infra.Configurations
{
    public class EnviromentConfiguration
    {
        private static IConfiguration _configuration;

        /// <summary>
        /// Configura o provider de configuração que será utilizado pelas propriedades estáticas desta classe.
        /// </summary>
        /// <param name="configuration">Instância de <see cref="IConfiguration"/> que contém as configurações da aplicação.</param>
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
