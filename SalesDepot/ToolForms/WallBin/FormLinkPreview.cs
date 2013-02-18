using System;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.PresentationClasses.Viewers;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormLinkPreview : Form
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
						if (SelectedFile.Type != FileTypes.MediaPlayerVideo)
							barManager.Items.Remove(barLargeButtonItemInsert);
						switch (SelectedFile.Type)
						{
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
									barManager.Items.Add(barLargeButtonItemInsert);
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
							if (_selectedFileViewer != null)
							{
								(_selectedFileViewer as Control).Visible = true;
								pnPreview.Controls.Add(_selectedFileViewer as Control);
							}
						});
					});
					thread.Start();

					form.Show();

					while (thread.IsAlive)
						Application.DoEvents();

					form.Close();
				}
				barLargeButtonItemEmail.Visibility = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayQuickView) == EmailButtonsDisplayOptions.DisplayQuickView && (SelectedFile.Type == FileTypes.Word || SelectedFile.Type == FileTypes.Excel || SelectedFile.Type == FileTypes.PDF) ? BarItemVisibility.Always : BarItemVisibility.Never;
				barLargeButtonItemPrint.Visibility = SelectedFile.Type == FileTypes.Word || SelectedFile.Type == FileTypes.Excel || SelectedFile.Type == FileTypes.PDF ? BarItemVisibility.Always : BarItemVisibility.Never;
				barLargeButtonItemSave.Visibility = SelectedFile.Type == FileTypes.Word || SelectedFile.Type == FileTypes.Excel || SelectedFile.Type == FileTypes.PDF ? BarItemVisibility.Always : BarItemVisibility.Never;
				barLargeButtonItemOpen.Visibility = SelectedFile.Type == FileTypes.Word || SelectedFile.Type == FileTypes.Excel || SelectedFile.Type == FileTypes.PDF ? BarItemVisibility.Always : BarItemVisibility.Never;
			}
			LinkManager.Instance.PreviousPreviewHandles.Add(RegistryHelper.SalesDepotHandle.ToInt32());
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = false;
		}

		private void FormLinkPreview_FormClosed(object sender, FormClosedEventArgs e)
		{
			pnPreview.Controls.Clear();
			_selectedFileViewer.ReleaseResources();
			_selectedFileViewer = null;
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Open();
		}

		private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Save();
		}

		private void barLargeButtonItemInsert_ItemClick(object sender, ItemClickEventArgs e)
		{
			var viewer = _selectedFileViewer as VideoViewer;
			if (viewer != null)
				viewer.InsertIntoPresentation();
		}

		private void barButtonItemEmailLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Email();
		}

		private void barButtonItemPrintLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Print();
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("qv");
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			_selectedFileViewer.ReleaseResources();
			Close();
		}
		#endregion

		public LibraryLink SelectedFile { get; set; }
	}
}