using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(ExcelLink))]
	[ToolboxItem(false)]
	public partial class ExcelViewer : UserControl, ILinkViewer
	{
		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName
		{
			get { return Link.Name; }
		}
		#endregion

		public ExcelViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;

			if (ExcelHelper.Instance.Connect())
			{
				var g = Guid.NewGuid();
				var newFileName = Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, g + ".html");
				ExcelHelper.Instance.ConvertToHtml(Link.FullPath, newFileName);
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
			LinkManager.OpenCopyOfFile((ExcelLink)Link);
		}

		public void Save()
		{
			LinkManager.SaveLink("Save copy of the file as", (ExcelLink)Link);
		}

		public void Email()
		{
			using (var form = new FormEmailLink())
			{
				form.FileLink = (ExcelLink)Link;
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}

		public void Print()
		{
			LinkManager.PrintFile((ExcelLink)Link);
		}
		#endregion
	}
}