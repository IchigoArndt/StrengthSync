using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Infra.Data.Contexts
{
    public class StrengthSyncDbContext(DbContextOptions<StrengthSyncDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }

    }
}
