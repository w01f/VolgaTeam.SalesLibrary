using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.SalesDepot.PresentationLayer.EmailBin
{
	public partial class FormZipFileName : MetroForm
	{
		public FormZipFileName()
		{
			InitializeComponent();
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public string FileName => textEditFileName.EditValue != null ? textEditFileName.EditValue.ToString() : null;
	}
}
