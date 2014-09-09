using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class ExcelViewer : UserControl, IFileViewer
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

		public ExcelViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;

			if (ExcelHelper.Instance.Connect())
			{
				Guid g = Guid.NewGuid();
				string newFileName = Path.Combine(SettingsManager.Instance.TempPath, g.ToString() + ".html");
				ExcelHelper.Instance.ConvertToHtml(File.LocalPath, newFileName);
				ExcelHelper.Instance.Disconnect();
				webBrowser.Url = new Uri(newFileName);
			}
		}

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources()
		{
			webBrowser.Navigate("about:blank");
		}
		#endregion
	}
}