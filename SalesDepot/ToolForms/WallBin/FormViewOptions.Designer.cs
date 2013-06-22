namespace SalesDepot.ToolForms.WallBin
{
    partial class FormViewOptions
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
			this.buttonXQuickSiteAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXQuickSiteEmail = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
			this.buttonXPrint = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpen = new DevComponents.DotNetBar.ButtonX();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.SuspendLayout();
			// 
			// buttonXQuickSiteAdd
			// 
			this.buttonXQuickSiteAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXQuickSiteAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXQuickSiteAdd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXQuickSiteAdd.Image = global::SalesDepot.Properties.Resources.QuickSiteAddLink;
			this.buttonXQuickSiteAdd.Location = new System.Drawing.Point(20, 339);
			this.buttonXQuickSiteAdd.Name = "buttonXQuickSiteAdd";
			this.buttonXQuickSiteAdd.Size = new System.Drawing.Size(290, 55);
			this.buttonXQuickSiteAdd.TabIndex = 6;
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
			this.buttonXQuickSiteEmail.Location = new System.Drawing.Point(20, 274);
			this.buttonXQuickSiteEmail.Name = "buttonXQuickSiteEmail";
			this.buttonXQuickSiteEmail.Size = new System.Drawing.Size(290, 55);
			this.buttonXQuickSiteEmail.TabIndex = 5;
			this.buttonXQuickSiteEmail.Text = "   Email as Web Link";
			this.buttonXQuickSiteEmail.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXQuickSiteEmail.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXQuickSiteEmail, "Email as Web Link");
			this.toolTipController.SetToolTip(this.buttonXQuickSiteEmail, "Email a LINK to this file on the internet");
			this.buttonXQuickSiteEmail.Click += new System.EventHandler(this.buttonXQuickSiteEmail_Click);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(20, 404);
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
			// buttonXEmail
			// 
			this.buttonXEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmail.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXEmail.Image = global::SalesDepot.Properties.Resources.SearchBarEmail;
			this.buttonXEmail.Location = new System.Drawing.Point(20, 209);
			this.buttonXEmail.Name = "buttonXEmail";
			this.buttonXEmail.Size = new System.Drawing.Size(290, 55);
			this.buttonXEmail.TabIndex = 3;
			this.buttonXEmail.Text = "   Attach to email";
			this.buttonXEmail.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXEmail.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXEmail, "Attach to email");
			this.toolTipController.SetToolTip(this.buttonXEmail, "Attach this file to an email and send it");
			this.buttonXEmail.Click += new System.EventHandler(this.buttonXEmail_Click);
			// 
			// buttonXPrint
			// 
			this.buttonXPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXPrint.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXPrint.Image = global::SalesDepot.Properties.Resources.SearchBarPrint;
			this.buttonXPrint.Location = new System.Drawing.Point(20, 144);
			this.buttonXPrint.Name = "buttonXPrint";
			this.buttonXPrint.Size = new System.Drawing.Size(290, 55);
			this.buttonXPrint.TabIndex = 2;
			this.buttonXPrint.Text = "   Send to Printer";
			this.buttonXPrint.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXPrint.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXPrint, "Send to Printer");
			this.toolTipController.SetToolTip(this.buttonXPrint, "Print this file");
			this.buttonXPrint.Click += new System.EventHandler(this.buttonXPrint_Click);
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXSave.Image = global::SalesDepot.Properties.Resources.SearchBarSave;
			this.buttonXSave.Location = new System.Drawing.Point(20, 79);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(290, 55);
			this.buttonXSave.TabIndex = 1;
			this.buttonXSave.Text = "   Save a copy";
			this.buttonXSave.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXSave, "Save a copy");
			this.toolTipController.SetToolTip(this.buttonXSave, "Save a copy of this file");
			this.buttonXSave.Click += new System.EventHandler(this.buttonXSave_Click);
			// 
			// buttonXOpen
			// 
			this.buttonXOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpen.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpen.Image = global::SalesDepot.Properties.Resources.SearchBarOpen;
			this.buttonXOpen.Location = new System.Drawing.Point(20, 14);
			this.buttonXOpen.Name = "buttonXOpen";
			this.buttonXOpen.Size = new System.Drawing.Size(290, 55);
			this.buttonXOpen.TabIndex = 0;
			this.buttonXOpen.Text = "   Open this file";
			this.buttonXOpen.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXOpen.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXOpen, "Open this file");
			this.toolTipController.SetToolTip(this.buttonXOpen, "Open a copy of this file");
			this.buttonXOpen.Click += new System.EventHandler(this.buttonXOpen_Click);
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// FormViewOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(331, 472);
			this.Controls.Add(this.buttonXQuickSiteAdd);
			this.Controls.Add(this.buttonXQuickSiteEmail);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXEmail);
			this.Controls.Add(this.buttonXPrint);
			this.Controls.Add(this.buttonXSave);
			this.Controls.Add(this.buttonXOpen);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormViewOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File Options - {0}";
			this.Load += new System.EventHandler(this.ViewOptionsForm_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOpen;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        private DevComponents.DotNetBar.ButtonX buttonXPrint;
        private DevComponents.DotNetBar.ButtonX buttonXEmail;
        private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevComponents.DotNetBar.ButtonX buttonXQuickSiteEmail;
		private DevComponents.DotNetBar.ButtonX buttonXQuickSiteAdd;
		private DevExpress.Utils.ToolTipController toolTipController;

    }
}