using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(WebLink))]
	[IntendForClass(typeof(YouTubeLink))]
	[IntendForClass(typeof(Html5Link))]
	[IntendForClass(typeof(QuickSiteLink))]
	[IntendForClass(typeof(VimeoLink))]
	[ToolboxItem(false)]
	public partial class WebViewer : UserControl, ILinkViewer
	{
		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName => Link.Name;

		#endregion

		public WebViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;
			webBrowser.Navigate(((HyperLink)Link).Url);
		}

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			webBrowser.Navigate("about:blank");
		}

		public void Open()
		{
			Utils.OpenFile(((HyperLink)Link).Url);
		}

		public void Save() { }

		public void Email() { }

		public void Print() { }
		#endregion
	}
}