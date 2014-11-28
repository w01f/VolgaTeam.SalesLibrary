using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using FileManager.ToolForms.WallBin;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class FolderFilesOptions : UserControl, ILinkProperties
	public partial class FolderFilesOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryFolderLink _data;
		private readonly Dictionary<LibraryLink, LibraryLink> _tempFilesCopy = new Dictionary<LibraryLink, LibraryLink>();

		public event EventHandler OnForseClose;

		public FolderFilesOptions(LibraryFolderLink data)
		{
			InitializeComponent();
			Text = "Individual File Settings";
			_data = data;
			LoadData();
		}

		private void LoadData()
		{
			var files = _data.AllFiles.Where(f => !(f is LibraryFolderLink)).ToList();
			files.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.NameWithExtension, y.NameWithExtension));
			foreach (var file in files.OfType<LibraryLink>())
				_tempFilesCopy.Add(file, (LibraryLink)file.Clone(file.Parent));
			gridControl.DataSource = _tempFilesCopy.Values;
		}

		public void SaveData()
		{
			foreach (var keyValuePair in _tempFilesCopy)
			{
				var originalFile = keyValuePair.Key;
				var fileCopy = keyValuePair.Value;

				originalFile.ExtendedProperties = fileCopy.ExtendedProperties.Clone(originalFile);
				originalFile.SearchTags = fileCopy.SearchTags.Clone();
				originalFile.CustomKeywords = fileCopy.CustomKeywords.Clone();
			}
		}

		private void ProcessLinkOptions(LinkPropertiesType propertiesType)
		{
			var selectedFiles = advBandedGridView
				.GetSelectedRows()
				.Select(i => advBandedGridView.GetRow(i))
				.OfType<LibraryLink>()
				.ToList();
			var targetFile = selectedFiles.Any() ?
				selectedFiles.FirstOrDefault() :
				advBandedGridView.GetFocusedRow() as LibraryLink;
			if (targetFile == null) return;
			if (FormLinkProperties.ShowProperties(targetFile, propertiesType, true) != DialogResult.OK) return;
			foreach (var file in selectedFiles.Where(file => file != targetFile))
			{
				file.ExtendedProperties = targetFile.ExtendedProperties.Clone(file);
				file.SearchTags = targetFile.SearchTags.Clone();
				file.CustomKeywords = targetFile.CustomKeywords.Clone();
				file.LastChanged = targetFile.LastChanged;
			}
			targetFile.Parent.LastChanged = targetFile.LastChanged;
		}

		private void SelectLinksIncludeFormat(IEnumerable<string> formatList)
		{
			advBandedGridView.ClearSelection();
			var i = 0;
			foreach (var file in _tempFilesCopy.Values)
			{
				if (formatList.Contains(file.Format))
					advBandedGridView.SelectRow(i);
				i++;
			}
		}

		private void SelectLinksExcludeFormat(IEnumerable<string> formatList)
		{
			advBandedGridView.ClearSelection();
			var i = 0;
			foreach (var file in _tempFilesCopy.Values)
			{
				if (!formatList.Contains(file.Format))
					advBandedGridView.SelectRow(i);
				i++;
			}
		}

		private void repositoryItemButtonEditOperations_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					ProcessLinkOptions(LinkPropertiesType.Notes);
					break;
				case 1:
					ProcessLinkOptions(LinkPropertiesType.Tags);
					break;
			}
		}

		private void barLargeButtonItemTags_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ProcessLinkOptions(LinkPropertiesType.Tags);
		}

		private void barLargeButtonItemSyncSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ProcessLinkOptions(LinkPropertiesType.Notes);
		}

		private void barLargeButtonItemSelectPowerPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinksIncludeFormat(new[] { "ppt" });
		}

		private void barLargeButtonItemSelectPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinksIncludeFormat(new[] { "pdf" });
		}

		private void barLargeButtonItemSelectWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinksIncludeFormat(new[] { "doc" });
		}

		private void barLargeButtonItemSelectExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinksIncludeFormat(new[] { "xls" });
		}

		private void barLargeButtonItemSelectVideo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinksIncludeFormat(new[] { "wmv", "mp4", "video" });
		}

		private void barLargeButtonItemSelectOther_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SelectLinksExcludeFormat(new[] { "ppt", "pdf", "doc", "xls", "wmv", "mp4", "video" });
		}
	}
}
