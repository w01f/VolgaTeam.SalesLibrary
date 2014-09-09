using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	[ToolboxItem(false)]
	public partial class PDFViewer : UserControl, IFileViewer
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

		public PDFViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;

			string tempName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(File.LocalPath));
			System.IO.File.Copy(File.LocalPath, tempName, true);
			axAcroPDF.LoadFile(tempName);
			axAcroPDF.setView("Fit");
		}

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources()
		{
			axAcroPDF.Dispose();
		}
		#endregion
	}
}