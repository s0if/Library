using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.APPLICATION.Service
{
    public class TimeZone
    {
        public DateTime GetPalestineTime()
        {
            TimeZoneInfo _palestineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Gaza");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _palestineTimeZone);
        }
    }
}
