namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class LinkExcelOptions
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.ckDoNotGenerateText = new System.Windows.Forms.CheckBox();
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
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
			this.labelControlTitle.Location = new System.Drawing.Point(8, 12);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(427, 17);
			this.labelControlTitle.TabIndex = 24;
			this.labelControlTitle.Text = "You may want to apply these special, advanced settings to the link";
			// 
			// ckDoNotGenerateText
			// 
			this.ckDoNotGenerateText.AutoSize = true;
			this.ckDoNotGenerateText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckDoNotGenerateText.ForeColor = System.Drawing.Color.Black;
			this.ckDoNotGenerateText.Location = new System.Drawing.Point(8, 50);
			this.ckDoNotGenerateText.Name = "ckDoNotGenerateText";
			this.ckDoNotGenerateText.Size = new System.Drawing.Size(452, 20);
			this.ckDoNotGenerateText.TabIndex = 25;
			this.ckDoNotGenerateText.Text = "Do NOT Create Full Data File (Always select this for Nielsen Books) ";
			this.ckDoNotGenerateText.UseVisualStyleBackColor = true;
			// 
			// LinkExcelOptions
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.ckDoNotGenerateText);
			this.Controls.Add(this.labelControlTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LinkExcelOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.CheckBox ckDoNotGenerateText;
	}
}
