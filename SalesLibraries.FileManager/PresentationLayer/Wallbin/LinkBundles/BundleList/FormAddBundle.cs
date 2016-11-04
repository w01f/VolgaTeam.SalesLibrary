using System;
using System.Drawing;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.BundleList
{
	public partial class FormAddBundle : MetroForm
	{
		public string BundleName
		{
			get { return textEditName.EditValue as String; }
			set { textEditName.EditValue = value; }
		}

		public FormAddBundle()
		{
			InitializeComponent();
			textEditName.MouseDown += EditorHelper.EditorMouseDown;
			textEditName.MouseUp += EditorHelper.EditorMouseUp;
			textEditName.Enter += EditorHelper.EditorEnter;
			textEditName.Focus();

			if (!((CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			buttonXApply.Font = new Font(buttonXApply.Font.FontFamily, buttonXApply.Font.Size - 2, buttonXApply.Font.Style);
			buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
		}
	}
}