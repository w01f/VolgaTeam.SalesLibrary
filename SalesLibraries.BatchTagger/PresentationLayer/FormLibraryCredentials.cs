using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.BatchTagger.BusinessClasses.Dictionaries;
using SalesLibraries.Common.Helpers;
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

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
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
