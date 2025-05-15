namespace TKILSAPRFC.Core.Helpers
{
    public static class Util
    {
        public static string FullName(this string firstName, string lastName) => (firstName + " " + lastName).Trim();

        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageSize, int pageNumber) => source.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        public static IQueryable<T> Page<T>(this IQueryable<T> source, int pageSize, int pageNumber) => source.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        public static PagedResult<T> PagedResult<T>(this IQueryable<T> source, int pageSize, int pageNumber) => new(source, pageNumber, pageSize, source.Count());

        public static PagedResult<T> PagedResult<T>(this IEnumerable<T> source, int pageSize, int pageNumber) => new(source, pageNumber, pageSize, source.Count());

        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static decimal TimeRoundUp(int minutes)
        {
            if (minutes > 0 && minutes <= 15)
            {
                return 25;
            }
            else if (minutes > 15 && minutes <= 30)
            {
                return 50;
            }
            else if (minutes > 30 && minutes <= 45)
            {
                return 75;
            }
            else if (minutes > 45 && minutes <= 59)
            {
                return 100;
            }
            return 0;
        }

        public static decimal DiffDate(TimeSpan timeSpan)
        {
            var hours = timeSpan.Days > 0 ? (timeSpan.Days * 24) + timeSpan.Hours : timeSpan.Hours;
            var minutes = TimeRoundUp(timeSpan.Minutes);
            if (minutes == 100)
            {
                hours += 1;
                minutes = 0;
            }
            return Convert.ToDecimal(hours + "." + minutes);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime DateOfNextWeek(this DateTime dt)
        {
            return dt.AddDays(6);
        }
        public static string ConcateUrlSegments(this string baseUrl, string methodPart)
        {
            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(methodPart))
            {
                return string.Empty;
            }
            var forSlash = '/';
            baseUrl = baseUrl.Trim(new char[] { forSlash });
            methodPart = methodPart.Trim(new char[] { forSlash });
            return $"{baseUrl}{forSlash}{methodPart}";
        }
        public static string ConcateUrlSegmentsWithForwardSlash(this string baseUrl, string methodPart)
        {
            if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(methodPart))
            {
                return string.Empty;
            }
            var forSlash = '\\';
            baseUrl = baseUrl.Trim(new char[] { forSlash });
            methodPart = methodPart.Trim(new char[] { forSlash });
            return $"{baseUrl}{forSlash}{methodPart}";
        }

    }
}
