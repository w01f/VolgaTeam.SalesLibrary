using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class WordViewer : UserControl, IFileViewer
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

		public WordViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;

			if (!WordHelper.Instance.Open()) return;
			var g = Guid.NewGuid();
			var newFileName = Path.Combine(SettingsManager.Instance.TempPath, g + ".html");
			WordHelper.Instance.ConvertToHtml(File.LocalPath, newFileName);
			WordHelper.Instance.Close();
			webBrowser.Url = new Uri(newFileName);
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