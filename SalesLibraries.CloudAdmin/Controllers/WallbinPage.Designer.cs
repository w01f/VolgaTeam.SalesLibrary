namespace SalesLibraries.CloudAdmin.Controllers
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
			this.components = new System.ComponentModel.Container();
			this.pnContainer = new System.Windows.Forms.Panel();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnMain = new System.Windows.Forms.Panel();
			this.retractableBar = new SalesLibraries.CommonGUI.RetractableBar.RetractableBarLeft();
			this.laEditorTitle = new System.Windows.Forms.Label();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.linkInfoControl = new SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl();
			this.pnTagInfoContainer = new System.Windows.Forms.Panel();
			this.superFilterControl = new SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings.SuperFilterControl();
			this.pnPageSelector = new System.Windows.Forms.Panel();
			this.comboBoxEditPages = new DevExpress.XtraEditors.ComboBoxEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.linkTagsInfoControl = new SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings.LinkTagsInfoControl();
			this.pnMain.SuspendLayout();
			this.retractableBar.Header.SuspendLayout();
			this.pnHeader.SuspendLayout();
			this.pnPageSelector.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPages.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
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
			this.pnMain.Size = new System.Drawing.Size(676, 601);
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
			this.retractableBar.Content.Size = new System.Drawing.Size(308, 557);
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
			this.retractableBar.Size = new System.Drawing.Size(312, 601);
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
			this.pnHeader.Controls.Add(this.pnPageSelector);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(988, 47);
			this.pnHeader.TabIndex = 8;
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
			this.linkInfoControl.Size = new System.Drawing.Size(254, 47);
			this.linkInfoControl.TabIndex = 2;
			// 
			// pnTagInfoContainer
			// 
			this.pnTagInfoContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnTagInfoContainer.Location = new System.Drawing.Point(450, 0);
			this.pnTagInfoContainer.Name = "pnTagInfoContainer";
			this.pnTagInfoContainer.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.pnTagInfoContainer.Size = new System.Drawing.Size(340, 47);
			this.pnTagInfoContainer.TabIndex = 1;
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
			// pnPageSelector
			// 
			this.pnPageSelector.Controls.Add(this.comboBoxEditPages);
			this.pnPageSelector.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnPageSelector.Location = new System.Drawing.Point(790, 0);
			this.pnPageSelector.Name = "pnPageSelector";
			this.pnPageSelector.Size = new System.Drawing.Size(198, 47);
			this.pnPageSelector.TabIndex = 3;
			this.pnPageSelector.Visible = false;
			// 
			// comboBoxEditPages
			// 
			this.comboBoxEditPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditPages.Enabled = false;
			this.comboBoxEditPages.Location = new System.Drawing.Point(40, 12);
			this.comboBoxEditPages.Name = "comboBoxEditPages";
			this.comboBoxEditPages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditPages.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditPages.Size = new System.Drawing.Size(152, 22);
			this.comboBoxEditPages.StyleController = this.styleController;
			this.comboBoxEditPages.TabIndex = 2;
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// linkTagsInfoControl
			// 
			this.linkTagsInfoControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.linkTagsInfoControl.Location = new System.Drawing.Point(0, 648);
			this.linkTagsInfoControl.Name = "linkTagsInfoControl";
			this.linkTagsInfoControl.Size = new System.Drawing.Size(988, 46);
			this.linkTagsInfoControl.TabIndex = 9;
			// 
			// WallbinPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.retractableBar);
			this.Controls.Add(this.pnHeader);
			this.Controls.Add(this.linkTagsInfoControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WallbinPage";
			this.Size = new System.Drawing.Size(988, 694);
			this.pnMain.ResumeLayout(false);
			this.retractableBar.Header.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			this.pnPageSelector.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPages.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
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
		public System.Windows.Forms.Panel pnTagInfoContainer;
		private PresentationLayer.Wallbin.Links.SingleSettings.LinkInfoControl linkInfoControl;
		public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPages;
		public System.Windows.Forms.Panel pnPageSelector;
		private DevExpress.XtraEditors.StyleController styleController;
		private PresentationLayer.Wallbin.Links.SingleSettings.LinkTagsInfoControl linkTagsInfoControl;
	}
}
