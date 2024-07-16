using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTime
{
    public class CalendarHelper
    {
        
        /*
         *  -----------------------------BIG DISCLAIMER-----------------------------
         *  -10800 in lines 27 and 47 corresponds to my timezone which now is GMT+3. Will add feature to configure that in future
         * 
         */
        public long GetDayStart(string dateString)
        {
            DateTime dateTime;

            // Parse the date string into a DateTime object
            if (DateTime.TryParse(dateString, out dateTime))
            {
                // Get the local timezone
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                // Get the start of the day in the local timezone
                DateTimeOffset startOfDay = new DateTimeOffset(dateTime.Date, localTimeZone.GetUtcOffset(dateTime.Date));
                long startOfDayUnix = startOfDay.ToUnixTimeSeconds();

                Properties.Settings.Default.UserTimeZone = localTimeZone.Id;
                Properties.Settings.Default.Save();

                Debug.WriteLine($"Timezone saved: {localTimeZone.Id}");


                return startOfDayUnix;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
                return 0;
            }
        }

        public long GetDayEnd(string dateString)
        {
            DateTime dateTime;

            // Parse the date string into a DateTime object
            if (DateTime.TryParse(dateString, out dateTime))
            {
                // Get the local timezone
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                // Get the end of the day (one second before midnight the next day) in the local timezone
                DateTimeOffset endOfDay = new DateTimeOffset(dateTime.Date.AddDays(1).AddSeconds(-1), localTimeZone.GetUtcOffset(dateTime.Date.AddDays(1).AddSeconds(-1)));
                long endOfDayUnix = endOfDay.ToUnixTimeSeconds();
                return endOfDayUnix;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
                return 0;
            }
        }

    }
}
