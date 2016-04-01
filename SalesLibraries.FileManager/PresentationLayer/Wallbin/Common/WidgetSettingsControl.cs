using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	public partial class WidgetSettingsControl : UserControl
	{
		private bool _loading;
		private readonly WidgetSettings _data;

		public event EventHandler<CheckedChangedEventArgs> StateChanged;
		public event EventHandler<EventArgs> DoubleClicked;

		public WidgetSettingsControl(WidgetSettings data)
		{
			_data = data;
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				radioButtonWidgetTypeCustom.Font = new Font(radioButtonWidgetTypeCustom.Font.FontFamily, radioButtonWidgetTypeCustom.Font.Size - 2, radioButtonWidgetTypeCustom.Font.Style);
				radioButtonWidgetTypeDisabled.Font = new Font(radioButtonWidgetTypeDisabled.Font.FontFamily, radioButtonWidgetTypeDisabled.Font.Size - 2, radioButtonWidgetTypeDisabled.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}
		}

		public void LoadData()
		{
			_loading = true;
			xtraTabControlWidgets.TabPages.Clear();
			xtraTabControlWidgets.TabPages.AddRange(
				MainController.Instance.Lists.Widgets.Items.Select(imageGroup =>
				{
					var tabPage = BaseLinkImagesContainer.Create(imageGroup);
					tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
					tabPage.OnImageDoubleClick += OnImageDoubleClick;
					return (XtraTabPage)tabPage;
				}).ToArray()
			);
			xtraTabControlWidgets.SelectedPageChanging += (o, e) =>
			{
				if (e.Page != null && !(e.Page is SearchResultsImagesContainer))
					((BaseLinkImagesContainer)e.Page).Init();
			};
			((BaseLinkImagesContainer)xtraTabControlWidgets.SelectedTabPage).Init();

			pbCustomWidget.Image = _data.WidgetType == WidgetType.CustomWidget ? _data.Image : null;
			switch (_data.WidgetType)
			{
				case WidgetType.CustomWidget:
					radioButtonWidgetTypeCustom.Checked = true;
					radioButtonWidgetTypeDisabled.Checked = false;
					break;
				case WidgetType.NoWidget:
					radioButtonWidgetTypeCustom.Checked = false;
					radioButtonWidgetTypeDisabled.Checked = true;
					break;
			}
			_loading = false;
		}

		public void SaveData()
		{
			if (_data == null) return;
			if (radioButtonWidgetTypeCustom.Checked)
			{
				_data.WidgetType = WidgetType.CustomWidget;
				_data.Image = pbCustomWidget.Image;
			}
			else if (radioButtonWidgetTypeDisabled.Checked)
			{
				_data.WidgetType = WidgetType.NoWidget;
				_data.Image = null;
			}
		}

		public void ChangeState(bool enable)
		{
			_loading = true;
			radioButtonWidgetTypeCustom.Checked = enable;
			radioButtonWidgetTypeDisabled.Checked = !enable;
			_loading = false;
		}

		private void OnWidgetTypeChanged(object sender, EventArgs e)
		{
			pbCustomWidget.Enabled = radioButtonWidgetTypeCustom.Checked;
			pnSearch.Enabled = radioButtonWidgetTypeCustom.Checked;
			xtraTabControlWidgets.Enabled = radioButtonWidgetTypeCustom.Checked;
			if (!_loading && StateChanged != null)
				StateChanged(this, new CheckedChangedEventArgs(radioButtonWidgetTypeCustom.Checked));
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			pbCustomWidget.Image = e.Image;
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			if (DoubleClicked != null)
				DoubleClicked(this, EventArgs.Empty);
		}

		private void OnSearchButtonClick(object sender, EventArgs e)
		{
			var keyword = textEditSearch.EditValue as String;
			if (String.IsNullOrEmpty(keyword)) return;
			MainController.Instance.Lists.Widgets.SearchResults.LoadImages(keyword);
		}

		private void OnSearchEditValueChanged(object sender, EventArgs e)
		{
			buttonXSearch.Enabled = !String.IsNullOrEmpty(textEditSearch.EditValue as String);
		}

		private void OnSearchKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OnSearchButtonClick(sender, EventArgs.Empty);
		}
	}
}