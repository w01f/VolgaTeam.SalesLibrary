using System;
using System.Drawing;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormPageSettings : MetroForm
	{
		public string PageName
		{
			get { return textEditPageName.EditValue as String; }
			set { textEditPageName.EditValue = value; }
		}

		public string Icon
		{
			get { return textEditIcon.EditValue as String; }
			set { textEditIcon.EditValue = value; }
		}

		public Color IconColor
		{
			get { return colorEditIconColor.Color; }
			set { colorEditIconColor.Color = value; }
		}

		public FormPageSettings()
		{
			InitializeComponent();
			textEditPageName.EnableSelectAll();
			textEditPageName.Focus();

			layoutControlItemApply.MinSize = RectangleHelper.ScaleSize(layoutControlItemApply.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemApply.MaxSize = RectangleHelper.ScaleSize(layoutControlItemApply.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}