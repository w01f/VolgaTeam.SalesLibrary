namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkTextOptions
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
			this.colorEditLinkSpecialColor = new DevExpress.XtraEditors.ColorEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.buttonEditLinkSpecialFont = new DevExpress.XtraEditors.ButtonEdit();
			this.rbLinkSpecialFormat = new System.Windows.Forms.RadioButton();
			this.rbLinkBoldFormat = new System.Windows.Forms.RadioButton();
			this.rbLinkRegularFormat = new System.Windows.Forms.RadioButton();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControlForeColor = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.colorEditLinkSpecialColor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLinkSpecialFont.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// colorEditLinkSpecialColor
			// 
			this.colorEditLinkSpecialColor.EditValue = System.Drawing.Color.Empty;
			this.colorEditLinkSpecialColor.Location = new System.Drawing.Point(128, 169);
			this.colorEditLinkSpecialColor.Name = "colorEditLinkSpecialColor";
			this.colorEditLinkSpecialColor.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.colorEditLinkSpecialColor.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.colorEditLinkSpecialColor.Properties.Appearance.Options.UseBackColor = true;
			this.colorEditLinkSpecialColor.Properties.Appearance.Options.UseForeColor = true;
			this.colorEditLinkSpecialColor.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.colorEditLinkSpecialColor.Properties.AppearanceDisabled.Options.UseBackColor = true;
			this.colorEditLinkSpecialColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.colorEditLinkSpecialColor.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colorEditLinkSpecialColor.Properties.ShowSystemColors = false;
			this.colorEditLinkSpecialColor.Size = new System.Drawing.Size(105, 22);
			this.colorEditLinkSpecialColor.StyleController = this.styleController;
			this.colorEditLinkSpecialColor.TabIndex = 31;
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
			// buttonEditLinkSpecialFont
			// 
			this.buttonEditLinkSpecialFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditLinkSpecialFont.Enabled = false;
			this.buttonEditLinkSpecialFont.Location = new System.Drawing.Point(128, 110);
			this.buttonEditLinkSpecialFont.Name = "buttonEditLinkSpecialFont";
			this.buttonEditLinkSpecialFont.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditLinkSpecialFont.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditLinkSpecialFont.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditLinkSpecialFont.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditLinkSpecialFont.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.buttonEditLinkSpecialFont.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.buttonEditLinkSpecialFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditLinkSpecialFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.buttonEditLinkSpecialFont.Size = new System.Drawing.Size(400, 22);
			this.buttonEditLinkSpecialFont.StyleController = this.styleController;
			this.buttonEditLinkSpecialFont.TabIndex = 30;
			this.buttonEditLinkSpecialFont.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FontEdit_ButtonClick);
			this.buttonEditLinkSpecialFont.Click += new System.EventHandler(this.FontEdit_Click);
			// 
			// rbLinkSpecialFormat
			// 
			this.rbLinkSpecialFormat.AutoSize = true;
			this.rbLinkSpecialFormat.BackColor = System.Drawing.Color.White;
			this.rbLinkSpecialFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.rbLinkSpecialFormat.ForeColor = System.Drawing.Color.Black;
			this.rbLinkSpecialFormat.Location = new System.Drawing.Point(8, 111);
			this.rbLinkSpecialFormat.Name = "rbLinkSpecialFormat";
			this.rbLinkSpecialFormat.Size = new System.Drawing.Size(123, 20);
			this.rbLinkSpecialFormat.TabIndex = 2;
			this.rbLinkSpecialFormat.Text = "Special Format";
			this.rbLinkSpecialFormat.UseVisualStyleBackColor = false;
			this.rbLinkSpecialFormat.CheckedChanged += new System.EventHandler(this.rbLinkSpecialFormat_CheckedChanged);
			// 
			// rbLinkBoldFormat
			// 
			this.rbLinkBoldFormat.AutoSize = true;
			this.rbLinkBoldFormat.BackColor = System.Drawing.Color.White;
			this.rbLinkBoldFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbLinkBoldFormat.ForeColor = System.Drawing.Color.Black;
			this.rbLinkBoldFormat.Location = new System.Drawing.Point(132, 52);
			this.rbLinkBoldFormat.Name = "rbLinkBoldFormat";
			this.rbLinkBoldFormat.Size = new System.Drawing.Size(62, 20);
			this.rbLinkBoldFormat.TabIndex = 1;
			this.rbLinkBoldFormat.Text = "BOLD";
			this.rbLinkBoldFormat.UseVisualStyleBackColor = false;
			// 
			// rbLinkRegularFormat
			// 
			this.rbLinkRegularFormat.AutoSize = true;
			this.rbLinkRegularFormat.BackColor = System.Drawing.Color.White;
			this.rbLinkRegularFormat.Checked = true;
			this.rbLinkRegularFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.rbLinkRegularFormat.ForeColor = System.Drawing.Color.Black;
			this.rbLinkRegularFormat.Location = new System.Drawing.Point(8, 52);
			this.rbLinkRegularFormat.Name = "rbLinkRegularFormat";
			this.rbLinkRegularFormat.Size = new System.Drawing.Size(72, 20);
			this.rbLinkRegularFormat.TabIndex = 0;
			this.rbLinkRegularFormat.TabStop = true;
			this.rbLinkRegularFormat.Text = "Normal";
			this.rbLinkRegularFormat.UseVisualStyleBackColor = false;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
			this.labelControlTitle.Location = new System.Drawing.Point(8, 12);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(305, 17);
			this.labelControlTitle.TabIndex = 24;
			this.labelControlTitle.Text = "Change the font style or text color of your link…";
			// 
			// labelControlForeColor
			// 
			this.labelControlForeColor.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlForeColor.Location = new System.Drawing.Point(26, 173);
			this.labelControlForeColor.Name = "labelControlForeColor";
			this.labelControlForeColor.Size = new System.Drawing.Size(60, 16);
			this.labelControlForeColor.TabIndex = 31;
			this.labelControlForeColor.Text = "Text Color";
			// 
			// LinkTextOptions
			// 
			this.Controls.Add(this.colorEditLinkSpecialColor);
			this.Controls.Add(this.labelControlForeColor);
			this.Controls.Add(this.labelControlTitle);
			this.Controls.Add(this.buttonEditLinkSpecialFont);
			this.Controls.Add(this.rbLinkSpecialFormat);
			this.Controls.Add(this.rbLinkRegularFormat);
			this.Controls.Add(this.rbLinkBoldFormat);
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.colorEditLinkSpecialColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditLinkSpecialFont.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public DevExpress.XtraEditors.ColorEdit colorEditLinkSpecialColor;
		public DevExpress.XtraEditors.ButtonEdit buttonEditLinkSpecialFont;
		public System.Windows.Forms.RadioButton rbLinkSpecialFormat;
		public System.Windows.Forms.RadioButton rbLinkBoldFormat;
		public System.Windows.Forms.RadioButton rbLinkRegularFormat;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.LabelControl labelControlForeColor;
	}
}
