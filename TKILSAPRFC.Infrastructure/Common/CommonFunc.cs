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

        public static byte[] DecodeStringToBytes(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return Array.Empty<byte>();

            
            try
            {
                if (data.Length % 2 != 0)
                    throw new FormatException("Invalid hex string length.");

                byte[] bytes = new byte[data.Length / 2];
                for (int i = 0; i < data.Length; i += 2)
                    bytes[i / 2] = Convert.ToByte(data.Substring(i, 2), 16);
                return bytes;
            }
            catch { }

            throw new FormatException("String is neither valid Base64 nor Hex.");
        }

    }
}
