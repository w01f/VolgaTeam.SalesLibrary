namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
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
			this.components = new System.ComponentModel.Container();
			this.buttonXOpen = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.SuspendLayout();
			// 
			// buttonXOpen
			// 
			this.buttonXOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpen.Image = global::SalesLibraries.SalesDepot.Properties.Resources.QuickViewOpen;
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
			this.buttonXClose.Image = global::SalesLibraries.SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(20, 82);
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
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// FormFolderViewOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(331, 146);
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
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.ButtonX buttonXOpen;
		private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevExpress.Utils.ToolTipController toolTipController;

    }
}