namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.BundleList
{
	partial class FormAddBundle
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
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonXApply = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.labelControlName = new DevExpress.XtraEditors.LabelControl();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// textEditName
			// 
			this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditName.Location = new System.Drawing.Point(98, 52);
			this.textEditName.Name = "textEditName";
			this.textEditName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditName.Size = new System.Drawing.Size(283, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 0;
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
			// buttonXApply
			// 
			this.buttonXApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXApply.Location = new System.Drawing.Point(125, 99);
			this.buttonXApply.Name = "buttonXApply";
			this.buttonXApply.Size = new System.Drawing.Size(109, 31);
			this.buttonXApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXApply.TabIndex = 1;
			this.buttonXApply.Text = "OK";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(272, 99);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(109, 31);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			// 
			// labelControlName
			// 
			this.labelControlName.AllowHtmlString = true;
			this.labelControlName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlName.Location = new System.Drawing.Point(98, 12);
			this.labelControlName.Name = "labelControlName";
			this.labelControlName.Size = new System.Drawing.Size(283, 16);
			this.labelControlName.StyleController = this.styleController;
			this.labelControlName.TabIndex = 3;
			this.labelControlName.Text = "<b>Link Bundle Name</b>";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::SalesLibraries.FileManager.Properties.Resources.BundlesFormNew;
			this.pictureBox1.Location = new System.Drawing.Point(12, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(70, 71);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// FormAddBundle
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(393, 142);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.labelControlName);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXApply);
			this.Controls.Add(this.textEditName);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddBundle";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Create a new Link Bundle";
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.TextEdit textEditName;
		private DevComponents.DotNetBar.ButtonX buttonXApply;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlName;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}