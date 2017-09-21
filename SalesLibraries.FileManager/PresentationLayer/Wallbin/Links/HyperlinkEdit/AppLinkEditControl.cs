﻿using System;
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

		public BaseNetworkLinkInfo GetHyperLinkInfo()
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

		public void ApplyDataFromTemplate(BaseNetworkLinkInfo templateInfo)
		{
			if (templateInfo != null)
			{
				textEditName.EditValue = templateInfo.Name;
				checkEditBlueHyperlink.Checked = templateInfo.FormatAsBluelink;
				checkEditBold.Checked = templateInfo.FormatBold;
			}
			if (templateInfo is HyperLinkInfo)
			{
				textEditPath.EditValue = ((HyperLinkInfo)templateInfo).Path;
			}
		}
	}
}
