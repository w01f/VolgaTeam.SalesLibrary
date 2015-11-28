using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormViewOptions : MetroForm
	{
		public ViewOptions SelectedOption { get; private set; }

		public FormViewOptions()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXOpen.Font = new Font(buttonXOpen.Font.FontFamily, buttonXOpen.Font.Size - 3, buttonXOpen.Font.Style);
				buttonXSave.Font = new Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 3, buttonXSave.Font.Style);
				buttonXPrint.Font = new Font(buttonXPrint.Font.FontFamily, buttonXPrint.Font.Size - 3, buttonXPrint.Font.Style);
				buttonXEmail.Font = new Font(buttonXEmail.Font.FontFamily, buttonXEmail.Font.Size - 3, buttonXEmail.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
			}
		}

		private void ViewOptionsForm_Load(object sender, EventArgs e)
		{
			if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayViewOptions) == EmailButtonsDisplayOptionsEnum.DisplayViewOptions)
				buttonXEmail.Enabled = true;
			else
				buttonXEmail.Enabled = false;
		}

		private void buttonXOpen_Click(object sender, EventArgs e)
		{
			SelectedOption = ViewOptions.Open;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXSave_Click(object sender, EventArgs e)
		{
			SelectedOption = ViewOptions.Save;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXPrint_Click(object sender, EventArgs e)
		{
			SelectedOption = ViewOptions.Print;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXEmail_Click(object sender, EventArgs e)
		{
			SelectedOption = ViewOptions.Email;
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