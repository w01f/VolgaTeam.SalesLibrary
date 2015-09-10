using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class LinkWebOptions : UserControl, ILinkProperties
	public sealed partial class LinkWebOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LinkWebOptions(LibraryLink data)
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
			ckIsUrl365.Checked = _data.ExtendedProperties.IsUrl365;
			ckForcePreview.Checked = _data.ExtendedProperties.ForcePreview;
			textEditLinkPath.EditValue = _data.RelativePath;
		}

		public void SaveData()
		{
			_data.ExtendedProperties.IsUrl365 = ckIsUrl365.Checked;
			_data.ExtendedProperties.ForcePreview = ckForcePreview.Checked;
			_data.RelativePath = textEditLinkPath.EditValue as String;
		}
	}
}
