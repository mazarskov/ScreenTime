using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenTime
{
    public partial class MainForm : Form
    {
        private HomeControl homeControl;
        public MainForm()
        {
            InitializeComponent();
            //DatabaseHelper.InitializeDatabase();
            settingsControl1.Hide();
            aboutControl1.Hide();
            homeControl1.Show();
            homeControl1.BringToFront();

            homeControl = FindHomeControl();
            this.FormClosing += MainForm_FormClosing;
            this.Text = "ScreenTime";
        }

        private void ShowHome()
        {
            settingsControl1.Hide();
            aboutControl1.Hide();
            homeControl1.Show();
            homeControl1.BringToFront();
        }

        private void ShowSettings()
        {
            homeControl1.Hide();
            aboutControl1.Hide();
            settingsControl1.Show();
            settingsControl1.BringToFront();
        }

        private void ShowAbout()
        {
            homeControl1.Hide();
            settingsControl1.Hide();
            aboutControl1.Show();
            aboutControl1.BringToFront();
        }
        

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHome();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private HomeControl FindHomeControl()
        {
            // This method finds the HomeControl in the form's controls
            return this.Controls.OfType<HomeControl>().FirstOrDefault()
                ?? this.Controls.Cast<Control>().SelectMany(c => c.Controls.OfType<HomeControl>()).FirstOrDefault();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            homeControl.HandleFormClosing();
        }

        private void settingsControl1_Load(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }
    }
}
