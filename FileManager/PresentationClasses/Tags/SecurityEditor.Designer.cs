namespace FileManager.PresentationClasses.Tags
{
	partial class SecurityEditor
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
			this.laHeader = new System.Windows.Forms.Label();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnData = new System.Windows.Forms.Panel();
			this.ckSecurityShareLink = new System.Windows.Forms.CheckBox();
			this.memoEditSecurityUsers = new DevExpress.XtraEditors.MemoEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.rbSecurityAllowed = new System.Windows.Forms.RadioButton();
			this.rbSecurityRestricted = new System.Windows.Forms.RadioButton();
			this.rbSecurityDenied = new System.Windows.Forms.RadioButton();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXReset = new DevComponents.DotNetBar.ButtonX();
			this.pnMain.SuspendLayout();
			this.pnData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSecurityUsers.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// laHeader
			// 
			this.laHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.laHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.laHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laHeader.Location = new System.Drawing.Point(0, 0);
			this.laHeader.Name = "laHeader";
			this.laHeader.Size = new System.Drawing.Size(350, 24);
			this.laHeader.TabIndex = 0;
			this.laHeader.Text = "Manage Security";
			this.laHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnMain
			// 
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnMain.Controls.Add(this.pnData);
			this.pnMain.Controls.Add(this.pnButtons);
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.Location = new System.Drawing.Point(0, 24);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(350, 380);
			this.pnMain.TabIndex = 1;
			// 
			// pnData
			// 
			this.pnData.Controls.Add(this.ckSecurityShareLink);
			this.pnData.Controls.Add(this.memoEditSecurityUsers);
			this.pnData.Controls.Add(this.rbSecurityAllowed);
			this.pnData.Controls.Add(this.rbSecurityRestricted);
			this.pnData.Controls.Add(this.rbSecurityDenied);
			this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnData.Location = new System.Drawing.Point(0, 47);
			this.pnData.Name = "pnData";
			this.pnData.Size = new System.Drawing.Size(346, 329);
			this.pnData.TabIndex = 1;
			// 
			// ckSecurityShareLink
			// 
			this.ckSecurityShareLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ckSecurityShareLink.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckSecurityShareLink.Location = new System.Drawing.Point(5, 285);
			this.ckSecurityShareLink.Name = "ckSecurityShareLink";
			this.ckSecurityShareLink.Size = new System.Drawing.Size(336, 41);
			this.ckSecurityShareLink.TabIndex = 39;
			this.ckSecurityShareLink.Text = "Allow Users to Email this Link and post to quickSITES";
			this.ckSecurityShareLink.UseVisualStyleBackColor = true;
			this.ckSecurityShareLink.CheckedChanged += new System.EventHandler(this.ValueCheckedChanged);
			// 
			// memoEditSecurityUsers
			// 
			this.memoEditSecurityUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditSecurityUsers.Enabled = false;
			this.memoEditSecurityUsers.Location = new System.Drawing.Point(23, 189);
			this.memoEditSecurityUsers.Name = "memoEditSecurityUsers";
			this.memoEditSecurityUsers.Properties.NullText = "Type Usernames, separated by  commas...";
			this.memoEditSecurityUsers.Size = new System.Drawing.Size(318, 90);
			this.memoEditSecurityUsers.StyleController = this.styleController;
			this.memoEditSecurityUsers.TabIndex = 38;
			this.memoEditSecurityUsers.EditValueChanged += new System.EventHandler(this.memoEditSecurityUsers_EditValueChanged);
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
			// rbSecurityAllowed
			// 
			this.rbSecurityAllowed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityAllowed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityAllowed.Location = new System.Drawing.Point(5, 8);
			this.rbSecurityAllowed.Name = "rbSecurityAllowed";
			this.rbSecurityAllowed.Size = new System.Drawing.Size(336, 45);
			this.rbSecurityAllowed.TabIndex = 37;
			this.rbSecurityAllowed.TabStop = true;
			this.rbSecurityAllowed.Text = "This link is DISPLAYED in the Local Sales Library and also in the iPad Sales Libr" +
    "ary…";
			this.rbSecurityAllowed.UseVisualStyleBackColor = true;
			this.rbSecurityAllowed.CheckedChanged += new System.EventHandler(this.ValueCheckedChanged);
			// 
			// rbSecurityRestricted
			// 
			this.rbSecurityRestricted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityRestricted.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityRestricted.Location = new System.Drawing.Point(5, 130);
			this.rbSecurityRestricted.Name = "rbSecurityRestricted";
			this.rbSecurityRestricted.Size = new System.Drawing.Size(336, 53);
			this.rbSecurityRestricted.TabIndex = 36;
			this.rbSecurityRestricted.TabStop = true;
			this.rbSecurityRestricted.Text = "This link is DISPLAYED in the Local Sales Library and ONLY VISIBLE in the iPad Sa" +
    "les Library by these SPECIFIC Users:";
			this.rbSecurityRestricted.UseVisualStyleBackColor = true;
			this.rbSecurityRestricted.CheckedChanged += new System.EventHandler(this.rbSecurityRestricted_CheckedChanged);
			// 
			// rbSecurityDenied
			// 
			this.rbSecurityDenied.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rbSecurityDenied.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbSecurityDenied.Location = new System.Drawing.Point(5, 67);
			this.rbSecurityDenied.Name = "rbSecurityDenied";
			this.rbSecurityDenied.Size = new System.Drawing.Size(336, 51);
			this.rbSecurityDenied.TabIndex = 35;
			this.rbSecurityDenied.TabStop = true;
			this.rbSecurityDenied.Text = "This link is ONLY DISPLAYED in the Local Sales Library (not visible at all in the" +
    " iPad Sales Library)";
			this.rbSecurityDenied.UseVisualStyleBackColor = true;
			this.rbSecurityDenied.CheckedChanged += new System.EventHandler(this.ValueCheckedChanged);
			// 
			// pnButtons
			// 
			this.pnButtons.Controls.Add(this.buttonXReset);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnButtons.Location = new System.Drawing.Point(0, 0);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(346, 47);
			this.pnButtons.TabIndex = 0;
			// 
			// buttonXReset
			// 
			this.buttonXReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReset.Location = new System.Drawing.Point(5, 8);
			this.buttonXReset.Name = "buttonXReset";
			this.buttonXReset.Size = new System.Drawing.Size(336, 30);
			this.buttonXReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReset.TabIndex = 0;
			this.buttonXReset.Text = "RESET ALL SECURITY for the Selected Links";
			this.buttonXReset.TextColor = System.Drawing.Color.Black;
			this.buttonXReset.Click += new System.EventHandler(this.buttonXReset_Click);
			// 
			// SecurityEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.laHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SecurityEditor";
			this.Size = new System.Drawing.Size(350, 404);
			this.pnMain.ResumeLayout(false);
			this.pnData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditSecurityUsers.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label laHeader;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnData;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXReset;
		private DevExpress.XtraEditors.StyleController styleController;
		public DevExpress.XtraEditors.MemoEdit memoEditSecurityUsers;
		public System.Windows.Forms.RadioButton rbSecurityAllowed;
		public System.Windows.Forms.RadioButton rbSecurityRestricted;
		public System.Windows.Forms.RadioButton rbSecurityDenied;
		public System.Windows.Forms.CheckBox ckSecurityShareLink;
	}
}
