using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class QuickTimeViewer : UserControl, IFileViewer
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

		public QuickTimeViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
		}

		#region VideoViewer Methods
		public void Play()
		{
		}

		public void Pause()
		{
		}

		public void Stop()
		{
		}

		public void InsertIntoPresentation() { }
		#endregion

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources() { }
		#endregion
	}
}