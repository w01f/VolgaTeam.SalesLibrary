using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormFolderViewOptions : Form
	{
		private ViewOptions _selectedOption;

		public FormFolderViewOptions()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXOpen.Font = new Font(buttonXOpen.Font.FontFamily, buttonXOpen.Font.Size - 3, buttonXOpen.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
			}
		}

		public ViewOptions SelectedOption
		{
			get { return _selectedOption; }
		}

		private void buttonXOpen_Click(object sender, EventArgs e)
		{
			_selectedOption = ViewOptions.Open;
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