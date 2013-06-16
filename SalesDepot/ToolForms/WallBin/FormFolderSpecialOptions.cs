using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormFolderSpecialOptions : Form
	{
		private FormViewOptions.ViewOptions _selectedOption;

		public FormFolderSpecialOptions()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXQuickSiteAdd.Font = new Font(buttonXQuickSiteAdd.Font.FontFamily, buttonXQuickSiteAdd.Font.Size - 3, buttonXQuickSiteAdd.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
			}
		}

		public FormViewOptions.ViewOptions SelectedOption
		{
			get { return _selectedOption; }
		}

		private void buttonXQuickSiteAdd_Click(object sender, EventArgs e)
		{
			_selectedOption = FormViewOptions.ViewOptions.QuickSiteAdd;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}