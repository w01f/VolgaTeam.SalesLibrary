using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
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

			labelControlTitle.Text = String.Format(labelControlTitle.Text, _originalPageName);

			textEditPageName.EnableSelectAll();
			textEditPageName.Focus();

			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
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