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

            foreach (var app in appUsageSeconds)
            {
                //Debug.WriteLine($"{app.Key}: {app.Value} seconds");

                testing += $"{app.Key} for {app.Value} seconds.\n";
            }
            Debug.WriteLine(testing);
            // Clear existing items
            listViewAppUsage.Items.Clear();
            listViewAppUsage.View = View.Details;

            // Clear any existing columns
            listViewAppUsage.Columns.Clear();

            // Add columns to the ListView
            listViewAppUsage.Columns.Add("App Name", 150, HorizontalAlignment.Left);
            listViewAppUsage.Columns.Add("Usage Seconds", 100, HorizontalAlignment.Left);

            // Populate the ListView
            foreach (var app in appUsageSeconds)
            {
                ListViewItem item = new ListViewItem(app.Key);
                item.SubItems.Add(app.Value.ToString());
                listViewAppUsage.Items.Add(item);
            }

            // Adjust the column width
            listViewAppUsage.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void listViewAppUsage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
