using System;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class LanLinkEditControl : UserControl, IHyperLinkEditControl
	{
		public LanLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (LanLinkInfo)PrepareHyperLinkInfo();
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
			if (!(Directory.Exists(linkInfo.Path) || File.Exists(linkInfo.Path)))
			{
				MainController.Instance.PopupMessages.ShowWarning("Link path is not correct");
				return false;
			}
			return true;
		}

		public BaseNetworkLinkInfo PrepareHyperLinkInfo()
		{
			return new LanLinkInfo
			{
				Name = textEditName.EditValue as String,
				Path = textEditPath.EditValue as String,
				FormatAsBluelink = checkEditBlueHyperlink.Checked,
				FormatBold = checkEditBold.Checked,
			};
		}

		public BaseNetworkLinkInfo GetFinalHyperLinkInfo()
		{
			return PrepareHyperLinkInfo();
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
