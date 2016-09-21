using System;
using System.Drawing;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	public partial class FormPageName : MetroForm
	{
		public string PageName
		{
			get { return textEditPageName.EditValue as String; }
			set { textEditPageName.EditValue = value; }
		}

		public FormPageName()
		{
			InitializeComponent();
			textEditPageName.MouseDown += EditorHelper.EditorMouseDown;
			textEditPageName.MouseUp += EditorHelper.EditorMouseUp;
			textEditPageName.Enter += EditorHelper.EditorEnter;
			textEditPageName.Focus();

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