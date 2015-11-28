using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormVideoViewOptions : MetroForm
	{
		public FormVideoViewOptions()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				buttonXAddToPresentation.Font = new Font(buttonXAddToPresentation.Font.FontFamily, buttonXAddToPresentation.Font.Size - 3, buttonXAddToPresentation.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
				buttonXReview.Font = new Font(buttonXReview.Font.FontFamily, buttonXReview.Font.Size - 3, buttonXReview.Font.Style);
			}
		}

		public VideoViewOptions SelectedOption { get; private set; }

		private void buttonXAddToPresentation_Click(object sender, EventArgs e)
		{
			SelectedOption = VideoViewOptions.Add;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXReview_Click(object sender, EventArgs e)
		{
			SelectedOption = VideoViewOptions.Open;
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