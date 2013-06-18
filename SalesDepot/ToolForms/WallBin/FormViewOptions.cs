using System;
using System.Drawing;
using System.Windows.Forms;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormViewOptions : Form
	{
		public enum ViewOptions
		{
			Open = 0,
			Save,
			Email,
			Print,
			QuickSiteEmail,
			QuickSiteAdd
		}

		private ViewOptions _selectedOption;

		public FormViewOptions()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXOpen.Font = new Font(buttonXOpen.Font.FontFamily, buttonXOpen.Font.Size - 3, buttonXOpen.Font.Style);
				buttonXSave.Font = new Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 3, buttonXSave.Font.Style);
				buttonXPrint.Font = new Font(buttonXPrint.Font.FontFamily, buttonXPrint.Font.Size - 3, buttonXPrint.Font.Style);
				buttonXEmail.Font = new Font(buttonXEmail.Font.FontFamily, buttonXEmail.Font.Size - 3, buttonXEmail.Font.Style);
				buttonXQuickSiteAdd.Font = new Font(buttonXQuickSiteAdd.Font.FontFamily, buttonXQuickSiteAdd.Font.Size - 3, buttonXQuickSiteAdd.Font.Style);
				buttonXQuickSiteEmail.Font = new Font(buttonXQuickSiteEmail.Font.FontFamily, buttonXQuickSiteEmail.Font.Size - 3, buttonXQuickSiteEmail.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
			}
		}

		public ViewOptions SelectedOption
		{
			get { return _selectedOption; }
		}

		private void ViewOptionsForm_Load(object sender, EventArgs e)
		{
			if ((SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayViewOptions) == EmailButtonsDisplayOptions.DisplayViewOptions)
				buttonXEmail.Enabled = true;
			else
				buttonXEmail.Enabled = false;
			buttonXQuickSiteAdd.Enabled = buttonXQuickSiteEmail.Enabled = SettingsManager.Instance.QBuilderSettings.AvailableHosts.Count > 0;
		}

		private void buttonXOpen_Click(object sender, EventArgs e)
		{
			_selectedOption = ViewOptions.Open;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXSave_Click(object sender, EventArgs e)
		{
			_selectedOption = ViewOptions.Save;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXPrint_Click(object sender, EventArgs e)
		{
			_selectedOption = ViewOptions.Print;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXEmail_Click(object sender, EventArgs e)
		{
			_selectedOption = ViewOptions.Email;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXQuickSiteEmail_Click(object sender, EventArgs e)
		{
			_selectedOption = ViewOptions.QuickSiteEmail;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXQuickSiteAdd_Click(object sender, EventArgs e)
		{
			_selectedOption = ViewOptions.QuickSiteAdd;
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