using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.BatchTagger.BusinessClasses.Dictionaries;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	public partial class FormLibraryCredentials : MetroForm
	{
		public FormLibraryCredentials()
		{
			InitializeComponent();

			textEditUser.EnableSelectAll();
			textEditPassword.EnableSelectAll();

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			}
		}

		public LibraryCredentials GetCredentials()
		{
			return new LibraryCredentials
			{
				User = textEditUser.EditValue as String,
				Password = textEditPassword.EditValue as String,
			};
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.IsNullOrEmpty(textEditUser.EditValue as String) ||
				String.IsNullOrEmpty(textEditPassword.EditValue as String))
			{
				AppManager.Instance.ShowWarning("Please set library credentials to continue");
				e.Cancel = true;
			}
		}
	}
}
