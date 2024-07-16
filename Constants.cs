using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTime
{
    public static class Constants
    {
        public static readonly List<string> SYSTEM_PROCESS_NAMES = new List<string>
        {
            "explorer.exe",
            "SearchUI.exe",
            "SystemSettings.exe",
            "ShellExperienceHost.exe",
            "dwm.exe",
            "sihost.exe",
            "ctfmon.exe",
            "Taskmgr.exe",
            "LockApp.exe",
            "SearchHost.exe"
            // Add more process names as needed
        };

        public static readonly List<string> SYSTEM_WINDOW_TITLES = new List<string>
        {
            "",
            "Program Manager",
            "Notification Center",
            "Volume Control"
            // Add more titles as needed
        };
    }
}
