namespace ScreenTime
{
    partial class ReportsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportsLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reportsLbl
            // 
            this.reportsLbl.AutoSize = true;
            this.reportsLbl.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsLbl.Location = new System.Drawing.Point(346, 18);
            this.reportsLbl.Name = "reportsLbl";
            this.reportsLbl.Size = new System.Drawing.Size(76, 23);
            this.reportsLbl.TabIndex = 4;
            this.reportsLbl.Text = "Reports";
            // 
            // ReportsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportsLbl);
            this.Name = "ReportsControl";
            this.Size = new System.Drawing.Size(800, 460);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reportsLbl;
    }
}
