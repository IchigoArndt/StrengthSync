using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Domain.Features.CalendarAppointments;
using StrengthSync.Infra.Data.Contexts;

namespace StrengthSync.Infra.Data.Features.CalendarAppointments
{
    public class CalendarAppointmentRepository(StrengthSyncDbContext context) : ICalendarAppointmentRepository
    {
        public async Task<Result<Exception, CalendarAppointment>> AddCalendar(CalendarAppointment calendar)
        {
            context.Calendars.Add(calendar);

            try
            {
                calendar.Id = await context.SaveChangesAsync();

                return calendar;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Result<Exception, IQueryable<CalendarAppointment>>> GetCalendarByMonth(int month, int year, long userId)
        {
            try
            {
                var lastDayMonth = DateTime.DaysInMonth(year, month);

                var dateFinalMonth = new DateTime(year, month, lastDayMonth);

                var dateInicialMonth = new DateTime(year, month, 1);

                var Calendars = context.Calendars.Where(x => x.Date >= dateInicialMonth
                                                 && x.Date <= dateInicialMonth
                                                 && x.UserId == userId).AsQueryable();

                return Calendars.AsResult();
            }
            catch (Exception ex)
            {
                return ex;
            }

        }
    }
}
