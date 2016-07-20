using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(VideoLink))]
	[ToolboxItem(false)]
	public partial class VideoViewer : UserControl, ILinkViewer
	{
		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName => Link.Name;

		#endregion

		public VideoViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;
			axWindowsMediaPlayer1.URL = Link.FullPath;
		}

		#region VideoViewer Methods
		public void Play()
		{
			axWindowsMediaPlayer1.Ctlcontrols.play();
		}

		public void Pause()
		{
			axWindowsMediaPlayer1.Ctlcontrols.pause();
		}

		public void Stop()
		{
			axWindowsMediaPlayer1.Ctlcontrols.stop();
		}

		public void InsertIntoPresentation()
		{
			LinkManager.AddVideoIntoPresentation((VideoLink)Link);
		}
		#endregion

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			axWindowsMediaPlayer1.Ctlcontrols.stop();
			axWindowsMediaPlayer1.close();
		}

		public void Open()
		{
			LinkManager.OpenVideo((VideoLink)Link);
		}

		public void Save()
		{
			LinkManager.SaveLink("Save copy of the file as", (VideoLink)Link);
		}

		public void Email()
		{
			LinkManager.EmailLink((VideoLink)Link);
		}

		public void Print()
		{
			LinkManager.PrintFile((VideoLink)Link);
		}
		#endregion
	}
}