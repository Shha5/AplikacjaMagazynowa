using DataAccessLibrary.Models;

namespace DataAccessLibrary.Helpers.Interfaces
{
    public interface IDateTimeHelper
    {
        DateRangeModel GetCurrentMonthRange();
    }
}