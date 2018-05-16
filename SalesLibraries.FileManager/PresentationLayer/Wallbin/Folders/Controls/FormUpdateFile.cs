using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class FormUpdateFile : MetroForm
	{
		public FormUpdateFile()
		{
			InitializeComponent();

			layoutControlItemAddLink.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAddLink.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAddLink.MinSize = RectangleHelper.ScaleSize(layoutControlItemAddLink.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemUpdateLink.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUpdateLink.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemUpdateLink.MinSize = RectangleHelper.ScaleSize(layoutControlItemUpdateLink.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
