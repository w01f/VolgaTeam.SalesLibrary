namespace OutlookSalesDepotAddIn
{
	partial class RibbonMail : Microsoft.Office.Tools.Ribbon.RibbonBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public RibbonMail()
			: base(Globals.Factory.GetRibbonFactory())
		{
			InitializeComponent();
		}

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
			this.tabSalesDepot = this.Factory.CreateRibbonTab();
			this.groupSalesDepot = this.Factory.CreateRibbonGroup();
			this.buttonSelectFile = this.Factory.CreateRibbonButton();
			this.tabSalesDepot.SuspendLayout();
			this.groupSalesDepot.SuspendLayout();
			// 
			// tabSalesDepot
			// 
			this.tabSalesDepot.Groups.Add(this.groupSalesDepot);
			this.tabSalesDepot.Label = "SALES LIBRARY";
			this.tabSalesDepot.Name = "tabSalesDepot";
			this.tabSalesDepot.Position = this.Factory.RibbonPosition.AfterOfficeId("TabNewMailMessage");
			// 
			// groupSalesDepot
			// 
			this.groupSalesDepot.Items.Add(this.buttonSelectFile);
			this.groupSalesDepot.Label = "Attach Files";
			this.groupSalesDepot.Name = "groupSalesDepot";
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
			this.buttonSelectFile.Image = global::OutlookSalesDepotAddIn.Properties.Resources.RibbonMailButtonLogo;
			this.buttonSelectFile.Label = " ";
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.ShowImage = true;
			this.buttonSelectFile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSelectFile_Click);
			// 
			// RibbonMail
			// 
			this.Name = "RibbonMail";
			this.RibbonType = "Microsoft.Outlook.Mail.Compose";
			this.Tabs.Add(this.tabSalesDepot);
			this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonMail_Load);
			this.tabSalesDepot.ResumeLayout(false);
			this.tabSalesDepot.PerformLayout();
			this.groupSalesDepot.ResumeLayout(false);
			this.groupSalesDepot.PerformLayout();

		}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab tabSalesDepot;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupSalesDepot;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSelectFile;
	}

	partial class ThisRibbonCollection
	{
		internal RibbonMail RibbonMail
		{
			get { return this.GetRibbon<RibbonMail>(); }
		}
	}
}
