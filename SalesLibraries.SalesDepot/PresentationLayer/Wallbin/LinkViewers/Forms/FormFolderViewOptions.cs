using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.SalesDepot.Business.LinkViewers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormFolderViewOptions : MetroForm
	{
		public ViewOptions SelectedOption { get; private set; }

		public FormFolderViewOptions()
		{
			InitializeComponent();
		}

		private void buttonXOpen_Click(object sender, EventArgs e)
		{
			SelectedOption = ViewOptions.Open;
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