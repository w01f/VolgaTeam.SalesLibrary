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
	[IntendForClass(typeof(WordLink))]
	[ToolboxItem(false)]
	public partial class WordViewer : UserControl, ILinkViewer
	{
		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName
		{
			get { return Link.Name; }
		}
		#endregion

		public WordViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;

			if (WordHelper.Instance.Connect())
			{
				var g = Guid.NewGuid();
				var newFileName = Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, g + ".html");
				WordHelper.Instance.ConvertToHtml(Link.FullPath, newFileName);
				WordHelper.Instance.Disconnect();
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
			LinkManager.OpenCopyOfFile((WordLink)Link);
		}

		public void Save()
		{
			LinkManager.SaveLink("Save copy of the file as", (WordLink)Link);
		}

		public void Email()
		{
			using (var form = new FormEmailLink())
			{
				form.FileLink = (WordLink)Link;
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}

		public void Print()
		{
			LinkManager.PrintFile((WordLink)Link);
		}
		#endregion
	}
}