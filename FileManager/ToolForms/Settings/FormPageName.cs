using System;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormPageName : DevComponents.DotNetBar.Metro.MetroForm
	{
		private readonly LibraryPage _page;
		public FormPageName(LibraryPage page)
		{
			InitializeComponent();
			_page = page;
			textEditPageName.EditValue = _page.Name;
			textEditPageName.MouseDown += FormMain.Instance.EditorMouseDown;
			textEditPageName.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditPageName.Enter += FormMain.Instance.EditorEnter;
			textEditPageName.Focus();
		}

		private void FormPageName_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			_page.Name = textEditPageName.EditValue as String;
		}
	}
}