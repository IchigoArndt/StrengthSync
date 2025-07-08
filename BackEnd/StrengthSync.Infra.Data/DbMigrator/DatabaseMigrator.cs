using Microsoft.EntityFrameworkCore;
using StrengthSync.Infra.Data.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace StrengthSync.Infra.Data.DbMigrator
{
    /// <summary>
    /// Responsável por aplicar as migrations pendentes no banco de dados.
    /// </summary>
    [ExcludeFromCodeCoverage]

    public class DatabaseMigrator(StrengthSyncDbContext context)
    {
        public async Task MigrateAsync()
        {
            var db = context.Database;
            if (!db.IsInMemory() && db.GetPendingMigrations().Any())
                await db.MigrateAsync();
        }
    }
}
