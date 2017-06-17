using System;
using System.Drawing;
using System.IO;
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

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}
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
