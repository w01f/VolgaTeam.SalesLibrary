using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Business.Services;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Settings;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormLinkPreview : MetroForm
	{
		private ILinkViewer _linkViewer;

		public LibraryObjectLink Link { get; set; }

		public FormLinkPreview()
		{
			InitializeComponent();
		}

		#region Form GUI Event Habdlers
		private void FormQuickView_Shown(object sender, EventArgs e)
		{
			if (Link != null)
			{
				Text = String.Format("Preview - {0}", Link.Name);

				MainController.Instance.ProcessManager.Run("Loading preview...", (cancelletionToken, formProgress) =>
					Invoke((MethodInvoker)delegate
					{
						try
						{
							_linkViewer = LinkViewerFactory.Create(Link);
						}
						catch
						{
							_linkViewer = new CommonFileViewer(Link);
						}
					}));

				if (_linkViewer != null)
				{
					((Control)_linkViewer).Visible = true;
					pnPreview.Controls.Add(_linkViewer as Control);
				}

				barLargeButtonItemInsert.Visibility = Link is VideoLink ? BarItemVisibility.Always : BarItemVisibility.Never;
				barLargeButtonItemEmail.Visibility =
					(MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayQuickView) == EmailButtonsDisplayOptionsEnum.DisplayQuickView && Link is LibraryFileLink ?
					BarItemVisibility.Always :
					BarItemVisibility.Never;
				barLargeButtonItemPrint.Visibility = Link is ExcelLink || Link is DocumentLink ? BarItemVisibility.Always : BarItemVisibility.Never;
				barLargeButtonItemSave.Visibility = Link is LibraryFileLink ? BarItemVisibility.Always : BarItemVisibility.Never;
			}
			LinkManager.PreviousPreviewHandles.Add(RegistryHelper.SalesDepotHandle.ToInt32());
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = false;
		}

		private void FormLinkPreview_FormClosed(object sender, FormClosedEventArgs e)
		{
			pnPreview.Controls.Clear();
			_linkViewer.ReleaseResources();
			_linkViewer = null;
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			_linkViewer.Open();
		}

		private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			_linkViewer.Save();
		}

		private void barLargeButtonItemInsert_ItemClick(object sender, ItemClickEventArgs e)
		{
			var viewer = _linkViewer as VideoViewer;
			if (viewer != null)
				viewer.InsertIntoPresentation();
		}

		private void barButtonItemEmailLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			_linkViewer.Email();
		}

		private void barButtonItemPrintLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			_linkViewer.Print();
		}

		private void barLargeButtonItemSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormFileSettings())
			{
				form.ShowDialog(this);
			}
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			MainController.Instance.HelpManager.OpenHelpLink("qv");
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			_linkViewer.ReleaseResources();
			Close();
		}
		#endregion
	}
}