using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormLinkSpecialOptions : MetroForm
	{
		public ViewOptions SelectedOption { get; private set; }

		public FormLinkSpecialOptions()
		{
			InitializeComponent();
		}

		private void FormLinkSpecialOptions_Load(object sender, EventArgs e)
		{
			buttonXEmailBin.Enabled &= (MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayEmailBin) == EmailButtonsDisplayOptionsEnum.DisplayEmailBin;
		}

		private void buttonXEmailBin_Click(object sender, EventArgs e)
		{
			SelectedOption = ViewOptions.EmailBinAdd;
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