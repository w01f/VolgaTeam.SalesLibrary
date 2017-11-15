using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormEmailLink : MetroForm
	{
		public FormEmailLink()
		{
			InitializeComponent();
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public LibraryFileLink FileLink { get; set; }

		private void ckChangeEmailName_CheckedChanged(object sender, EventArgs e)
		{
			textEditEmailName.Enabled = checkEditChangeEmailName.Checked;
		}

		private void FormEmailPresentation_Load(object sender, EventArgs e)
		{
			Text = string.Format(Text, FileLink.NameWithExtension);
		}

		private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			var selectedName = checkEditChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : FileLink.NameWithExtension;
			var destinationFilePath = Path.Combine(Path.GetTempPath(), (selectedName + FileLink.Extension));
			File.Copy(FileLink.FullPath, destinationFilePath, true);
			if (File.Exists(destinationFilePath))
				LinkManager.EmailFile(destinationFilePath);
		}
	}
}