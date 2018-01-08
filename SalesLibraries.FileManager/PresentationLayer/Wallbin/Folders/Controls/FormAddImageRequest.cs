using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class FormAddImageRequest : MetroForm
	{
		public FormAddImageRequest()
		{
			InitializeComponent();

			layoutControlItemAdd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAdd.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
