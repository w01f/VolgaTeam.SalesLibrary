using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	public partial class FormAddUrl : MetroForm
	{
		public string Url
		{
			get { return textEditUrl.EditValue as String; }
			set { textEditUrl.EditValue = value; }
		}

		public FormAddUrl()
		{
			InitializeComponent();

			textEditUrl.EnableSelectAll();
			textEditUrl.Focus();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.IsNullOrEmpty(Url))
				MainController.Instance.PopupMessages.ShowWarning("You need to set url");
		}
	}
}