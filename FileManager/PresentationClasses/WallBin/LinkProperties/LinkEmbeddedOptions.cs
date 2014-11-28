using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class LinkEmbeddedOptions : UserControl, ILinkProperties
	public partial class LinkEmbeddedOptions : XtraTabPage, ILinkProperties
	{
		protected readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LinkEmbeddedOptions()
		{
			InitializeComponent();
		}

		public LinkEmbeddedOptions(LibraryLink data)
		{
			InitializeComponent();
			_data = data;
			LoadData();
		}

		private void LoadData()
		{
			pnAdminTools.Visible = (_data.PreviewContainer != null && Directory.Exists(_data.PreviewContainer.ContainerPath)) ||
				(_data.UniversalPreviewContainer != null && Directory.Exists(_data.UniversalPreviewContainer.ContainerPath));
			buttonXOpenQV.Enabled = _data.PreviewContainer != null && Directory.Exists(_data.PreviewContainer.ContainerPath);
			buttonXOpenWV.Enabled = _data.UniversalPreviewContainer != null &&
				Directory.Exists(_data.UniversalPreviewContainer.ContainerPath);
		}

		public virtual void SaveData() { }

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure want to delete preview files for the link?") != DialogResult.Yes) return;
			if (_data.PreviewContainer != null)
				_data.PreviewContainer.ClearContent();
			if (_data.UniversalPreviewContainer != null)
				_data.UniversalPreviewContainer.ClearContent();
			AppManager.Instance.ShowInfo("Preview files for the link was deleted and will re-create during next Sync.");
		}

		private void buttonXOpenQV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.PreviewContainer.ContainerPath);
			}
			catch { }
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.UniversalPreviewContainer.ContainerPath);
			}
			catch { }
		}
	}
}
