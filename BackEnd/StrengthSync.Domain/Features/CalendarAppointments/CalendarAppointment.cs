using StrengthSync.Domain.Base;
using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Domain.Features.CalendarAppointments
{
    public class CalendarAppointment : BaseEntity
    {
        public DateTime Date { get; set; }
        public bool Trained { get; set; }

        //FK
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
