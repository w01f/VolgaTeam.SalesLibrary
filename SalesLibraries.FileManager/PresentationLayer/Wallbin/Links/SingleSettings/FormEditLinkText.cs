using System;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkText : MetroForm
	{
		public string EditedText
		{
			get { return memoEdit.EditValue as String; }
			set { memoEdit.EditValue = value; }
		}

		public bool TextWordWrap
		{
			get { return checkEditTextWordWrap.Checked; }
			set { checkEditTextWordWrap.Checked = value; }
		}

		public FormEditLinkText()
		{
			InitializeComponent();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
