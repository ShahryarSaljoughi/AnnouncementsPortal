using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Helper
{
    public static class LocalizationUtils
    {
        public static DateTimeOffset ToPersianTime(this DateTimeOffset input)
        {
            var localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(input, "Iran Standard Time");
            
            return localTime;
        }

        public static string ToPersianDate(this DateTimeOffset input)
        {
            var localTime = input.ToPersianTime().DateTime;
            var persianCalendar = new PersianCalendar();
            var day = persianCalendar.GetDayOfMonth(localTime);
            var month = persianCalendar.GetMonth(localTime);
            var year = persianCalendar.GetYear(localTime);
            return $"{year}/{month}/{day}";
        }

        public static string ToPersianNum(this string input)
        {
            var result = input;
            var mapper = new Dictionary<string, string>()
            {
                {1.ToString(), "۱" },// ه34567890
                {2.ToString(), "۲"},
                {3.ToString(), "۳"},
                {4.ToString(), "۴"},
                {5.ToString(), "۵"},
                {6.ToString(), "۶"},
                {7.ToString(), "۷"},
                {8.ToString(), "۸"},
                {9.ToString(), "۹"},
                {0.ToString(), "۰"},
            };
            foreach (var c in mapper)
            {
                result = result.Replace(c.Key, c.Value);
            }
            return result;
        }
    }
}
