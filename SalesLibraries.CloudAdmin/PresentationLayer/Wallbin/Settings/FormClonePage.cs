using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	public partial class FormClonePage : MetroForm
	{
		private readonly string _originalPageName;

		public string NewPageName
		{
			get { return textEditPageName.EditValue as String; }
			set { textEditPageName.EditValue = value; }
		}

		public FormClonePage(string originalPageName)
		{
			_originalPageName = originalPageName;

			InitializeComponent();

			labelControlTitle1.Text = String.Format(labelControlTitle1.Text, _originalPageName);

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
			buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
		}

		private void FormClonePage_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.Equals(NewPageName, _originalPageName, StringComparison.OrdinalIgnoreCase))
			{
				MainController.Instance.PopupMessages.ShowWarning(String.Format("You can’t have Pages with the same Name.{0}Try again...", Environment.NewLine));
				e.Cancel = true;
			}
		}
	}
}