using StrenghtSync.Infra.SharedKernel;

namespace StrengthSync.Domain.Features.CalendarAppointments
{
    public interface ICalendarAppointmentRepository
    {
        public Task<Result<Exception, CalendarAppointment>> AddCalendar(CalendarAppointment calendar);
        public Task<Result<Exception, IQueryable<CalendarAppointment>>> GetCalendarByMonth(int month, int year, long userId);
    }
}
