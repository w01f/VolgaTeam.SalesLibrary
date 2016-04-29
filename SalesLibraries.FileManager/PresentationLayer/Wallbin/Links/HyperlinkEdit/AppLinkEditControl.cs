﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class AppLinkEditControl : UserControl, IHyperLinkEditControl
	{
		public AppLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				laName.Font = new Font(laName.Font.FontFamily, laName.Font.Size - 2, laName.Font.Style);
				laPath.Font = new Font(laPath.Font.FontFamily, laPath.Font.Size - 2, laPath.Font.Style);
				laSecondPath.Font = new Font(laSecondPath.Font.FontFamily, laSecondPath.Font.Size - 2, laSecondPath.Font.Style);
			}
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (AppLinkInfo)GetHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.Name))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link name before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.Path))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link path before saving");
				return false;
			}
			return true;
		}

		public BaseNetworkLink GetHyperLinkInfo()
		{
			return new AppLinkInfo
			{
				Name = textEditName.EditValue as String,
				Path = textEditPath.EditValue as String,
				SecondPath = textEditSecondPath.EditValue as String,
				FormatAsBluelink = checkEditBlueHyperlink.Checked,
				FormatBold = checkEditBold.Checked,
			};
		}

		public void ApplySharedSettings(BaseNetworkLink templateEditor)
		{
			if (templateEditor != null)
			{
				textEditName.EditValue = templateEditor.Name;
				checkEditBlueHyperlink.Checked = templateEditor.FormatAsBluelink;
				checkEditBold.Checked = templateEditor.FormatBold;
			}
			if (templateEditor is HyperLinkInfo)
			{
				textEditPath.EditValue = ((HyperLinkInfo)templateEditor).Path;
			}
		}
	}
}