namespace SalesDepot.ToolForms.QBuilderForms
{
    partial class FormBrowserSelector
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
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpera = new DevComponents.DotNetBar.ButtonX();
			this.buttonXIE = new DevComponents.DotNetBar.ButtonX();
			this.buttonXFirefox = new DevComponents.DotNetBar.ButtonX();
			this.buttonXChrome = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(20, 276);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(290, 55);
			this.buttonXClose.TabIndex = 4;
			this.buttonXClose.Text = "   Close this Window";
			this.buttonXClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXClose, "Close");
			this.toolTipController.SetToolTip(this.buttonXClose, "Close this window");
			this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
			// 
			// buttonXOpera
			// 
			this.buttonXOpera.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpera.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpera.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpera.Image = global::SalesDepot.Properties.Resources.QBuilderBrowserOpera;
			this.buttonXOpera.Location = new System.Drawing.Point(20, 209);
			this.buttonXOpera.Name = "buttonXOpera";
			this.buttonXOpera.Size = new System.Drawing.Size(290, 55);
			this.buttonXOpera.TabIndex = 3;
			this.buttonXOpera.Text = "   Opera";
			this.buttonXOpera.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXOpera.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXOpera, "Opera");
			this.toolTipController.SetToolTip(this.buttonXOpera, "Open quickSITE in Opera");
			this.buttonXOpera.Click += new System.EventHandler(this.buttonXOpera_Click);
			// 
			// buttonXIE
			// 
			this.buttonXIE.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXIE.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXIE.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXIE.Image = global::SalesDepot.Properties.Resources.QBuilderBrowserInternetExplorer;
			this.buttonXIE.Location = new System.Drawing.Point(20, 144);
			this.buttonXIE.Name = "buttonXIE";
			this.buttonXIE.Size = new System.Drawing.Size(290, 55);
			this.buttonXIE.TabIndex = 2;
			this.buttonXIE.Text = "   Internet Explorer";
			this.buttonXIE.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXIE.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXIE, "Internet Explorer");
			this.toolTipController.SetToolTip(this.buttonXIE, "Open quickSITE in Internet Explorer");
			this.buttonXIE.Click += new System.EventHandler(this.buttonXIE_Click);
			// 
			// buttonXFirefox
			// 
			this.buttonXFirefox.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXFirefox.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXFirefox.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXFirefox.Image = global::SalesDepot.Properties.Resources.QBuilderBrowserFirefox;
			this.buttonXFirefox.Location = new System.Drawing.Point(20, 79);
			this.buttonXFirefox.Name = "buttonXFirefox";
			this.buttonXFirefox.Size = new System.Drawing.Size(290, 55);
			this.buttonXFirefox.TabIndex = 1;
			this.buttonXFirefox.Text = "   Firefox";
			this.buttonXFirefox.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXFirefox.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXFirefox, "Firefox");
			this.toolTipController.SetToolTip(this.buttonXFirefox, "Open quickSITE in Firefox");
			this.buttonXFirefox.Click += new System.EventHandler(this.buttonXFirefox_Click);
			// 
			// buttonXChrome
			// 
			this.buttonXChrome.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXChrome.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXChrome.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXChrome.Image = global::SalesDepot.Properties.Resources.QBuilderBrowserChrome;
			this.buttonXChrome.Location = new System.Drawing.Point(20, 14);
			this.buttonXChrome.Name = "buttonXChrome";
			this.buttonXChrome.Size = new System.Drawing.Size(290, 55);
			this.buttonXChrome.TabIndex = 0;
			this.buttonXChrome.Text = "   Google Chrome";
			this.buttonXChrome.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXChrome.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXChrome, "Google Chrome");
			this.toolTipController.SetToolTip(this.buttonXChrome, "Open quickSITE in Google Chrome");
			this.buttonXChrome.Click += new System.EventHandler(this.buttonXChrome_Click);
			// 
			// FormBrowserSelector
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(331, 345);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXOpera);
			this.Controls.Add(this.buttonXIE);
			this.Controls.Add(this.buttonXFirefox);
			this.Controls.Add(this.buttonXChrome);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBrowserSelector";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Preview this quickSITE in";
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXChrome;
        private DevComponents.DotNetBar.ButtonX buttonXFirefox;
        private DevComponents.DotNetBar.ButtonX buttonXIE;
        private DevComponents.DotNetBar.ButtonX buttonXOpera;
		private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevExpress.Utils.ToolTipController toolTipController;

    }
}