using DataAccessLibrary.Helpers.Interfaces;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateRangeModel GetCurrentMonthRange()
        {
            var date = DateTime.Today;
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddTicks(-1);

            return new DateRangeModel()
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}
