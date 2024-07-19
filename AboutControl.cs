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

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }

        private void lblColumn5_Click(object sender, EventArgs e)
        {

        }
    }
}
