using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class FormDeleteLink : MetroForm
	{
		public FormDeleteLink()
		{
			InitializeComponent();

			layoutControlItemDeleteLink.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDeleteLink.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDeleteLink.MinSize = RectangleHelper.ScaleSize(layoutControlItemDeleteLink.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDeleteLinkAndRelatedLinks.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDeleteLinkAndRelatedLinks.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDeleteLinkAndRelatedLinks.MinSize = RectangleHelper.ScaleSize(layoutControlItemDeleteLinkAndRelatedLinks.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
