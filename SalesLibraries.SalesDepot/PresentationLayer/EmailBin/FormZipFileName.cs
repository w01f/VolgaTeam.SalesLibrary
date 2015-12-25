using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.SalesDepot.PresentationLayer.EmailBin
{
	public partial class FormZipFileName : MetroForm
	{
		public FormZipFileName()
		{
			InitializeComponent();
			if (!((CreateGraphics()).DpiX > 96)) return;
			laLogo.Font = new System.Drawing.Font(laLogo.Font.FontFamily, laLogo.Font.Size - 2, laLogo.Font.Style);
			buttonXOK.Font = new System.Drawing.Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			buttonXCancel.Font = new System.Drawing.Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
		}

		public string FileName
		{
			get
			{
				return textEditFileName.EditValue != null ? textEditFileName.EditValue.ToString() : null;
			}
		}
	}
}
