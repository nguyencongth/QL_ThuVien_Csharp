
namespace BT_QuanLyThuVien
{
    partial class InSach
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crtrbSach = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crtrbSach
            // 
            this.crtrbSach.ActiveViewIndex = -1;
            this.crtrbSach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crtrbSach.Cursor = System.Windows.Forms.Cursors.Default;
            this.crtrbSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crtrbSach.Location = new System.Drawing.Point(0, 0);
            this.crtrbSach.Name = "crtrbSach";
            this.crtrbSach.Size = new System.Drawing.Size(1261, 690);
            this.crtrbSach.TabIndex = 0;
            this.crtrbSach.Load += new System.EventHandler(this.crtrbSach_Load);
            // 
            // InSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 690);
            this.Controls.Add(this.crtrbSach);
            this.Name = "InSach";
            this.Text = "InSach";
            this.Load += new System.EventHandler(this.InSach_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crtrbSach;
    }
}