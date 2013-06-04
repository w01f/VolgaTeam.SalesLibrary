using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;

namespace SalesDepot.PresentationClasses.Viewers
{
	[ToolboxItem(false)]
	public partial class WebViewer : UserControl, IFileViewer
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

		public WebViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
			webBrowser.Navigate(File.LocalPath);
		}

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			webBrowser.Navigate("about:blank");
		}

		public void Open()
		{
			LinkManager.Instance.StartProcess(File);
		}

		public void Save() { }

		public void Email() { }

		public void Print() { }

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