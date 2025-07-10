using Microsoft.EntityFrameworkCore;
using StrengthSync.Domain.Features.CalendarAppointments;
using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Infra.Data.Contexts
{
    public class StrengthSyncDbContext(DbContextOptions<StrengthSyncDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CalendarAppointment> Calendars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }

    }
}
