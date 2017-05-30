namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
    partial class FormLinkSpecialOptions
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
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.buttonXEmailBin = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// buttonXEmailBin
			// 
			this.buttonXEmailBin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmailBin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmailBin.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXEmailBin.Image = global::SalesLibraries.SalesDepot.Properties.Resources.QuickViewEmail;
			this.buttonXEmailBin.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXEmailBin.Location = new System.Drawing.Point(13, 12);
			this.buttonXEmailBin.Name = "buttonXEmailBin";
			this.buttonXEmailBin.Size = new System.Drawing.Size(290, 55);
			this.buttonXEmailBin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmailBin.TabIndex = 7;
			this.buttonXEmailBin.Text = "   Attach to Email";
			this.buttonXEmailBin.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXEmailBin.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXEmailBin, "Email this File");
			this.toolTipController.SetToolTip(this.buttonXEmailBin, "Attach the ACTUAL File to an Outlook Email");
			this.buttonXEmailBin.Click += new System.EventHandler(this.buttonXEmailBin_Click);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesLibraries.SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(13, 84);
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
			// FormLinkSpecialOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(315, 159);
			this.Controls.Add(this.buttonXEmailBin);
			this.Controls.Add(this.buttonXClose);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLinkSpecialOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File Options - {0}";
			this.Load += new System.EventHandler(this.FormLinkSpecialOptions_Load);
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevExpress.Utils.ToolTipController toolTipController;
		public DevComponents.DotNetBar.ButtonX buttonXEmailBin;

    }
}