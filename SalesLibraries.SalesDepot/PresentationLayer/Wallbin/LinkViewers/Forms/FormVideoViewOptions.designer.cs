namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
    partial class FormVideoViewOptions
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
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonXAddToPresentation = new DevComponents.DotNetBar.ButtonX();
			this.buttonXReview = new DevComponents.DotNetBar.ButtonX();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.SuspendLayout();
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXClose.Image = global::SalesLibraries.SalesDepot.Properties.Resources.Cancel;
			this.buttonXClose.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXClose.Location = new System.Drawing.Point(12, 142);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(317, 55);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 7;
			this.buttonXClose.Text = "   Close this Window";
			this.buttonXClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXClose, "Close");
			this.toolTipController.SetToolTip(this.buttonXClose, "Close this window");
			this.buttonXClose.Click += new System.EventHandler(this.buttonXClose_Click);
			// 
			// buttonXAddToPresentation
			// 
			this.buttonXAddToPresentation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAddToPresentation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAddToPresentation.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXAddToPresentation.Image = global::SalesLibraries.SalesDepot.Properties.Resources.QuickViewAdd;
			this.buttonXAddToPresentation.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXAddToPresentation.Location = new System.Drawing.Point(12, 12);
			this.buttonXAddToPresentation.Name = "buttonXAddToPresentation";
			this.buttonXAddToPresentation.Size = new System.Drawing.Size(317, 55);
			this.buttonXAddToPresentation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAddToPresentation.TabIndex = 6;
			this.buttonXAddToPresentation.Text = "   Add to presentation";
			this.buttonXAddToPresentation.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXAddToPresentation.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXAddToPresentation, "Add to presentation");
			this.toolTipController.SetToolTip(this.buttonXAddToPresentation, "Add this video to presentation");
			this.buttonXAddToPresentation.Click += new System.EventHandler(this.buttonXAddToPresentation_Click);
			// 
			// buttonXReview
			// 
			this.buttonXReview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXReview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXReview.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXReview.Image = global::SalesLibraries.SalesDepot.Properties.Resources.QuickViewOpen;
			this.buttonXReview.ImageFixedSize = new System.Drawing.Size(48, 48);
			this.buttonXReview.Location = new System.Drawing.Point(12, 77);
			this.buttonXReview.Name = "buttonXReview";
			this.buttonXReview.Size = new System.Drawing.Size(317, 55);
			this.buttonXReview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXReview.TabIndex = 5;
			this.buttonXReview.Text = "   Review video clip";
			this.buttonXReview.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXReview.TextColor = System.Drawing.Color.Black;
			this.toolTipController.SetTitle(this.buttonXReview, "Review video clip");
			this.toolTipController.SetToolTip(this.buttonXReview, "Open this clip in Video Player");
			this.buttonXReview.Click += new System.EventHandler(this.buttonXReview_Click);
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// FormVideoViewOptions
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(341, 203);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXAddToPresentation);
			this.Controls.Add(this.buttonXReview);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormVideoViewOptions";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Video Options - {0}";
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXClose;
        private DevComponents.DotNetBar.ButtonX buttonXAddToPresentation;
		private DevComponents.DotNetBar.ButtonX buttonXReview;
		private DevExpress.Utils.ToolTipController toolTipController;

    }
}