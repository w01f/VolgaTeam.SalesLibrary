namespace SalesLibraries.FileManager.PresentationLayer.Sync
{
	partial class FormSyncLock
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
			this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlContent = new DevExpress.XtraEditors.LabelControl();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.labelControlContacts = new DevExpress.XtraEditors.LabelControl();
			this.pnButtonBorder = new System.Windows.Forms.Panel();
			this.pnBottom.SuspendLayout();
			this.pnButtonBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// simpleButtonOK
			// 
			this.simpleButtonOK.AllowFocus = false;
			this.simpleButtonOK.Appearance.BackColor = System.Drawing.Color.Red;
			this.simpleButtonOK.Appearance.BackColor2 = System.Drawing.Color.Red;
			this.simpleButtonOK.Appearance.BorderColor = System.Drawing.Color.White;
			this.simpleButtonOK.Appearance.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonOK.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonOK.Appearance.Options.UseBackColor = true;
			this.simpleButtonOK.Appearance.Options.UseBorderColor = true;
			this.simpleButtonOK.Appearance.Options.UseFont = true;
			this.simpleButtonOK.Appearance.Options.UseForeColor = true;
			this.simpleButtonOK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.simpleButtonOK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.simpleButtonOK.Location = new System.Drawing.Point(2, 2);
			this.simpleButtonOK.LookAndFeel.UseDefaultLookAndFeel = false;
			this.simpleButtonOK.Name = "simpleButtonOK";
			this.simpleButtonOK.Size = new System.Drawing.Size(164, 58);
			this.simpleButtonOK.TabIndex = 0;
			this.simpleButtonOK.Text = "OK";
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.AllowHtmlString = true;
			this.labelControlTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlTitle.Location = new System.Drawing.Point(0, 0);
			this.labelControlTitle.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
			this.labelControlTitle.LookAndFeel.UseDefaultLookAndFeel = false;
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.labelControlTitle.Size = new System.Drawing.Size(655, 102);
			this.labelControlTitle.TabIndex = 1;
			this.labelControlTitle.Text = "Your Library <u><b>WILL NOT SYNC</b></u> until\r\nyou resolve these issues:";
			this.labelControlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelCaption_MouseDown);
			// 
			// labelControlContent
			// 
			this.labelControlContent.AllowHtmlString = true;
			this.labelControlContent.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlContent.Appearance.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlContent.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlContent.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.labelControlContent.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlContent.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlContent.Location = new System.Drawing.Point(0, 102);
			this.labelControlContent.LookAndFeel.UseDefaultLookAndFeel = false;
			this.labelControlContent.Name = "labelControlContent";
			this.labelControlContent.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.labelControlContent.Size = new System.Drawing.Size(655, 330);
			this.labelControlContent.TabIndex = 2;
			this.labelControlContent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelCaption_MouseDown);
			// 
			// pnBottom
			// 
			this.pnBottom.BackColor = System.Drawing.Color.Transparent;
			this.pnBottom.Controls.Add(this.labelControlContacts);
			this.pnBottom.Controls.Add(this.pnButtonBorder);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.ForeColor = System.Drawing.Color.Black;
			this.pnBottom.Location = new System.Drawing.Point(0, 432);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(655, 100);
			this.pnBottom.TabIndex = 3;
			this.pnBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelCaption_MouseDown);
			// 
			// labelControlContacts
			// 
			this.labelControlContacts.AllowHtmlString = true;
			this.labelControlContacts.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlContacts.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlContacts.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlContacts.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlContacts.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlContacts.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelControlContacts.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControlContacts.Location = new System.Drawing.Point(0, 0);
			this.labelControlContacts.LookAndFeel.UseDefaultLookAndFeel = false;
			this.labelControlContacts.Name = "labelControlContacts";
			this.labelControlContacts.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.labelControlContacts.Size = new System.Drawing.Size(452, 100);
			this.labelControlContacts.TabIndex = 3;
			this.labelControlContacts.Text = "When these issues are resolved, then you can sync your library.\r\nFor more info, c" +
    "ontact: <href={0}><color=white>{1}</color></href>";
			this.labelControlContacts.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.labelControlContacts_HyperlinkClick);
			// 
			// pnButtonBorder
			// 
			this.pnButtonBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnButtonBorder.BackColor = System.Drawing.Color.White;
			this.pnButtonBorder.Controls.Add(this.simpleButtonOK);
			this.pnButtonBorder.ForeColor = System.Drawing.Color.Black;
			this.pnButtonBorder.Location = new System.Drawing.Point(475, 19);
			this.pnButtonBorder.Name = "pnButtonBorder";
			this.pnButtonBorder.Padding = new System.Windows.Forms.Padding(2);
			this.pnButtonBorder.Size = new System.Drawing.Size(168, 62);
			this.pnButtonBorder.TabIndex = 0;
			// 
			// FormSyncLock
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(655, 532);
			this.Controls.Add(this.labelControlContent);
			this.Controls.Add(this.pnBottom);
			this.Controls.Add(this.labelControlTitle);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormSyncLock";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MetroForm";
			this.Shown += new System.EventHandler(this.FormSyncLock_Shown);
			this.pnBottom.ResumeLayout(false);
			this.pnButtonBorder.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.LabelControl labelControlContent;
		private System.Windows.Forms.Panel pnBottom;
		private DevExpress.XtraEditors.LabelControl labelControlContacts;
		private System.Windows.Forms.Panel pnButtonBorder;
	}
}