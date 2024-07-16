using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScreenTime.DatabaseHelper;

namespace ScreenTime
{

    public class UsageAnalyzer
    {

        public Dictionary<string, int> CalculateAppUsageSeconds(List<AppUsageData> dataFromSpecificDay)
        {
            // Group by AppName and calculate total seconds used for each app
            var appUsageSeconds = dataFromSpecificDay
                .GroupBy(data => data.AppName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(data => (int)(data.EndTime - data.StartTime))
                );

            return appUsageSeconds;
        }
    }

}
