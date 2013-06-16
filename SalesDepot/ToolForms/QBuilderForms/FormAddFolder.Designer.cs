namespace SalesDepot.ToolForms.QBuilderForms
{
	partial class FormAddFolder
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
			this.simpleButtonAddFolder = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonLogin = new DevExpress.XtraEditors.SimpleButton();
			this.labelControlSiteValue = new DevExpress.XtraEditors.LabelControl();
			this.labelControlSiteTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlFolderName = new DevExpress.XtraEditors.LabelControl();
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
			// simpleButtonAddFolder
			// 
			this.simpleButtonAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonAddFolder.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonAddFolder.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonAddFolder.Appearance.Options.UseFont = true;
			this.simpleButtonAddFolder.Appearance.Options.UseForeColor = true;
			this.simpleButtonAddFolder.Location = new System.Drawing.Point(261, 4);
			this.simpleButtonAddFolder.Name = "simpleButtonAddFolder";
			this.simpleButtonAddFolder.Size = new System.Drawing.Size(97, 37);
			this.simpleButtonAddFolder.TabIndex = 1;
			this.simpleButtonAddFolder.Text = "Add Links";
			this.simpleButtonAddFolder.Click += new System.EventHandler(this.simpleButtonAddFolder_Click);
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
			this.labelControlSiteTitle.Text = "You are about to add links to Link Cart on Selected site:";
			// 
			// labelControlFolderName
			// 
			this.labelControlFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlFolderName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlFolderName.Location = new System.Drawing.Point(6, 103);
			this.labelControlFolderName.Name = "labelControlFolderName";
			this.labelControlFolderName.Size = new System.Drawing.Size(223, 16);
			this.labelControlFolderName.StyleController = this.styleController;
			this.labelControlFolderName.TabIndex = 6;
			this.labelControlFolderName.Text = "Folder: {0}";
			// 
			// FormAddFolder
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(365, 131);
			this.Controls.Add(this.labelControlFolderName);
			this.Controls.Add(this.simpleButtonLogin);
			this.Controls.Add(this.labelControlSiteValue);
			this.Controls.Add(this.labelControlSiteTitle);
			this.Controls.Add(this.simpleButtonCancel);
			this.Controls.Add(this.simpleButtonAddFolder);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddFolder";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Links to quickSITE";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SimpleButton simpleButtonAddFolder;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
		private DevExpress.XtraEditors.SimpleButton simpleButtonLogin;
		private DevExpress.XtraEditors.LabelControl labelControlSiteValue;
		private DevExpress.XtraEditors.LabelControl labelControlSiteTitle;
		private DevExpress.XtraEditors.LabelControl labelControlFolderName;
	}
}