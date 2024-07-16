using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenTime
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
            lblInfo.Text = "VERY UNFINISHED\nScreenTime is inspired by screen time on apple systems.\nThis build is very early production and will include more functionality later.\nDeveloped by Max Azarskov";
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
