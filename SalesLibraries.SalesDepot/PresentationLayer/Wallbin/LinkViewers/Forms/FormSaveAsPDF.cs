using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormSaveAsPDF : MetroForm
	{
		public FormSaveAsPDF()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				rbFile.Font = new Font(rbFile.Font.FontFamily, rbFile.Font.Size - 2, rbFile.Font.Style);
				rbSlide.Font = new Font(rbSlide.Font.FontFamily, rbSlide.Font.Size - 2, rbSlide.Font.Style);
			}
		}

		public bool WholeFile { get; set; }

		private void FormSaveAsPDF_FormClosed(object sender, FormClosedEventArgs e)
		{
			WholeFile = rbFile.Checked;
		}
	}
}