using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(PdfLink))]
	[ToolboxItem(false)]
	public partial class PDFViewer : UserControl, ILinkViewer
	{
		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName
		{
			get { return Link.Name; }
		}
		#endregion

		public PDFViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;

			var tempName = Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Link.FullPath));
			System.IO.File.Copy(Link.FullPath, tempName, true);
			pdfViewerControl.LoadDocument(tempName);
		}

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			pdfViewerControl.CloseDocument();
		}

		public void Open()
		{
			LinkManager.OpenCopyOfFile((PdfLink)Link);
		}

		public void Save()
		{
			LinkManager.SaveLink("Save copy of the file as", (PdfLink)Link);
		}

		public void Email()
		{
			using (var form = new FormEmailLink())
			{
				form.FileLink = (PdfLink)Link;
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}

		public void Print()
		{
			LinkManager.PrintFile((PdfLink)Link);
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