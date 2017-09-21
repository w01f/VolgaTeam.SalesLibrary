using System;
using System.Drawing;
using System.Windows.Forms;
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
				pictureEditImage.Image = Image.FromFile(openFileDialog.FileName);
			}
		}
	}
}
