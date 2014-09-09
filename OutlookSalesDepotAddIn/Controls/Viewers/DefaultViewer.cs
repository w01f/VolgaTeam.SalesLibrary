using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class DefaultViewer : UserControl, IFileViewer
	{
		#region Properties
		public LibraryLink File { get; private set; }

		public string DisplayName
		{
			get { return File.DisplayName; }
		}

		public string CriteriaOverlap
		{
			get { return File.CriteriaOverlap; }
		}

		public Image Widget
		{
			get { return File.Widget; }
		}
		#endregion

		public DefaultViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;

			switch (File.Type)
			{
				case FileTypes.BuggyPresentation:
				case FileTypes.FriendlyPresentation:
				case FileTypes.Presentation:
					laMessage.Text = "Double-Click PowerPoint files to preview";
					break;
				default:
					laMessage.Text = "Double-Click File to preview...";
					break;
			}
		}

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources() { }
		#endregion

		private void laMessage_DoubleClick(object sender, EventArgs e)
		{
			LinkManager.Instance.OpenLink(File);
		}
	}
}