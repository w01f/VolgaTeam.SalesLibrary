namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	partial class FormClonePage
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
			this.textEditPageName = new DevExpress.XtraEditors.TextEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.labelControlTitle2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControlTitle1 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.textEditPageName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// textEditPageName
			// 
			this.textEditPageName.Location = new System.Drawing.Point(12, 76);
			this.textEditPageName.Name = "textEditPageName";
			this.textEditPageName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditPageName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditPageName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditPageName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditPageName.Size = new System.Drawing.Size(329, 22);
			this.textEditPageName.StyleController = this.styleController;
			this.textEditPageName.TabIndex = 0;
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
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(12, 123);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(109, 31);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 1;
			this.buttonXOK.Text = "OK";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(232, 123);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(109, 31);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			// 
			// labelControlTitle2
			// 
			this.labelControlTitle2.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlTitle2.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTitle2.Location = new System.Drawing.Point(12, 54);
			this.labelControlTitle2.Name = "labelControlTitle2";
			this.labelControlTitle2.Size = new System.Drawing.Size(129, 16);
			this.labelControlTitle2.StyleController = this.styleController;
			this.labelControlTitle2.TabIndex = 3;
			this.labelControlTitle2.Text = "Name the NEW Page:";
			// 
			// labelControlTitle1
			// 
			this.labelControlTitle1.AllowHtmlString = true;
			this.labelControlTitle1.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlTitle1.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlTitle1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.labelControlTitle1.Location = new System.Drawing.Point(12, 12);
			this.labelControlTitle1.Name = "labelControlTitle1";
			this.labelControlTitle1.Size = new System.Drawing.Size(147, 16);
			this.labelControlTitle1.StyleController = this.styleController;
			this.labelControlTitle1.TabIndex = 4;
			this.labelControlTitle1.Text = "You are cloning page: <b>{0}</b>";
			this.labelControlTitle1.UseMnemonic = false;
			// 
			// FormClonePage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(353, 166);
			this.Controls.Add(this.labelControlTitle1);
			this.Controls.Add(this.labelControlTitle2);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.textEditPageName);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClonePage";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Create New Page";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClonePage_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.textEditPageName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.TextEdit textEditPageName;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlTitle2;
		private DevExpress.XtraEditors.LabelControl labelControlTitle1;
	}
}