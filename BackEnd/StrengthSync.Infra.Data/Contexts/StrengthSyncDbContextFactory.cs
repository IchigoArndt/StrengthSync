using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthSync.Infra.Data.Contexts
{
    /// <summary>
    /// Factory usada em tempo de design (CLI) para evitar que o EF Core execute o Program.cs.
    /// </summary>
    public class StrengthSyncDbContextFactory : IDesignTimeDbContextFactory<StrengthSyncDbContext>
    {
        public StrengthSyncDbContext CreateDbContext(string[] args)
        {
            // Pega o caminho da raiz do projeto que contém o appsettings
            var basePath = Directory.GetCurrentDirectory();

            // Carrega appsettings.Development.json (pode ajustar se quiser .json específico para EF)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            // Constrói opções do DbContext com a connection string
            var optionsBuilder = new DbContextOptionsBuilder<StrengthSyncDbContext>();
            optionsBuilder.UseSqlServer(configuration["AppSettings:ConnectionString"]);

            return new StrengthSyncDbContext(optionsBuilder.Options);
        }
    }
}
