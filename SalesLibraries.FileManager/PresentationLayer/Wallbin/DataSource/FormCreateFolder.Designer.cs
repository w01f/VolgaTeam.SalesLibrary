namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	partial class FormCreateFolder
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCreate = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			this.laName = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(291, 69);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCreate
			// 
			this.buttonXCreate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCreate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXCreate.Location = new System.Drawing.Point(182, 69);
			this.buttonXCreate.Name = "buttonXCreate";
			this.buttonXCreate.Size = new System.Drawing.Size(93, 32);
			this.buttonXCreate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCreate.TabIndex = 1;
			this.buttonXCreate.Text = "Create";
			this.buttonXCreate.TextColor = System.Drawing.Color.Black;
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
			// textEditName
			// 
			this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditName.Location = new System.Drawing.Point(15, 28);
			this.textEditName.Name = "textEditName";
			this.textEditName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditName.Size = new System.Drawing.Size(369, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 0;
			// 
			// laName
			// 
			this.laName.AutoSize = true;
			this.laName.BackColor = System.Drawing.Color.White;
			this.laName.ForeColor = System.Drawing.Color.Black;
			this.laName.Location = new System.Drawing.Point(12, 9);
			this.laName.Name = "laName";
			this.laName.Size = new System.Drawing.Size(82, 16);
			this.laName.TabIndex = 34;
			this.laName.Text = "Folder Name";
			// 
			// FormCreateFolder
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(393, 108);
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.laName);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXCreate);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ForeColor = System.Drawing.Color.Black;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCreateFolder";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create a New Subfolder";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCreateFolder_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXCreate;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.TextEdit textEditName;
		private System.Windows.Forms.Label laName;
	}
}