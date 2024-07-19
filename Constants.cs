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
            "SearchHost.exe",
            "Rainmeter.exe",
            "ApplicationFrameHost.exe",
            "FortniteClient-Win64-Shipping_EAC_EOS.exe"
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

        public static readonly List<string> CODING_TITLES = new List<string>
        {
            "Code.exe",
            "devenv.exe"
        };

        public static readonly List<string> GAMING_TITLES = new List<string>
        {
            "VALORANT-Win64-Shipping.exe",
            "FortniteClient-Win64-Shipping.exe"
        };

        public static readonly List<string> BRAINROT_TITLES = new List<string>
        {
            "YouTube (chrome.exe)",
            "/ X (chrome.exe)",
            "Reddit (chrome.exe)",
            "4chan (chrome.exe)",
            "on X: (chrome.exe)"
        };


    }
}
