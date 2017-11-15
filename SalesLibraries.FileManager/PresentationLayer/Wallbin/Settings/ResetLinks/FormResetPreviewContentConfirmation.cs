using System;
using System.Collections.Generic;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	public partial class FormResetPreviewContentConfirmation : MetroForm
	{
		public FormResetPreviewContentConfirmation(IList<string> actionNames)
		{
			InitializeComponent();
			Text = String.Format(Text, MainController.Instance.Settings.EnableLocalSync ? "QV-WV" : "WV");
			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				String.Join(", ", actionNames), MainController.Instance.Settings.EnableLocalSync ? "QV-WV" : "WV");
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
