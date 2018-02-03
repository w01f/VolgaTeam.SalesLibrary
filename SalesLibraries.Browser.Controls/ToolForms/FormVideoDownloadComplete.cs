using System;
using System.Diagnostics;
using System.IO;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Browser.Controls.ToolForms
{
	public partial class FormVideoDownloadComplete : MetroForm
	{
		private readonly string _filePath;

		public FormVideoDownloadComplete(string filePath)
		{
			InitializeComponent();
			_filePath = filePath;
			simpleLabelItemTitle.Text = String.Format(simpleLabelItemTitle.Text, Path.GetFileName(_filePath));

			var scaleFactor = Utils.GetScaleFactor(CreateGraphics().DpiX);
			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, scaleFactor);
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, scaleFactor);
			layoutControlItemOpenFile.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenFile.MaxSize, scaleFactor);
			layoutControlItemOpenFile.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenFile.MinSize, scaleFactor);
			layoutControlItemOpenFolder.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenFolder.MaxSize, scaleFactor);
			layoutControlItemOpenFolder.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenFolder.MinSize, scaleFactor);
			layoutControlItemAddSlide.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAddSlide.MaxSize, scaleFactor);
			layoutControlItemAddSlide.MinSize = RectangleHelper.ScaleSize(layoutControlItemAddSlide.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		private void buttonXOpenFile_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_filePath);
			}
			catch{}
		}

		private void buttonXOpenFolder_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(Path.GetDirectoryName(_filePath));
			}
			catch { }
		}
	}
}
