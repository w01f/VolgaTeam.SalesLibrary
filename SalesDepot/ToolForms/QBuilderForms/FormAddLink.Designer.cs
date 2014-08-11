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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlSiteValue = new DevExpress.XtraEditors.LabelControl();
			this.labelControlSiteTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlLinkName = new DevExpress.XtraEditors.LabelControl();
			this.buttonXAddLink = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLogin = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
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
			// labelControlSiteValue
			// 
			this.labelControlSiteValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlSiteValue.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSiteValue.Appearance.ForeColor = System.Drawing.Color.Black;
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
			this.labelControlSiteTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSiteTitle.Appearance.ForeColor = System.Drawing.Color.Black;
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
			this.labelControlLinkName.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlLinkName.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlLinkName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlLinkName.Location = new System.Drawing.Point(6, 103);
			this.labelControlLinkName.Name = "labelControlLinkName";
			this.labelControlLinkName.Size = new System.Drawing.Size(223, 16);
			this.labelControlLinkName.StyleController = this.styleController;
			this.labelControlLinkName.TabIndex = 6;
			this.labelControlLinkName.Text = "Link: {0}";
			// 
			// buttonXAddLink
			// 
			this.buttonXAddLink.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAddLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXAddLink.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAddLink.Location = new System.Drawing.Point(261, 4);
			this.buttonXAddLink.Name = "buttonXAddLink";
			this.buttonXAddLink.Size = new System.Drawing.Size(97, 37);
			this.buttonXAddLink.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAddLink.TabIndex = 9;
			this.buttonXAddLink.Text = "Add Link";
			this.buttonXAddLink.Click += new System.EventHandler(this.simpleButtonAddLink_Click);
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(261, 90);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(97, 37);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 11;
			this.buttonXCancel.Text = "Cancel";
			// 
			// buttonXLogin
			// 
			this.buttonXLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLogin.Location = new System.Drawing.Point(261, 47);
			this.buttonXLogin.Name = "buttonXLogin";
			this.buttonXLogin.Size = new System.Drawing.Size(97, 37);
			this.buttonXLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLogin.TabIndex = 10;
			this.buttonXLogin.Text = "Select Site";
			this.buttonXLogin.Click += new System.EventHandler(this.simpleButtonLogin_Click);
			// 
			// FormAddLink
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(365, 131);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXLogin);
			this.Controls.Add(this.labelControlLinkName);
			this.Controls.Add(this.labelControlSiteValue);
			this.Controls.Add(this.labelControlSiteTitle);
			this.Controls.Add(this.buttonXAddLink);
			this.DoubleBuffered = true;
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

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlSiteValue;
		private DevExpress.XtraEditors.LabelControl labelControlSiteTitle;
		private DevExpress.XtraEditors.LabelControl labelControlLinkName;
		private DevComponents.DotNetBar.ButtonX buttonXAddLink;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXLogin;
	}
}