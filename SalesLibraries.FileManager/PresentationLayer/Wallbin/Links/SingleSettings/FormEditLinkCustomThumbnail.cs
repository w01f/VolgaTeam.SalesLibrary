using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkCustomThumbnail : MetroForm
	{
		public FormEditLinkCustomThumbnail()
		{
			InitializeComponent();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void buttonXLoad_Click(object sender, EventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					var tempFile = Path.GetTempFileName();
					File.Copy(openFileDialog.FileName, tempFile, true);
					pictureEdit.Image = Image.FromFile(tempFile);
				}
			}
		}

		private void buttonXClear_Click(object sender, EventArgs e)
		{
			pictureEdit.Image = null;
		}

		private void pictureEdit_DragDrop(object sender, DragEventArgs e)
		{
			var imageEditor = (PictureEdit)sender;
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) &&
				e.Data.GetData(DataFormats.FileDrop, true) is String[])
			{
				var imageFilePath = (e.Data.GetData(DataFormats.FileDrop) as String[] ?? new string[] { }).FirstOrDefault();
				if (imageFilePath == null) return;
				var tempFile = Path.GetTempFileName();
				File.Copy(imageFilePath, tempFile, true);
				imageEditor.Image = Image.FromFile(tempFile);
			}
		}

		private void pictureEdit_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop, true) && e.Data.GetData(DataFormats.FileDrop, true) is String[])
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}
	}
}
