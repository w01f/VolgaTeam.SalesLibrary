namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
    partial class FormAddHyperLink
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.pnEditContainer = new System.Windows.Forms.Panel();
			this.buttonXLan = new DevComponents.DotNetBar.ButtonX();
			this.buttonXYouTube = new DevComponents.DotNetBar.ButtonX();
			this.buttonXUrl = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(398, 354);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 16;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(289, 354);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(93, 32);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 15;
			this.buttonXSave.Text = "Save";
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			// 
			// pnEditContainer
			// 
			this.pnEditContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnEditContainer.BackColor = System.Drawing.Color.Transparent;
			this.pnEditContainer.ForeColor = System.Drawing.Color.Black;
			this.pnEditContainer.Location = new System.Drawing.Point(0, 68);
			this.pnEditContainer.Name = "pnEditContainer";
			this.pnEditContainer.Size = new System.Drawing.Size(498, 276);
			this.pnEditContainer.TabIndex = 33;
			// 
			// buttonXLan
			// 
			this.buttonXLan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLan.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLan.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkAddNetwork;
			this.buttonXLan.Location = new System.Drawing.Point(276, 12);
			this.buttonXLan.Name = "buttonXLan";
			this.buttonXLan.Size = new System.Drawing.Size(103, 46);
			this.buttonXLan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLan.TabIndex = 32;
			this.buttonXLan.Tag = "2";
			this.buttonXLan.Text = "LAN";
			// 
			// buttonXYouTube
			// 
			this.buttonXYouTube.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXYouTube.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXYouTube.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkAddYoutube;
			this.buttonXYouTube.Location = new System.Drawing.Point(144, 12);
			this.buttonXYouTube.Name = "buttonXYouTube";
			this.buttonXYouTube.Size = new System.Drawing.Size(103, 46);
			this.buttonXYouTube.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXYouTube.TabIndex = 31;
			this.buttonXYouTube.Tag = "1";
			this.buttonXYouTube.Text = "YouTube";
			// 
			// buttonXUrl
			// 
			this.buttonXUrl.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUrl.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUrl.Image = global::SalesLibraries.FileManager.Properties.Resources.LinkAddUrl;
			this.buttonXUrl.Location = new System.Drawing.Point(12, 12);
			this.buttonXUrl.Name = "buttonXUrl";
			this.buttonXUrl.Size = new System.Drawing.Size(103, 46);
			this.buttonXUrl.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUrl.TabIndex = 30;
			this.buttonXUrl.Tag = "0";
			this.buttonXUrl.Text = "URL";
			// 
			// FormAddHyperLink
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(498, 393);
			this.Controls.Add(this.pnEditContainer);
			this.Controls.Add(this.buttonXLan);
			this.Controls.Add(this.buttonXYouTube);
			this.Controls.Add(this.buttonXUrl);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAddHyperLink";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add a Hyperlink";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddLinkForm_FormClosing);
			this.ResumeLayout(false);

        }

        #endregion
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXSave;
		private DevComponents.DotNetBar.ButtonX buttonXUrl;
		private DevComponents.DotNetBar.ButtonX buttonXYouTube;
		private DevComponents.DotNetBar.ButtonX buttonXLan;
		private System.Windows.Forms.Panel pnEditContainer;
	}
}