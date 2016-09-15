namespace SalesLibraries.FileManager.Controllers
{
	sealed partial class WallbinPage
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
			this.laEditorTitle = new System.Windows.Forms.Label();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.pnTagInfoContainer = new System.Windows.Forms.Panel();
			this.linkInfoControl = new SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl();
			this.superFilterControl = new SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings.SuperFilterControl();
			this.pnMain.SuspendLayout();
			this.retractableBar.Header.SuspendLayout();
			this.pnHeader.SuspendLayout();
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
			this.pnMain.Location = new System.Drawing.Point(312, 47);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(676, 647);
			this.pnMain.TabIndex = 2;
			// 
			// retractableBar
			// 
			this.retractableBar.AnimationDelay = 0;
			this.retractableBar.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBar.Content
			// 
			this.retractableBar.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBar.Content.Name = "Content";
			this.retractableBar.Content.Size = new System.Drawing.Size(308, 603);
			this.retractableBar.Content.TabIndex = 1;
			this.retractableBar.ContentSize = 350;
			this.retractableBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBar.Header
			// 
			this.retractableBar.Header.Controls.Add(this.laEditorTitle);
			this.retractableBar.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBar.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBar.Header.Name = "Header";
			this.retractableBar.Header.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.retractableBar.Header.Size = new System.Drawing.Size(257, 36);
			this.retractableBar.Header.TabIndex = 2;
			this.retractableBar.Location = new System.Drawing.Point(0, 47);
			this.retractableBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBar.Name = "retractableBar";
			this.retractableBar.Size = new System.Drawing.Size(312, 647);
			this.retractableBar.TabIndex = 7;
			// 
			// laEditorTitle
			// 
			this.laEditorTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laEditorTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laEditorTitle.Location = new System.Drawing.Point(10, 0);
			this.laEditorTitle.Name = "laEditorTitle";
			this.laEditorTitle.Size = new System.Drawing.Size(247, 36);
			this.laEditorTitle.TabIndex = 1;
			this.laEditorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnHeader
			// 
			this.pnHeader.BackColor = System.Drawing.Color.White;
			this.pnHeader.Controls.Add(this.linkInfoControl);
			this.pnHeader.Controls.Add(this.pnTagInfoContainer);
			this.pnHeader.Controls.Add(this.superFilterControl);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(988, 47);
			this.pnHeader.TabIndex = 8;
			// 
			// pnTagInfoContainer
			// 
			this.pnTagInfoContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnTagInfoContainer.Location = new System.Drawing.Point(618, 0);
			this.pnTagInfoContainer.Name = "pnTagInfoContainer";
			this.pnTagInfoContainer.Size = new System.Drawing.Size(370, 47);
			this.pnTagInfoContainer.TabIndex = 1;
			// 
			// linkInfoControl
			// 
			this.linkInfoControl.AllowHtmlString = true;
			this.linkInfoControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkInfoControl.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
			this.linkInfoControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.linkInfoControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.linkInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkInfoControl.Location = new System.Drawing.Point(196, 0);
			this.linkInfoControl.Name = "linkInfoControl";
			this.linkInfoControl.Size = new System.Drawing.Size(422, 47);
			this.linkInfoControl.TabIndex = 2;
			// 
			// superFilterControl
			// 
			this.superFilterControl.BackColor = System.Drawing.Color.White;
			this.superFilterControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.superFilterControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superFilterControl.Location = new System.Drawing.Point(0, 0);
			this.superFilterControl.Name = "superFilterControl";
			this.superFilterControl.Size = new System.Drawing.Size(196, 47);
			this.superFilterControl.TabIndex = 0;
			// 
			// WallbinPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.retractableBar);
			this.Controls.Add(this.pnHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WallbinPage";
			this.Size = new System.Drawing.Size(988, 694);
			this.pnMain.ResumeLayout(false);
			this.retractableBar.Header.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnContainer;
		private System.Windows.Forms.Panel pnEmpty;
		private System.Windows.Forms.Panel pnMain;
		private CommonGUI.RetractableBar.RetractableBarLeft retractableBar;
		private System.Windows.Forms.Label laEditorTitle;
		private System.Windows.Forms.Panel pnHeader;
		private PresentationLayer.Wallbin.Links.SingleSettings.SuperFilterControl superFilterControl;
		private System.Windows.Forms.Panel pnTagInfoContainer;
		private PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl linkInfoControl;
	}
}
