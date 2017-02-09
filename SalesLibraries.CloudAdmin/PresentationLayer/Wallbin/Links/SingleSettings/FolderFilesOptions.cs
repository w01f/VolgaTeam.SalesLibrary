using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryFolderLink))]
	//public partial class FolderFilesOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class FolderFilesOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LibraryFolderLink _data;
		private readonly List<LibraryFileLink> _files = new List<LibraryFileLink>();

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.AdvancedSettings };
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public FolderFilesOptions(LibraryFolderLink data)
		{
			InitializeComponent();
			Text = "Individual File Settings";
			_data = data;
		}

		public void LoadData()
		{
			_files.AddRange(_data.AllLinks.Where(f => !(f is LibraryFolderLink)));
			_files.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.NameWithExtension, y.NameWithExtension));
			gridControl.DataSource = _files;
		}

		public void SaveData() { }

		private void ProcessLinkOptions(LinkSettingsType settingsType)
		{
			var selectedFiles = advBandedGridView
				.GetSelectedRows()
				.Select(i => advBandedGridView.GetRow(i))
				.OfType<LibraryFileLink>()
				.ToList();
			var targetFile = selectedFiles.Any() ?
				selectedFiles.FirstOrDefault() :
				advBandedGridView.GetFocusedRow() as LibraryFileLink;
			if (targetFile == null) return;
			SettingsEditorFactory.RunEmbedded(targetFile, _data, settingsType);
		}

		private void SelectLinks(IEnumerable<LibraryFileLink> selectedLinks)
		{
			advBandedGridView.ClearSelection();
			var i = 0;
			foreach (var file in _files)
			{
				if (selectedLinks.Any(selectedFile => selectedFile == file))
					advBandedGridView.SelectRow(i);
				i++;
			}
		}

		private void repositoryItemButtonEditOperations_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					ProcessLinkOptions(LinkSettingsType.Notes);
					break;
				case 1:
					ProcessLinkOptions(LinkSettingsType.Tags);
					break;
			}
		}

		private void barLargeButtonItemTags_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ProcessLinkOptions(LinkSettingsType.Tags);
		}

		private void barLargeButtonItemSyncSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ProcessLinkOptions(LinkSettingsType.Notes);
		}

		private void barLargeButtonItemSelectPowerPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinks(_files.OfType<PowerPointLink>());
		}

		private void barLargeButtonItemSelectPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinks(_files.OfType<PdfLink>());
		}

		private void barLargeButtonItemSelectWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinks(_files.OfType<WordLink>());
		}

		private void barLargeButtonItemSelectExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinks(_files.OfType<ExcelLink>());
		}

		private void barLargeButtonItemSelectVideo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinks(_files.OfType<VideoLink>());
		}

		private void barLargeButtonItemSelectOther_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinks(_files.OfType<CommonFileLink>());
		}
	}
}
