namespace SalesDepot.ToolForms.WallBin
{
	partial class FormFolderSpecialOptions
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
			this.buttonXQuickSiteAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.toolTipController = new DevExpress.Utils.ToolTipController();
			this.SuspendLayout();
			// 
			// buttonXQuickSiteAdd
			// 
			this.buttonXQuickSiteAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXQuickSiteAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXQuickSiteAdd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXQuickSiteAdd.Image = global::SalesDepot.Properties.Resources.QuickSiteAddLink;
			this.buttonXQuickSiteAdd.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXQuickSiteAdd.Location = new System.Drawing.Point(12, 12);
			this.buttonXQuickSiteAdd.Name = "buttonXQuickSiteAdd";
			this.buttonXQuickSiteAdd.Size = new System.Drawing.Size(305, 73);
			this.buttonXQuickSiteAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXQuickSiteAdd.TabIndex = 6;
			this.buttonXQuickSiteAdd.Text = "   Add All Links in this \r\n  window to quickSITE";
			this.buttonXQuickSiteAdd.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXQuickSiteAdd.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXQuickSiteAdd, "Add all Links");
			this.toolTipController.SetToolTip(this.buttonXQuickSiteAdd, "Place all links from this window in your quickSITES cart");
			this.buttonXQuickSiteAdd.Click += new System.EventHandler(this.buttonXQuickSiteAdd_Click);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(12, 104);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(305, 55);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 4;
			this.buttonXClose.Text = "   Close this Window";
			this.buttonXClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXClose, "Close");
			this.toolTipController.SetToolTip(this.buttonXClose, "Close this window");
			this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// FormFolderSpecialOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(334, 171);
			this.Controls.Add(this.buttonXQuickSiteAdd);
			this.Controls.Add(this.buttonXClose);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFolderSpecialOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Folder Options - {0}";
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevComponents.DotNetBar.ButtonX buttonXQuickSiteAdd;
		private DevExpress.Utils.ToolTipController toolTipController;

    }
}