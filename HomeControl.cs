using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScreenTime
{
    public partial class HomeControl : UserControl
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        string finString;
        long start;
        long end;
        string currentApp;
        int today = DateTimeOffset.UtcNow.Day;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


        public HomeControl()
        {
            InitializeComponent();
            InitializeTimer();
            TodaysUsage();
            BarGenerator barGenerator = new BarGenerator();
            lblColumn.Text = barGenerator.Generate(45);
            lblColumn2.Text = barGenerator.Generate(40);
            lblColumn3.Text = barGenerator.Generate(45);
            lblColumn4.Text = barGenerator.Generate(30);
            lblColumn5.Text = barGenerator.Generate(15);
            lblColumn6.Text = barGenerator.Generate(55);
            lblColumn7.Text = barGenerator.Generate(20);
            lblColumn8.Text = barGenerator.Generate(30);
            lblColumn9.Text = barGenerator.Generate(10);
            lblColumn10.Text = barGenerator.Generate(50);
            lblColumn11.Text = barGenerator.Generate(5);
            lblColumn12.Text = barGenerator.Generate(60);

        }

        private void InitializeTimer()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer)sender;
            timer.Stop();
            int currentDay = DateTimeOffset.UtcNow.Day;
            try
            {
                IntPtr handle = GetForegroundWindow();
                StringBuilder windowText = new StringBuilder(256);
                if (GetWindowText(handle, windowText, 256) > 0)
                {
                    string activeApp = windowText.ToString();

                    // Extract the part after the last hyphen
                    string simpleAppName = GetSimplifiedAppName(activeApp);

                    // Get the process ID of the active window
                    GetWindowThreadProcessId(handle, out uint processId);
                    string processName = GetProcessNameById((int)processId);

                    finString = $"{simpleAppName} ({processName})";



                    // Check if the process name or window title is in the exclusion lists
                    if (Constants.SYSTEM_PROCESS_NAMES.Contains(processName) || Constants.SYSTEM_WINDOW_TITLES.Contains(simpleAppName))
                    {
                        // If the current app is being tracked, stop tracking it
                        if (currentApp != null)
                        {
                            end = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                            Debug.WriteLine($"{currentApp}, Start-{start}, End-{end}");
                            await Task.Run(() => DatabaseHelper.InsertDataAsync(currentApp, start, end));
                            currentApp = null; // Reset currentApp
                        }
                        appString.Text = "Tracking paused for system app";
                        return; // Skip tracking this application
                    }

                    if (currentDay != today)
                    {
                        if (currentApp != null)
                        {
                            end = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 1;
                            Debug.WriteLine($"{currentApp}, Start-{start}, End-{end}");
                            await Task.Run(() => DatabaseHelper.InsertDataAsync(currentApp, start, end));
                        }

                        today = currentDay;
                    }



                    if (currentApp != $"{simpleAppName} ({processName})")
                    {
                        if (currentApp != null)
                        {
                            end = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                            Debug.WriteLine($"{currentApp}, Start-{start}, End-{end}");
                            await Task.Run(() => DatabaseHelper.InsertDataAsync(currentApp, start, end));
                        }

                        currentApp = $"{simpleAppName} ({processName})";
                        start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    }

                    appString.Text = $"Currently focused on: \n{finString}";
                }
            }
            finally
            {
                timer.Start();
            }
        }

        private string GetSimplifiedAppName(string fullAppName)
        {
            int lastHyphenIndex = fullAppName.LastIndexOf('-');
            if (lastHyphenIndex >= 0 && lastHyphenIndex < fullAppName.Length - 1)
            {
                return fullAppName.Substring(lastHyphenIndex + 1).Trim();
            }
            return fullAppName; // Return the full name if no hyphen is found
        }

        private string GetProcessNameById(int processId)
        {
            try
            {
                var process = System.Diagnostics.Process.GetProcessById(processId);
                return process.ProcessName + ".exe";
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        
        public void HandleFormClosing()
        {
            cancellationTokenSource.Cancel();
            // End the current session with the current timestamp
            if (!string.IsNullOrEmpty(currentApp))
            {
                end = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                Debug.WriteLine($"{currentApp}, Start-{start}, End-{end}");
                Task.Run(() => DatabaseHelper.InsertDataAsync(currentApp, start, end)); 
            }
            cancellationTokenSource.Dispose();
        }

        public void TodaysUsage()
        {
            DateTime currentDate = DateTime.Now;
            
            string bruh = currentDate.ToString("dd/MM/yyyy");
            string updateTime = currentDate.ToString("dd/MM/yyyy HH:mm");
            CalendarHelper calendarHelper = new CalendarHelper();
            long startDay = calendarHelper.GetDayStart(bruh);
            long endDay = calendarHelper.GetDayEnd(bruh);
            Debug.WriteLine($"{startDay}, {endDay}");

            var analyzer = new UsageAnalyzer();

            var dataFromDay = DatabaseHelper.GetDataFromSpecificDay(startDay, endDay);

            var appUsageSeconds = analyzer.CalculateAppUsageSeconds(dataFromDay);

            string savedTimeZone = Properties.Settings.Default.UserTimeZone;

            string testing = $"Last update: {updateTime}\nTimezone: {savedTimeZone}\n";

            var sortedAppUsage = appUsageSeconds.OrderByDescending(app => app.Value);

            foreach (var app in sortedAppUsage)
            {
                //Debug.WriteLine($"{app.Key}: {app.Value} seconds");
                if (app.Value > 59)
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(app.Value);
                    int minutes = timeSpan.Minutes;
                    testing += $"{app.Key} for {minutes} minutes.\n";
                }
                else
                {
                    testing += $"{app.Key} for {app.Value} seconds.\n";
                }
                
            }

            todayUsageLbl.Text = testing;
        }
        

        private void appString_Click(object sender, EventArgs e)
        {
           
        }

        private void homeLbl_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void todayUsageLbl_Click(object sender, EventArgs e)
        {

        }

        private void lblColumn_Click(object sender, EventArgs e)
        {

        }

        private void lblColumn2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TodaysUsage();
        }
    }
}
