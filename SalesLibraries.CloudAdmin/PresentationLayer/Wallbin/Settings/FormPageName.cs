using System;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	public partial class FormPageName : DevComponents.DotNetBar.Metro.MetroForm
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
		}
	}
}