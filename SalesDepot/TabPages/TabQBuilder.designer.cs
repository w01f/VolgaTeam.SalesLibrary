namespace SalesDepot.TabPages
{
    partial class TabQBuilder
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
			this.splitContainerControlBottom = new DevExpress.XtraEditors.SplitContainerControl();
			this.splitContainerControlTop = new DevExpress.XtraEditors.SplitContainerControl();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlBottom)).BeginInit();
			this.splitContainerControlBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTop)).BeginInit();
			this.splitContainerControlTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerControlBottom
			// 
			this.splitContainerControlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlBottom.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlBottom.Name = "splitContainerControlBottom";
			this.splitContainerControlBottom.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControlBottom.Panel1.MinSize = 350;
			this.splitContainerControlBottom.Panel1.Text = "Panel1";
			this.splitContainerControlBottom.Panel2.Controls.Add(this.splitContainerControlTop);
			this.splitContainerControlBottom.Panel2.Text = "Panel2";
			this.splitContainerControlBottom.Size = new System.Drawing.Size(714, 366);
			this.splitContainerControlBottom.TabIndex = 0;
			this.splitContainerControlBottom.Text = "splitContainerControl1";
			// 
			// splitContainerControlTop
			// 
			this.splitContainerControlTop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlTop.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControlTop.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlTop.Name = "splitContainerControlTop";
			this.splitContainerControlTop.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControlTop.Panel1.Text = "Panel1";
			this.splitContainerControlTop.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControlTop.Panel2.MinSize = 300;
			this.splitContainerControlTop.Panel2.Text = "Panel2";
			this.splitContainerControlTop.Size = new System.Drawing.Size(352, 366);
			this.splitContainerControlTop.TabIndex = 1;
			this.splitContainerControlTop.Text = "splitContainerControl1";
			// 
			// TabQBuilder
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.splitContainerControlBottom);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "TabQBuilder";
			this.Size = new System.Drawing.Size(714, 366);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlBottom)).EndInit();
			this.splitContainerControlBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlTop)).EndInit();
			this.splitContainerControlTop.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControlBottom;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControlTop;
    }
}
