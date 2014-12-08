using System;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using OutlookSalesDepotAddIn.BusinessClasses;
using OutlookSalesDepotAddIn.Controls.Viewers;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.Forms
{
	public partial class FormLinkPreview : MetroForm
	{
		private IFileViewer _selectedFileViewer;

		public FormLinkPreview()
		{
			InitializeComponent();
		}
		#region Form GUI Event Habdlers
		private void FormQuickView_Shown(object sender, EventArgs e)
		{
			if (SelectedFile != null)
			{
				Text = "Preview - " + SelectedFile.NameWithExtension;

				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading preview...";
					var thread = new Thread(delegate()
					{
						_selectedFileViewer = null;
						switch (SelectedFile.Type)
						{
							case FileTypes.Presentation:
								Invoke((MethodInvoker)delegate
								{
									try
									{
										_selectedFileViewer = new PowerPointViewer(SelectedFile);
									}
									catch
									{
										_selectedFileViewer = new DefaultViewer(SelectedFile);
									}
								});
								break;
							case FileTypes.Excel:
								Invoke((MethodInvoker)delegate
								{
									try
									{
										_selectedFileViewer = new ExcelViewer(SelectedFile);
									}
									catch
									{
										_selectedFileViewer = new DefaultViewer(SelectedFile);
									}
								});
								break;
							case FileTypes.Word:
								Invoke((MethodInvoker)delegate
								{
									try
									{
										_selectedFileViewer = new WordViewer(SelectedFile);
									}
									catch
									{
										_selectedFileViewer = new DefaultViewer(SelectedFile);
									}
								});
								break;
							case FileTypes.PDF:
								Invoke((MethodInvoker)delegate
								{
									try
									{
										_selectedFileViewer = new PDFViewer(SelectedFile);
									}
									catch
									{
										_selectedFileViewer = new DefaultViewer(SelectedFile);
									}
								});
								break;
							case FileTypes.MediaPlayerVideo:
								Invoke((MethodInvoker)delegate
								{
									try
									{
										_selectedFileViewer = new VideoViewer(SelectedFile);
									}
									catch
									{
										_selectedFileViewer = new DefaultViewer(SelectedFile);
									}
								});
								break;
							case FileTypes.Folder:
								Invoke((MethodInvoker)delegate
								{
									try
									{
										_selectedFileViewer = new FolderViewer(SelectedFile);
									}
									catch
									{
										_selectedFileViewer = new DefaultViewer(SelectedFile);
									}
								});
								break;
							default:
								Invoke((MethodInvoker)delegate { _selectedFileViewer = new DefaultViewer(SelectedFile); });
								break;
						}
						Invoke((MethodInvoker)delegate
						{
							if (_selectedFileViewer == null) return;
							(_selectedFileViewer as Control).Visible = true;
							pnPreview.Controls.Add(_selectedFileViewer as Control);
						});
					});
					thread.Start();

					form.Show();

					while (thread.IsAlive)
						Application.DoEvents();

					form.Close();
				}
			}
		}

		private void FormLinkPreview_FormClosed(object sender, FormClosedEventArgs e)
		{
			pnPreview.Controls.Clear();
			_selectedFileViewer.ReleaseResources();
			_selectedFileViewer = null;
		}
		#endregion

		#region Button Clicks
		private void barLargeButtonItemAttach_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer == null) return;
			_selectedFileViewer.Attach();
			Close();
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			Close();
		}
		#endregion

		public LibraryLink SelectedFile { get; set; }
	}
}