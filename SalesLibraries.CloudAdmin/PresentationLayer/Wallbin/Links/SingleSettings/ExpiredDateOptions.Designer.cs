namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	partial class ExpiredDateOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpiredDateOptions));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gbExpiredLinks = new DevExpress.XtraEditors.GroupControl();
			this.checkBoxLabelLink = new System.Windows.Forms.CheckBox();
			this.timeEditExpirationTime = new DevExpress.XtraEditors.TimeEdit();
			this.checkBoxSendEmailWhenDelete = new System.Windows.Forms.CheckBox();
			this.laExpireddateActions = new System.Windows.Forms.Label();
			this.dateEditExpirationDate = new DevExpress.XtraEditors.DateEdit();
			this.laExpirationDateTitle = new System.Windows.Forms.Label();
			this.laAddDateValue = new System.Windows.Forms.Label();
			this.laAddDateTitle = new System.Windows.Forms.Label();
			this.checkBoxEnableExpiredLinks = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.gbExpiredLinks)).BeginInit();
			this.gbExpiredLinks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.timeEditExpirationTime.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// gbExpiredLinks
			// 
			this.gbExpiredLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbExpiredLinks.Appearance.BackColor = System.Drawing.Color.White;
			this.gbExpiredLinks.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gbExpiredLinks.Appearance.Options.UseBackColor = true;
			this.gbExpiredLinks.Appearance.Options.UseForeColor = true;
			this.gbExpiredLinks.Controls.Add(this.checkBoxLabelLink);
			this.gbExpiredLinks.Controls.Add(this.timeEditExpirationTime);
			this.gbExpiredLinks.Controls.Add(this.checkBoxSendEmailWhenDelete);
			this.gbExpiredLinks.Controls.Add(this.laExpireddateActions);
			this.gbExpiredLinks.Controls.Add(this.dateEditExpirationDate);
			this.gbExpiredLinks.Controls.Add(this.laExpirationDateTitle);
			this.gbExpiredLinks.Controls.Add(this.laAddDateValue);
			this.gbExpiredLinks.Controls.Add(this.laAddDateTitle);
			this.gbExpiredLinks.Enabled = false;
			this.gbExpiredLinks.Location = new System.Drawing.Point(3, 29);
			this.gbExpiredLinks.Name = "gbExpiredLinks";
			this.gbExpiredLinks.ShowCaption = false;
			this.gbExpiredLinks.Size = new System.Drawing.Size(525, 509);
			this.gbExpiredLinks.TabIndex = 3;
			// 
			// checkBoxLabelLink
			// 
			this.checkBoxLabelLink.AutoSize = true;
			this.checkBoxLabelLink.BackColor = System.Drawing.Color.Transparent;
			this.checkBoxLabelLink.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBoxLabelLink.ForeColor = System.Drawing.Color.Black;
			this.checkBoxLabelLink.Location = new System.Drawing.Point(9, 150);
			this.checkBoxLabelLink.Name = "checkBoxLabelLink";
			this.checkBoxLabelLink.Size = new System.Drawing.Size(164, 20);
			this.checkBoxLabelLink.TabIndex = 11;
			this.checkBoxLabelLink.Text = "Display EXPIRED Label";
			this.checkBoxLabelLink.UseVisualStyleBackColor = false;
			// 
			// timeEditExpirationTime
			// 
			this.timeEditExpirationTime.EditValue = new System.DateTime(2011, 8, 15, 0, 0, 0, 0);
			this.timeEditExpirationTime.Location = new System.Drawing.Point(135, 103);
			this.timeEditExpirationTime.Name = "timeEditExpirationTime";
			this.timeEditExpirationTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.timeEditExpirationTime.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.timeEditExpirationTime.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.timeEditExpirationTime.Properties.Appearance.Options.UseBackColor = true;
			this.timeEditExpirationTime.Properties.Appearance.Options.UseFont = true;
			this.timeEditExpirationTime.Properties.Appearance.Options.UseForeColor = true;
			this.timeEditExpirationTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.timeEditExpirationTime.Properties.EditValueChangedDelay = 10000;
			this.timeEditExpirationTime.Properties.HideSelection = false;
			this.timeEditExpirationTime.Size = new System.Drawing.Size(100, 22);
			this.timeEditExpirationTime.TabIndex = 10;
			// 
			// checkBoxSendEmailWhenDelete
			// 
			this.checkBoxSendEmailWhenDelete.AutoSize = true;
			this.checkBoxSendEmailWhenDelete.BackColor = System.Drawing.Color.Transparent;
			this.checkBoxSendEmailWhenDelete.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBoxSendEmailWhenDelete.ForeColor = System.Drawing.Color.Black;
			this.checkBoxSendEmailWhenDelete.Location = new System.Drawing.Point(9, 176);
			this.checkBoxSendEmailWhenDelete.Name = "checkBoxSendEmailWhenDelete";
			this.checkBoxSendEmailWhenDelete.Size = new System.Drawing.Size(282, 20);
			this.checkBoxSendEmailWhenDelete.TabIndex = 9;
			this.checkBoxSendEmailWhenDelete.Text = "Send Reminder Email to Admin List at Sync";
			this.checkBoxSendEmailWhenDelete.UseVisualStyleBackColor = false;
			// 
			// laExpireddateActions
			// 
			this.laExpireddateActions.AutoSize = true;
			this.laExpireddateActions.BackColor = System.Drawing.Color.Transparent;
			this.laExpireddateActions.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laExpireddateActions.ForeColor = System.Drawing.Color.Black;
			this.laExpireddateActions.Location = new System.Drawing.Point(6, 131);
			this.laExpireddateActions.Name = "laExpireddateActions";
			this.laExpireddateActions.Size = new System.Drawing.Size(153, 16);
			this.laExpireddateActions.TabIndex = 6;
			this.laExpireddateActions.Text = "When the Link Expires,";
			// 
			// dateEditExpirationDate
			// 
			this.dateEditExpirationDate.EditValue = null;
			this.dateEditExpirationDate.Location = new System.Drawing.Point(9, 103);
			this.dateEditExpirationDate.Name = "dateEditExpirationDate";
			this.dateEditExpirationDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditExpirationDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.dateEditExpirationDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditExpirationDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.dateEditExpirationDate.Properties.Appearance.Options.UseBackColor = true;
			this.dateEditExpirationDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditExpirationDate.Properties.Appearance.Options.UseForeColor = true;
			this.dateEditExpirationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditExpirationDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.dateEditExpirationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditExpirationDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditExpirationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditExpirationDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditExpirationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditExpirationDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditExpirationDate.Properties.NullText = "Select";
			this.dateEditExpirationDate.Properties.ShowPopupShadow = false;
			this.dateEditExpirationDate.Properties.ShowToday = false;
			this.dateEditExpirationDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditExpirationDate.Size = new System.Drawing.Size(120, 20);
			this.dateEditExpirationDate.TabIndex = 5;
			// 
			// laExpirationDateTitle
			// 
			this.laExpirationDateTitle.AutoSize = true;
			this.laExpirationDateTitle.BackColor = System.Drawing.Color.Transparent;
			this.laExpirationDateTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laExpirationDateTitle.ForeColor = System.Drawing.Color.Black;
			this.laExpirationDateTitle.Location = new System.Drawing.Point(6, 65);
			this.laExpirationDateTitle.Name = "laExpirationDateTitle";
			this.laExpirationDateTitle.Size = new System.Drawing.Size(140, 16);
			this.laExpirationDateTitle.TabIndex = 2;
			this.laExpirationDateTitle.Text = "This Link Expires on:";
			// 
			// laAddDateValue
			// 
			this.laAddDateValue.AutoSize = true;
			this.laAddDateValue.BackColor = System.Drawing.Color.Transparent;
			this.laAddDateValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAddDateValue.ForeColor = System.Drawing.Color.Black;
			this.laAddDateValue.Location = new System.Drawing.Point(6, 30);
			this.laAddDateValue.Name = "laAddDateValue";
			this.laAddDateValue.Size = new System.Drawing.Size(42, 16);
			this.laAddDateValue.TabIndex = 1;
			this.laAddDateValue.Text = "label1";
			// 
			// laAddDateTitle
			// 
			this.laAddDateTitle.AutoSize = true;
			this.laAddDateTitle.BackColor = System.Drawing.Color.Transparent;
			this.laAddDateTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAddDateTitle.ForeColor = System.Drawing.Color.Black;
			this.laAddDateTitle.Location = new System.Drawing.Point(6, 9);
			this.laAddDateTitle.Name = "laAddDateTitle";
			this.laAddDateTitle.Size = new System.Drawing.Size(104, 16);
			this.laAddDateTitle.TabIndex = 0;
			this.laAddDateTitle.Text = "Link Added on:";
			// 
			// checkBoxEnableExpiredLinks
			// 
			this.checkBoxEnableExpiredLinks.AutoSize = true;
			this.checkBoxEnableExpiredLinks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkBoxEnableExpiredLinks.ForeColor = System.Drawing.Color.Black;
			this.checkBoxEnableExpiredLinks.Location = new System.Drawing.Point(3, 3);
			this.checkBoxEnableExpiredLinks.Name = "checkBoxEnableExpiredLinks";
			this.checkBoxEnableExpiredLinks.Size = new System.Drawing.Size(173, 20);
			this.checkBoxEnableExpiredLinks.TabIndex = 2;
			this.checkBoxEnableExpiredLinks.Text = "Enable Expiration Date";
			this.checkBoxEnableExpiredLinks.UseVisualStyleBackColor = true;
			this.checkBoxEnableExpiredLinks.CheckedChanged += new System.EventHandler(this.checkBoxEnableExpiredLinks_CheckedChanged);
			// 
			// ExpiredDateOptions
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Controls.Add(this.gbExpiredLinks);
			this.Controls.Add(this.checkBoxEnableExpiredLinks);
			this.Size = new System.Drawing.Size(531, 541);
			this.Text = "Expiration Date";
			((System.ComponentModel.ISupportInitialize)(this.gbExpiredLinks)).EndInit();
			this.gbExpiredLinks.ResumeLayout(false);
			this.gbExpiredLinks.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.timeEditExpirationTime.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditExpirationDate.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.GroupControl gbExpiredLinks;
		private System.Windows.Forms.CheckBox checkBoxLabelLink;
		private DevExpress.XtraEditors.TimeEdit timeEditExpirationTime;
		private System.Windows.Forms.CheckBox checkBoxSendEmailWhenDelete;
		private System.Windows.Forms.Label laExpireddateActions;
		private DevExpress.XtraEditors.DateEdit dateEditExpirationDate;
		private System.Windows.Forms.Label laExpirationDateTitle;
		private System.Windows.Forms.Label laAddDateValue;
		private System.Windows.Forms.Label laAddDateTitle;
		private System.Windows.Forms.CheckBox checkBoxEnableExpiredLinks;
	}
}
