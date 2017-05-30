namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	partial class WallbinContainer
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
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.retractableBar = new SalesLibraries.CommonGUI.RetractableBar.RetractableBarLeft();
			this.emailBinControl = new SalesLibraries.SalesDepot.PresentationLayer.EmailBin.EmailBinControl();
			this.pbEmailBinHelp = new System.Windows.Forms.PictureBox();
			this.laRetractableBarTitle = new System.Windows.Forms.Label();
			this.pnMain.SuspendLayout();
			this.retractableBar.Content.SuspendLayout();
			this.retractableBar.Header.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbEmailBinHelp)).BeginInit();
			this.SuspendLayout();
			// 
			// pnContainer
			// 
			this.pnContainer.BackColor = System.Drawing.Color.White;
			this.pnContainer.Location = new System.Drawing.Point(33, 49);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(199, 158);
			this.pnContainer.TabIndex = 0;
			// 
			// pnEmpty
			// 
			this.pnEmpty.BackColor = System.Drawing.Color.White;
			this.pnEmpty.Location = new System.Drawing.Point(33, 259);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(199, 141);
			this.pnEmpty.TabIndex = 1;
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.pnContainer);
			this.pnMain.Controls.Add(this.pnEmpty);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(312, 0);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(580, 549);
			this.pnMain.TabIndex = 2;
			// 
			// retractableBar
			// 
			this.retractableBar.AnimationDelay = 0;
			this.retractableBar.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBar.Content
			// 
			this.retractableBar.Content.Controls.Add(this.emailBinControl);
			this.retractableBar.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBar.Content.Name = "Content";
			this.retractableBar.Content.Size = new System.Drawing.Size(308, 505);
			this.retractableBar.Content.TabIndex = 1;
			this.retractableBar.ContentSize = 350;
			this.retractableBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBar.Header
			// 
			this.retractableBar.Header.Controls.Add(this.pbEmailBinHelp);
			this.retractableBar.Header.Controls.Add(this.laRetractableBarTitle);
			this.retractableBar.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBar.Header.Name = "Header";
			this.retractableBar.Header.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.retractableBar.Header.Size = new System.Drawing.Size(257, 36);
			this.retractableBar.Header.TabIndex = 2;
			this.retractableBar.Location = new System.Drawing.Point(0, 0);
			this.retractableBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBar.Name = "retractableBar";
			this.retractableBar.Size = new System.Drawing.Size(312, 549);
			this.retractableBar.TabIndex = 7;
			this.retractableBar.Visible = false;
			// 
			// emailBinControl
			// 
			this.emailBinControl.BackColor = System.Drawing.Color.White;
			this.emailBinControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.emailBinControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.emailBinControl.Location = new System.Drawing.Point(0, 0);
			this.emailBinControl.Name = "emailBinControl";
			this.emailBinControl.Size = new System.Drawing.Size(308, 505);
			this.emailBinControl.TabIndex = 0;
			// 
			// pbEmailBinHelp
			// 
			this.pbEmailBinHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbEmailBinHelp.Image = global::SalesLibraries.SalesDepot.Properties.Resources.HelpSmall;
			this.pbEmailBinHelp.Location = new System.Drawing.Point(219, 0);
			this.pbEmailBinHelp.Name = "pbEmailBinHelp";
			this.pbEmailBinHelp.Size = new System.Drawing.Size(36, 36);
			this.pbEmailBinHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbEmailBinHelp.TabIndex = 3;
			this.pbEmailBinHelp.TabStop = false;
			this.pbEmailBinHelp.Click += new System.EventHandler(this.pbEmailBinHelp_Click);
			// 
			// laRetractableBarTitle
			// 
			this.laRetractableBarTitle.Dock = System.Windows.Forms.DockStyle.Left;
			this.laRetractableBarTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laRetractableBarTitle.Location = new System.Drawing.Point(20, 0);
			this.laRetractableBarTitle.Name = "laRetractableBarTitle";
			this.laRetractableBarTitle.Size = new System.Drawing.Size(163, 36);
			this.laRetractableBarTitle.TabIndex = 1;
			this.laRetractableBarTitle.Text = "Email Attachments";
			this.laRetractableBarTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// WallbinContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.retractableBar);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WallbinContainer";
			this.Size = new System.Drawing.Size(892, 549);
			this.pnMain.ResumeLayout(false);
			this.retractableBar.Content.ResumeLayout(false);
			this.retractableBar.Header.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbEmailBinHelp)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.Panel pnContainer;
		protected System.Windows.Forms.Panel pnEmpty;
		private System.Windows.Forms.Panel pnMain;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBar;
		private System.Windows.Forms.Label laRetractableBarTitle;
		private EmailBin.EmailBinControl emailBinControl;
		private System.Windows.Forms.PictureBox pbEmailBinHelp;
	}
}
