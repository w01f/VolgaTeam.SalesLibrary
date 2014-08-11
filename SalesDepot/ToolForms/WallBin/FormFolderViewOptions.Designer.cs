namespace SalesDepot.ToolForms.WallBin
{
	partial class FormFolderViewOptions
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
			this.buttonXOpen = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonXQuickSiteAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXQuickSiteEmail = new DevComponents.DotNetBar.ButtonX();
			this.toolTipController = new DevExpress.Utils.ToolTipController();
			this.SuspendLayout();
			// 
			// buttonXOpen
			// 
			this.buttonXOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpen.Image = global::SalesDepot.Properties.Resources.SearchBarOpen;
			this.buttonXOpen.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXOpen.Location = new System.Drawing.Point(20, 6);
			this.buttonXOpen.Name = "buttonXOpen";
			this.buttonXOpen.Size = new System.Drawing.Size(290, 55);
			this.buttonXOpen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpen.TabIndex = 0;
			this.buttonXOpen.Text = "   Open this folder";
			this.buttonXOpen.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXOpen.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXOpen, "Open this folder");
			this.toolTipController.SetToolTip(this.buttonXOpen, "Open this folder");
			this.buttonXOpen.Click += new System.EventHandler(this.buttonXOpen_Click);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(20, 201);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(290, 55);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 4;
			this.buttonXClose.Text = "   Close this Window";
			this.buttonXClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXClose, "Close");
			this.toolTipController.SetToolTip(this.buttonXClose, "Close this window");
			this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
			// 
			// buttonXQuickSiteAdd
			// 
			this.buttonXQuickSiteAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXQuickSiteAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXQuickSiteAdd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXQuickSiteAdd.Image = global::SalesDepot.Properties.Resources.QuickSiteAddLink;
			this.buttonXQuickSiteAdd.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXQuickSiteAdd.Location = new System.Drawing.Point(20, 136);
			this.buttonXQuickSiteAdd.Name = "buttonXQuickSiteAdd";
			this.buttonXQuickSiteAdd.Size = new System.Drawing.Size(290, 55);
			this.buttonXQuickSiteAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXQuickSiteAdd.TabIndex = 8;
			this.buttonXQuickSiteAdd.Text = "   Add to quickSITE";
			this.buttonXQuickSiteAdd.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXQuickSiteAdd.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXQuickSiteAdd, "Add to quickSITE");
			this.toolTipController.SetToolTip(this.buttonXQuickSiteAdd, "Place this link in your quickSITES cart");
			this.buttonXQuickSiteAdd.Click += new System.EventHandler(this.buttonXQuickSiteAdd_Click);
			// 
			// buttonXQuickSiteEmail
			// 
			this.buttonXQuickSiteEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXQuickSiteEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXQuickSiteEmail.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXQuickSiteEmail.Image = global::SalesDepot.Properties.Resources.QuickSiteEmailLink;
			this.buttonXQuickSiteEmail.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXQuickSiteEmail.Location = new System.Drawing.Point(20, 71);
			this.buttonXQuickSiteEmail.Name = "buttonXQuickSiteEmail";
			this.buttonXQuickSiteEmail.Size = new System.Drawing.Size(290, 55);
			this.buttonXQuickSiteEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXQuickSiteEmail.TabIndex = 7;
			this.buttonXQuickSiteEmail.Text = "   Email as Web Link";
			this.buttonXQuickSiteEmail.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXQuickSiteEmail.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXQuickSiteEmail, "Email as Web Link");
			this.toolTipController.SetToolTip(this.buttonXQuickSiteEmail, "Email a LINK to this file on the internet");
			this.buttonXQuickSiteEmail.Click += new System.EventHandler(this.buttonXQuickSiteEmail_Click);
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// FormFolderViewOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(331, 269);
			this.Controls.Add(this.buttonXQuickSiteAdd);
			this.Controls.Add(this.buttonXQuickSiteEmail);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXOpen);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFolderViewOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Folder Options - {0}";
			this.Load += new System.EventHandler(this.FormFolderViewOptions_Load);
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.ButtonX buttonXOpen;
        private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevComponents.DotNetBar.ButtonX buttonXQuickSiteAdd;
		private DevComponents.DotNetBar.ButtonX buttonXQuickSiteEmail;
		private DevExpress.Utils.ToolTipController toolTipController;

    }
}