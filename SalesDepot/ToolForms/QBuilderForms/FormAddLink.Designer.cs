namespace SalesDepot.ToolForms.QBuilderForms
{
	partial class FormAddLink
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
			this.components = new System.ComponentModel.Container();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.simpleButtonAddLink = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonLogin = new DevExpress.XtraEditors.SimpleButton();
			this.labelControlSiteValue = new DevExpress.XtraEditors.LabelControl();
			this.labelControlSiteTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlLinkName = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
			// simpleButtonAddLink
			// 
			this.simpleButtonAddLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonAddLink.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonAddLink.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonAddLink.Appearance.Options.UseFont = true;
			this.simpleButtonAddLink.Appearance.Options.UseForeColor = true;
			this.simpleButtonAddLink.Location = new System.Drawing.Point(261, 4);
			this.simpleButtonAddLink.Name = "simpleButtonAddLink";
			this.simpleButtonAddLink.Size = new System.Drawing.Size(97, 37);
			this.simpleButtonAddLink.TabIndex = 1;
			this.simpleButtonAddLink.Text = "Add Link";
			this.simpleButtonAddLink.Click += new System.EventHandler(this.simpleButtonAddLink_Click);
			// 
			// simpleButtonCancel
			// 
			this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonCancel.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonCancel.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonCancel.Appearance.Options.UseFont = true;
			this.simpleButtonCancel.Appearance.Options.UseForeColor = true;
			this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButtonCancel.Location = new System.Drawing.Point(261, 90);
			this.simpleButtonCancel.Name = "simpleButtonCancel";
			this.simpleButtonCancel.Size = new System.Drawing.Size(97, 37);
			this.simpleButtonCancel.TabIndex = 2;
			this.simpleButtonCancel.Text = "Cancel";
			// 
			// simpleButtonLogin
			// 
			this.simpleButtonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonLogin.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonLogin.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonLogin.Appearance.Options.UseFont = true;
			this.simpleButtonLogin.Appearance.Options.UseForeColor = true;
			this.simpleButtonLogin.Location = new System.Drawing.Point(261, 47);
			this.simpleButtonLogin.Name = "simpleButtonLogin";
			this.simpleButtonLogin.Size = new System.Drawing.Size(97, 37);
			this.simpleButtonLogin.TabIndex = 5;
			this.simpleButtonLogin.Text = "Select Site";
			this.simpleButtonLogin.Click += new System.EventHandler(this.simpleButtonLogin_Click);
			// 
			// labelControlSiteValue
			// 
			this.labelControlSiteValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlSiteValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlSiteValue.Location = new System.Drawing.Point(6, 58);
			this.labelControlSiteValue.Name = "labelControlSiteValue";
			this.labelControlSiteValue.Size = new System.Drawing.Size(223, 16);
			this.labelControlSiteValue.StyleController = this.styleController;
			this.labelControlSiteValue.TabIndex = 4;
			this.labelControlSiteValue.Text = "Site: {0}";
			// 
			// labelControlSiteTitle
			// 
			this.labelControlSiteTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlSiteTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlSiteTitle.Location = new System.Drawing.Point(6, 4);
			this.labelControlSiteTitle.Name = "labelControlSiteTitle";
			this.labelControlSiteTitle.Size = new System.Drawing.Size(244, 37);
			this.labelControlSiteTitle.StyleController = this.styleController;
			this.labelControlSiteTitle.TabIndex = 3;
			this.labelControlSiteTitle.Text = "You are about to add link to Link Cart on Selected site:";
			// 
			// labelControlLinkName
			// 
			this.labelControlLinkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlLinkName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlLinkName.Location = new System.Drawing.Point(6, 103);
			this.labelControlLinkName.Name = "labelControlLinkName";
			this.labelControlLinkName.Size = new System.Drawing.Size(223, 16);
			this.labelControlLinkName.StyleController = this.styleController;
			this.labelControlLinkName.TabIndex = 6;
			this.labelControlLinkName.Text = "Link: {0}";
			// 
			// FormAddLink
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(365, 131);
			this.Controls.Add(this.labelControlLinkName);
			this.Controls.Add(this.simpleButtonLogin);
			this.Controls.Add(this.labelControlSiteValue);
			this.Controls.Add(this.labelControlSiteTitle);
			this.Controls.Add(this.simpleButtonCancel);
			this.Controls.Add(this.simpleButtonAddLink);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddLink";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Link to quickSITE";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SimpleButton simpleButtonAddLink;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
		private DevExpress.XtraEditors.SimpleButton simpleButtonLogin;
		private DevExpress.XtraEditors.LabelControl labelControlSiteValue;
		private DevExpress.XtraEditors.LabelControl labelControlSiteTitle;
		private DevExpress.XtraEditors.LabelControl labelControlLinkName;
	}
}