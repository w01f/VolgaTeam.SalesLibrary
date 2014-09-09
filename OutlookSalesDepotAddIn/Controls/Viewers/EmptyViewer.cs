using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class EmptyViewer : UserControl, IFileViewer
	{
		#region Properties
		public LibraryLink File { get; private set; }

		public string DisplayName
		{
			get { return string.Empty; }
		}

		public string CriteriaOverlap
		{
			get { return string.Empty; }
		}

		public Image Widget
		{
			get { return null; }
		}
		#endregion

		public EmptyViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
		}

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources() {}
		#endregion
	}
}