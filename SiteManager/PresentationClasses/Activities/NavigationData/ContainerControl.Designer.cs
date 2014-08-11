namespace SalesDepot.SiteManager.PresentationClasses.Activities.NavigationData
{
	public partial class ContainerControl
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
			this.xtraTabControlGroups = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGroups)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraTabControlGroups
			// 
			this.xtraTabControlGroups.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGroups.Appearance.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlGroups.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlGroups.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlGroups.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlGroups.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlGroups.Name = "xtraTabControlGroups";
			this.xtraTabControlGroups.Size = new System.Drawing.Size(898, 483);
			this.xtraTabControlGroups.TabIndex = 2;
			this.xtraTabControlGroups.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlGroups_SelectedPageChanged);
			// 
			// ContainerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.xtraTabControlGroups);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ContainerControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGroups)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControlGroups;

	}
}
