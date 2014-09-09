using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class VideoViewer : UserControl, IFileViewer
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

		public VideoViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
			axWindowsMediaPlayer.URL = File.LocalPath;
		}

		#region VideoViewer Methods
		public void Play()
		{
			axWindowsMediaPlayer.Ctlcontrols.play();
		}

		public void Pause()
		{
			axWindowsMediaPlayer.Ctlcontrols.pause();
		}

		public void Stop()
		{
			axWindowsMediaPlayer.Ctlcontrols.stop();
		}
		#endregion

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources()
		{
			axWindowsMediaPlayer.Ctlcontrols.stop();
			axWindowsMediaPlayer.close();
		}
		#endregion
	}
}