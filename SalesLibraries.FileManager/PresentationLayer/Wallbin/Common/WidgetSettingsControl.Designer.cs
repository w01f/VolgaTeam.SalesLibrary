namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	partial class WidgetSettingsControl
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
			this.pbCustomWidget = new System.Windows.Forms.PictureBox();
			this.xtraTabControlWidgets = new DevExpress.XtraTab.XtraTabControl();
			this.radioButtonWidgetTypeCustom = new System.Windows.Forms.RadioButton();
			this.radioButtonWidgetTypeDisabled = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.pbCustomWidget)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).BeginInit();
			this.SuspendLayout();
			// 
			// pbCustomWidget
			// 
			this.pbCustomWidget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pbCustomWidget.BackColor = System.Drawing.Color.Transparent;
			this.pbCustomWidget.Enabled = false;
			this.pbCustomWidget.ForeColor = System.Drawing.Color.Black;
			this.pbCustomWidget.Location = new System.Drawing.Point(293, 497);
			this.pbCustomWidget.Name = "pbCustomWidget";
			this.pbCustomWidget.Size = new System.Drawing.Size(36, 36);
			this.pbCustomWidget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbCustomWidget.TabIndex = 8;
			this.pbCustomWidget.TabStop = false;
			// 
			// xtraTabControlWidgets
			// 
			this.xtraTabControlWidgets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControlWidgets.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraTabControlWidgets.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlWidgets.Appearance.Options.UseBackColor = true;
			this.xtraTabControlWidgets.Appearance.Options.UseForeColor = true;
			this.xtraTabControlWidgets.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlWidgets.Enabled = false;
			this.xtraTabControlWidgets.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlWidgets.Name = "xtraTabControlWidgets";
			this.xtraTabControlWidgets.Size = new System.Drawing.Size(920, 488);
			this.xtraTabControlWidgets.TabIndex = 9;
			// 
			// radioButtonWidgetTypeCustom
			// 
			this.radioButtonWidgetTypeCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonWidgetTypeCustom.AutoSize = true;
			this.radioButtonWidgetTypeCustom.BackColor = System.Drawing.Color.White;
			this.radioButtonWidgetTypeCustom.ForeColor = System.Drawing.Color.Black;
			this.radioButtonWidgetTypeCustom.Location = new System.Drawing.Point(172, 505);
			this.radioButtonWidgetTypeCustom.Name = "radioButtonWidgetTypeCustom";
			this.radioButtonWidgetTypeCustom.Size = new System.Drawing.Size(120, 20);
			this.radioButtonWidgetTypeCustom.TabIndex = 12;
			this.radioButtonWidgetTypeCustom.TabStop = true;
			this.radioButtonWidgetTypeCustom.Text = "Custom Widget:";
			this.radioButtonWidgetTypeCustom.UseVisualStyleBackColor = false;
			this.radioButtonWidgetTypeCustom.CheckedChanged += new System.EventHandler(this.OnWidgetTypeChanged);
			// 
			// radioButtonWidgetTypeDisabled
			// 
			this.radioButtonWidgetTypeDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonWidgetTypeDisabled.AutoSize = true;
			this.radioButtonWidgetTypeDisabled.BackColor = System.Drawing.Color.White;
			this.radioButtonWidgetTypeDisabled.ForeColor = System.Drawing.Color.Black;
			this.radioButtonWidgetTypeDisabled.Location = new System.Drawing.Point(6, 505);
			this.radioButtonWidgetTypeDisabled.Name = "radioButtonWidgetTypeDisabled";
			this.radioButtonWidgetTypeDisabled.Size = new System.Drawing.Size(87, 20);
			this.radioButtonWidgetTypeDisabled.TabIndex = 13;
			this.radioButtonWidgetTypeDisabled.TabStop = true;
			this.radioButtonWidgetTypeDisabled.Text = "No Widget";
			this.radioButtonWidgetTypeDisabled.UseVisualStyleBackColor = false;
			this.radioButtonWidgetTypeDisabled.CheckedChanged += new System.EventHandler(this.OnWidgetTypeChanged);
			// 
			// WidgetSettingsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.radioButtonWidgetTypeDisabled);
			this.Controls.Add(this.radioButtonWidgetTypeCustom);
			this.Controls.Add(this.xtraTabControlWidgets);
			this.Controls.Add(this.pbCustomWidget);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WidgetSettingsControl";
			this.Size = new System.Drawing.Size(920, 542);
			((System.ComponentModel.ISupportInitialize)(this.pbCustomWidget)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.PictureBox pbCustomWidget;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlWidgets;
		private System.Windows.Forms.RadioButton radioButtonWidgetTypeCustom;
		private System.Windows.Forms.RadioButton radioButtonWidgetTypeDisabled;
    }
}