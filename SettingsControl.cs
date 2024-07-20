using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenTime
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }



        private void settingsLbl_Click(object sender, EventArgs e)
        {

        }

        private void dbButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            DataTable data = DatabaseHelper.LoadData();
            dataGridView1.DataSource = data;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataRemover_Click(object sender, EventArgs e)
        {
            DatabaseHelper.DeleteAllData();
        }


        private void calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            string savedTimeZone = Properties.Settings.Default.UserTimeZone;
            string bruh = calendar.SelectionStart.ToShortDateString();
            CalendarHelper calendarHelper = new CalendarHelper();
            long startDay = calendarHelper.GetDayStart(bruh);
            long endDay = calendarHelper.GetDayEnd(bruh);
            Debug.WriteLine($"{startDay}, {endDay}");

            var analyzer = new UsageAnalyzer();

            var dataFromDay = DatabaseHelper.GetDataFromSpecificDay(startDay, endDay);

            var appUsageSeconds = analyzer.CalculateAppUsageSeconds(dataFromDay);

            string testing = "";
            int totalUsage = 0;


            var sortedAppUsage = appUsageSeconds.OrderByDescending(app => app.Value);

            foreach (var app in sortedAppUsage)
            {
                //Debug.WriteLine($"{app.Key}: {app.Value} seconds");


                if (app.Value > 3599)
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(app.Value);
                    totalUsage += app.Value;
                    int minutes = timeSpan.Minutes;
                    int hours = timeSpan.Hours;
                    testing += $"{app.Key} for {hours} hours and {minutes} minutes.\n";

                }
                else if (app.Value > 59)
                {
                    TimeSpan timeSpan = TimeSpan.FromSeconds(app.Value);
                    totalUsage += app.Value;
                    int minutes = timeSpan.Minutes;
                    testing += $"{app.Key} for {minutes} minutes.\n";
                }
                else
                {
                    totalUsage += app.Value;
                    testing += $"{app.Key} for {app.Value} seconds.\n";
                }

            }
            TimeSpan timeSpan1 = TimeSpan.FromSeconds(totalUsage);
            int totalMinutes = timeSpan1.Minutes;
            int totalHours = timeSpan1.Hours;


            testing += $"\nTotal of {totalHours} hours and {totalMinutes} minutes.";
            dayUsageLbl.Text = testing;
        }

        private void dayUsageLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
