using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class LinkVideoOptions : UserControl, ILinkProperties
	public sealed partial class LinkVideoOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LinkVideoOptions(LibraryLink data)
		{
			InitializeComponent();
			Text = "Advanced";
			_data = data;
			LoadData();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
				labelControlTitle.Font = new Font(labelControlTitle.Font.FontFamily, labelControlTitle.Font.Size - 2, labelControlTitle.Font.Style);
			}
		}

		private void LoadData()
		{
			ckForcePreview.Checked = _data.ExtendedProperties.ForcePreview;
		}

		public void SaveData()
		{
			_data.ExtendedProperties.ForcePreview = ckForcePreview.Checked;
		}
	}
}
