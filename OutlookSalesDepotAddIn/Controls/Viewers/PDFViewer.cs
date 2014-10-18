using System.ComponentModel;
using System.Diagnostics;
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
			pdfViewerControl.LoadDocument(tempName);
		}

		#region IFileViewer Methods
		public void Attach()
		{
			LinkManager.Instance.AttachFile(File);
		}

		public void ReleaseResources()
		{
			pdfViewerControl.CloseDocument();
		}
		#endregion

		private void pdfViewerControl_MouseMove(object sender, MouseEventArgs e)
		{
			pdfViewerControl.Focus();
		}

		private void pdfViewerControl_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
				Process.Start(pdfViewerControl.DocumentFilePath);
			}
			catch { }
		}
	}
}