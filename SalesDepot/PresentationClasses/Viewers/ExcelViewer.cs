using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.InteropClasses;
using SalesDepot.ToolForms.WallBin;

namespace SalesDepot.PresentationClasses.Viewers
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
		public void ReleaseResources()
		{
			webBrowser.Navigate("about:blank");
		}

		public void Open()
		{
			LinkManager.Instance.OpenCopyOfFile(File);
		}

		public void Save()
		{
			LinkManager.Instance.SaveFile("Save copy of the file as", File);
		}

		public void Email()
		{
			using (var form = new FormEmailLink())
			{
				form.link = File;
				form.ShowDialog();
			}
		}

		public void Print()
		{
			LinkManager.Instance.PrintFile(File);
		}

		public void EmailLinkToQuickSite()
		{
			LinkManager.Instance.EmailLinkToQuickSite(File);
		}

		public void AddLinkToQuickSite()
		{
			LinkManager.Instance.AddLinkToQuickSite(File);
		}
		#endregion
	}
}