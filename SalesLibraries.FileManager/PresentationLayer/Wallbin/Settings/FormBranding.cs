using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormBranding : MetroForm
	{
		public Library Library { get; set; }
		
		public FormBranding()
		{
			InitializeComponent();
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			if (DialogResult != DialogResult.OK) return;
			var brandingText = textEditBrandingName.EditValue as String;
			if (String.IsNullOrEmpty(brandingText))
			{
				e.Cancel = true;
				MainController.Instance.PopupMessages.ShowWarning("Branding Name is not set");
				return;
			}
			Library.BrandingText = brandingText;
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			textEditBrandingName.EditValue = Library.BrandingText;
		}
	}
}