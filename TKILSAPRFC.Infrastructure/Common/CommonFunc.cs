using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TKILSAPRFC.Infrastructure.Common
{
    public class CommonFunc
    {
        public static string GetCurrentIndiaTimeFormatted()
        {
            DateTime utcNow = DateTime.UtcNow;
            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "India Standard Time"
                : "Asia/Kolkata";

            TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime istTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istZone);

            return istTime.ToString("hh:mm:ss tt _ dd-MMM-yyyy");
        }
    }
}
