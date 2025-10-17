using System.Collections;
using System.Globalization;

namespace Rasta.ProjectManagment
{
    public static class Extentions
    {
        public static string ConvertToPersianDate(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date)}/{persianCalendar.GetDayOfMonth(date)}";
        }
    }
}
