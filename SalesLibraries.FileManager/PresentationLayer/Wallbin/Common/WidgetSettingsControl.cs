using System;
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
			xtraTabControlWidgets.SelectedPageChanging += (o, e) => { if (e.Page != null) ((BaseLinkImagesContainer)e.Page).Init(); };
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
	}
}