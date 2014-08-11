namespace FileManager.PresentationClasses.TabPages
{
    partial class TabClipartControl
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
			this.splitContainerControlMain = new DevExpress.XtraEditors.SplitContainerControl();
			this.clipartTreeListControl = new FileManager.PresentationClasses.Cliparts.ClipartTreeListControl();
			this.pnMain = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlMain)).BeginInit();
			this.splitContainerControlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerControlMain
			// 
			this.splitContainerControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlMain.Name = "splitContainerControlMain";
			this.splitContainerControlMain.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControlMain.Panel1.Controls.Add(this.clipartTreeListControl);
			this.splitContainerControlMain.Panel1.Text = "Panel1";
			this.splitContainerControlMain.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
			this.splitContainerControlMain.Panel2.Controls.Add(this.pnMain);
			this.splitContainerControlMain.Panel2.Text = "Panel2";
			this.splitContainerControlMain.Size = new System.Drawing.Size(977, 603);
			this.splitContainerControlMain.SplitterPosition = 253;
			this.splitContainerControlMain.TabIndex = 0;
			this.splitContainerControlMain.Text = "splitContainerControl1";
			// 
			// clipartTreeListControl
			// 
			this.clipartTreeListControl.BackColor = System.Drawing.Color.White;
			this.clipartTreeListControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.clipartTreeListControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.clipartTreeListControl.Location = new System.Drawing.Point(0, 0);
			this.clipartTreeListControl.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
			this.clipartTreeListControl.Name = "clipartTreeListControl";
			this.clipartTreeListControl.Size = new System.Drawing.Size(249, 599);
			this.clipartTreeListControl.TabIndex = 0;
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(708, 599);
			this.pnMain.TabIndex = 0;
			// 
			// TabClipartControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.splitContainerControlMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "TabClipartControl";
			this.Size = new System.Drawing.Size(977, 603);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlMain)).EndInit();
			this.splitContainerControlMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.SplitContainerControl splitContainerControlMain;
        private PresentationClasses.Cliparts.ClipartTreeListControl clipartTreeListControl;
        private System.Windows.Forms.Panel pnMain;
    }
}
