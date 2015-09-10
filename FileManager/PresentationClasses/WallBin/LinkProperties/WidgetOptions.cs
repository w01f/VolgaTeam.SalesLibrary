using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using FileManager.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class WidgetOptions : UserControl, ILinkProperties
	public partial class WidgetOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;
		
		public WidgetOptions(LibraryLink data)
		{
			InitializeComponent();
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
				laAvailableWidgets.Font = new Font(laAvailableWidgets.Font.FontFamily, laAvailableWidgets.Font.Size - 2, laAvailableWidgets.Font.Style);
				laSelectedWidget.Font = new Font(laSelectedWidget.Font.FontFamily, laSelectedWidget.Font.Size - 2, laSelectedWidget.Font.Style);
				checkBoxEnableWidget.Font = new Font(checkBoxEnableWidget.Font.FontFamily, checkBoxEnableWidget.Font.Size - 2, checkBoxEnableWidget.Font.Style);
			}
		}

		private void LoadData()
		{
			xtraTabControlWidgets.TabPages.Clear();
			foreach (var imageGroup in ListManager.Instance.Widgets.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlWidgets.TabPages.Add(tabPage);
			}

			pbSelectedWidget.Image = _data.EnableWidget ? _data.Widget : null;
			laWidgetFileName.Text = String.Empty;
			checkBoxEnableWidget.Checked = _data.EnableWidget;

		}

		public void SaveData()
		{
			_data.EnableWidget = checkBoxEnableWidget.Checked;
			_data.Widget = pbSelectedWidget.Image;
			_data.BannerProperties.Enable = !_data.EnableWidget && _data.BannerProperties.Enable;
		}

		private void checkBoxEnableWidget_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxWidgets.Enabled = checkBoxEnableWidget.Checked;
		}

		private void xtraTabControlWidgets_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			laWidgetFileName.Text = string.Empty;
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			pbSelectedWidget.Image = e.Image;
			laWidgetFileName.Text = string.Empty;
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			if (OnForseClose != null)
				OnForseClose(this, EventArgs.Empty);
		}
	}
}
