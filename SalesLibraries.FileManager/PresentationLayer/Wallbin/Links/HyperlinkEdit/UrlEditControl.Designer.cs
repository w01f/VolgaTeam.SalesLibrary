namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	partial class UrlEditControl
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
			this.labelControllTitle = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.ckBlueHyperlink = new System.Windows.Forms.CheckBox();
			this.ckForcePreview = new System.Windows.Forms.CheckBox();
			this.laPath = new System.Windows.Forms.Label();
			this.laName = new System.Windows.Forms.Label();
			this.textEditPath = new DevExpress.XtraEditors.TextEdit();
			this.textEditName = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControllTitle
			// 
			this.labelControllTitle.AllowHtmlString = true;
			this.labelControllTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControllTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControllTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControllTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControllTitle.Location = new System.Drawing.Point(14, 14);
			this.labelControllTitle.Name = "labelControllTitle";
			this.labelControllTitle.Size = new System.Drawing.Size(326, 27);
			this.labelControllTitle.StyleController = this.styleController;
			this.labelControllTitle.TabIndex = 36;
			this.labelControllTitle.Text = "<color=gray><i>Add a simple link to another website…</i></color>";
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
			// ckBlueHyperlink
			// 
			this.ckBlueHyperlink.AutoSize = true;
			this.ckBlueHyperlink.BackColor = System.Drawing.Color.White;
			this.ckBlueHyperlink.Checked = true;
			this.ckBlueHyperlink.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckBlueHyperlink.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckBlueHyperlink.ForeColor = System.Drawing.Color.Black;
			this.ckBlueHyperlink.Location = new System.Drawing.Point(14, 215);
			this.ckBlueHyperlink.Name = "ckBlueHyperlink";
			this.ckBlueHyperlink.Size = new System.Drawing.Size(120, 20);
			this.ckBlueHyperlink.TabIndex = 3;
			this.ckBlueHyperlink.Text = "Blue Hyperlink";
			this.ckBlueHyperlink.UseVisualStyleBackColor = false;
			// 
			// ckForcePreview
			// 
			this.ckForcePreview.AutoSize = true;
			this.ckForcePreview.BackColor = System.Drawing.Color.White;
			this.ckForcePreview.Checked = true;
			this.ckForcePreview.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckForcePreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckForcePreview.ForeColor = System.Drawing.Color.Black;
			this.ckForcePreview.Location = new System.Drawing.Point(14, 176);
			this.ckForcePreview.Name = "ckForcePreview";
			this.ckForcePreview.Size = new System.Drawing.Size(301, 20);
			this.ckForcePreview.TabIndex = 2;
			this.ckForcePreview.Text = "Immediately Launch this URL when clicked";
			this.ckForcePreview.UseVisualStyleBackColor = false;
			// 
			// laPath
			// 
			this.laPath.AutoSize = true;
			this.laPath.BackColor = System.Drawing.Color.White;
			this.laPath.ForeColor = System.Drawing.Color.Black;
			this.laPath.Location = new System.Drawing.Point(11, 113);
			this.laPath.Name = "laPath";
			this.laPath.Size = new System.Drawing.Size(63, 16);
			this.laPath.TabIndex = 33;
			this.laPath.Text = "Link Path";
			// 
			// laName
			// 
			this.laName.AutoSize = true;
			this.laName.BackColor = System.Drawing.Color.White;
			this.laName.ForeColor = System.Drawing.Color.Black;
			this.laName.Location = new System.Drawing.Point(11, 60);
			this.laName.Name = "laName";
			this.laName.Size = new System.Drawing.Size(70, 16);
			this.laName.TabIndex = 32;
			this.laName.Text = "Link Name";
			// 
			// textEditPath
			// 
			this.textEditPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditPath.Location = new System.Drawing.Point(14, 132);
			this.textEditPath.Name = "textEditPath";
			this.textEditPath.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditPath.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditPath.Properties.Appearance.Options.UseBackColor = true;
			this.textEditPath.Properties.Appearance.Options.UseForeColor = true;
			this.textEditPath.Size = new System.Drawing.Size(326, 22);
			this.textEditPath.StyleController = this.styleController;
			this.textEditPath.TabIndex = 1;
			// 
			// textEditName
			// 
			this.textEditName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditName.Location = new System.Drawing.Point(14, 79);
			this.textEditName.Name = "textEditName";
			this.textEditName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditName.Size = new System.Drawing.Size(326, 22);
			this.textEditName.StyleController = this.styleController;
			this.textEditName.TabIndex = 0;
			// 
			// UrlEditControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.textEditName);
			this.Controls.Add(this.labelControllTitle);
			this.Controls.Add(this.ckBlueHyperlink);
			this.Controls.Add(this.ckForcePreview);
			this.Controls.Add(this.laPath);
			this.Controls.Add(this.laName);
			this.Controls.Add(this.textEditPath);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "UrlEditControl";
			this.Size = new System.Drawing.Size(350, 280);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPath.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControllTitle;
		private DevExpress.XtraEditors.StyleController styleController;
		public System.Windows.Forms.CheckBox ckBlueHyperlink;
		public System.Windows.Forms.CheckBox ckForcePreview;
		private System.Windows.Forms.Label laPath;
		private System.Windows.Forms.Label laName;
		private DevExpress.XtraEditors.TextEdit textEditPath;
		private DevExpress.XtraEditors.TextEdit textEditName;
	}
}
