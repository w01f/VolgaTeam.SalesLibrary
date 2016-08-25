using System;
using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkText : MetroForm
	{
		public string EditedText
		{
			get { return memoEdit.EditValue as String; }
			set { memoEdit.EditValue = value; }
		}

		public FormEditLinkText()
		{
			InitializeComponent();
		}
	}
}
