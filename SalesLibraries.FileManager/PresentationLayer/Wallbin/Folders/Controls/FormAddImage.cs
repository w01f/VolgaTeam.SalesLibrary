using System;
using System.Drawing;
using System.IO;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class FormAddImage : MetroForm
	{
		private readonly Image _originalImage;

		public FormAddImage(string imagePath)
		{
			InitializeComponent();

			var tempFile = Path.GetTempFileName();
			File.Copy(imagePath, tempFile, true);
			_originalImage = Image.FromFile(tempFile);

			pictureEditImage.Image = _originalImage;

			layoutControlItemAdd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAdd.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnImageSizeCheckedChanged(object sender, System.EventArgs e)
		{
			if (checkEditExtraLarge.Checked)
				pictureEditImage.Image = _originalImage;
			else if (checkEditLarge.Checked)
				pictureEditImage.Image = _originalImage.Resize(new Size((Int32)(_originalImage.Width * 0.7), _originalImage.Height));
			else if (checkEditMedium.Checked)
				pictureEditImage.Image = _originalImage.Resize(new Size((Int32)(_originalImage.Width * 0.5), _originalImage.Height));
			else if (checkEditSmall.Checked)
				pictureEditImage.Image = _originalImage.Resize(new Size((Int32)(_originalImage.Width * 0.3), _originalImage.Height));
			else if (checkEditExtraSmall.Checked)
				pictureEditImage.Image = _originalImage.Resize(new Size((Int32)(_originalImage.Width * 0.15), _originalImage.Height));
		}
	}
}
