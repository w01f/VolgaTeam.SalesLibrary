using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(WebLink))]
	[ToolboxItem(false)]
	public partial class WebViewer : UserControl, ILinkViewer
	{
		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName
		{
			get { return Link.Name; }
		}
		#endregion

		public WebViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;
			webBrowser.Navigate(((WebLink)Link).Url);
		}

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			webBrowser.Navigate("about:blank");
		}

		public void Open()
		{
			MainController.Instance.ActivityManager.AddLinkAccessActivity("Open Link", Link);
			Utils.OpenFile(((WebLink)Link).Url);
		}

		public void Save() { }

		public void Email() { }

		public void Print() { }
		#endregion
	}
}