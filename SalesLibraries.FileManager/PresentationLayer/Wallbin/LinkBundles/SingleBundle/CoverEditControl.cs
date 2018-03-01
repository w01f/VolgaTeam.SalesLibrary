using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	[IntendForClass(typeof(CoverItem))]
	//public partial class CoverEditControl : UserControl, ILinkBundleInfoEditControl
	public partial class CoverEditControl : XtraTabPage, ILinkBundleInfoEditControl
	{
		private CoverItem _bundleItem;

		public CoverEditControl(CoverItem bundleItem)
		{
			_bundleItem = bundleItem;
			InitializeComponent();

			Text = CoverItem.ItemName;

			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void LoadData()
		{
			pictureEditImage.Image = _bundleItem.Logo;
		}

		public void SaveData()
		{
			_bundleItem.Logo = pictureEditImage.Image;
		}

		public void Release()
		{
			_bundleItem = null;
		}

		private void OnImageEditClick(object sender, EventArgs e)
		{
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			if (openFileDialog.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
			{
				var tempFile = Path.GetTempFileName();
				File.Copy(openFileDialog.FileName, tempFile, true);
				pictureEditImage.Image = Image.FromFile(tempFile);
			}
		}

		private void OnImageDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) &&
				e.Data.GetData(DataFormats.FileDrop, true) is String[])
			{
				var imageFilePath = (e.Data.GetData(DataFormats.FileDrop) as String[] ?? new string[] { }).FirstOrDefault();
				if (imageFilePath == null) return;
				var tempFile = Path.GetTempFileName();
				File.Copy(imageFilePath, tempFile, true);
				pictureEditImage.Image = Image.FromFile(tempFile);
			}
		}

		private void OnImageDragOver(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) && e.Data.GetData(DataFormats.FileDrop, true) is String[])
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}
	}
}
