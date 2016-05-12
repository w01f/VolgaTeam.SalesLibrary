using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkWidget : MetroForm, ILinkSettingsEditForm
	{
		private readonly BaseLibraryLink _sourceLink;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Widget,
		};

		public FormEditLinkWidget(BaseLibraryLink sourceLink)
		{
			_sourceLink = sourceLink;
			InitializeComponent();
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

				radioButtonWidgetTypeAuto.Font = new Font(radioButtonWidgetTypeAuto.Font.FontFamily, radioButtonWidgetTypeAuto.Font.Size - 2,
					radioButtonWidgetTypeAuto.Font.Style);
				radioButtonWidgetTypeCustom.Font = new Font(radioButtonWidgetTypeCustom.Font.FontFamily, radioButtonWidgetTypeCustom.Font.Size - 2,
					radioButtonWidgetTypeCustom.Font.Style);
				radioButtonWidgetTypeDisabled.Font = new Font(radioButtonWidgetTypeDisabled.Font.FontFamily, radioButtonWidgetTypeDisabled.Font.Size - 2,
					radioButtonWidgetTypeDisabled.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}
		}

		public void InitForm(LinkSettingsType settingsType)
		{
			Width = 960;
			Height = 590;
			Text = string.Format(Text, _sourceLink);
			StartPosition = FormStartPosition.CenterScreen;

			LoadData();
		}

		private void LoadData()
		{
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
			xtraTabControlWidgets.SelectedPageChanged += (o, e) => ((BaseLinkImagesContainer)e.Page).Init();
			((BaseLinkImagesContainer)xtraTabControlWidgets.SelectedTabPage).Init();

			var fileLink = _sourceLink as LibraryFileLink;
			if (fileLink != null)
			{
				radioButtonWidgetTypeAuto.Visible = true;
				pnAutoWidget.Visible = true;
				if (fileLink.Widget.HasAutoWidget)
				{
					pbAutoWidget.Visible = true;
					pbAutoWidget.Image = fileLink.Widget.AutoWidget;
					laExtension.Text = fileLink.Extension.Replace(".", String.Empty).ToLower();
				}
				else
				{
					pbAutoWidget.Visible = false;
					laExtension.Text = "Not Assigned";
				}
			}
			else
			{
				radioButtonWidgetTypeAuto.Visible = false;
				pnAutoWidget.Visible = false;
			}

			checkEditInvert.Checked = _sourceLink.Widget.Inverted;
			pbCustomWidget.Image = _sourceLink.Widget.WidgetType == WidgetType.CustomWidget ? _sourceLink.Widget.Image : null;
			switch (_sourceLink.Widget.WidgetType)
			{
				case WidgetType.AutoWidget:
					radioButtonWidgetTypeAuto.Checked = true;
					radioButtonWidgetTypeCustom.Checked = false;
					radioButtonWidgetTypeDisabled.Checked = false;
					break;
				case WidgetType.CustomWidget:
					radioButtonWidgetTypeAuto.Checked = false;
					radioButtonWidgetTypeCustom.Checked = true;
					radioButtonWidgetTypeDisabled.Checked = false;
					break;
				case WidgetType.NoWidget:
					radioButtonWidgetTypeAuto.Checked = false;
					radioButtonWidgetTypeCustom.Checked = false;
					radioButtonWidgetTypeDisabled.Checked = true;
					break;
			}
		}

		private void SaveData()
		{
			if (radioButtonWidgetTypeAuto.Checked)
			{
				_sourceLink.Widget.WidgetType = WidgetType.AutoWidget;
				_sourceLink.Widget.Image = null;
			}
			else if (radioButtonWidgetTypeCustom.Checked)
			{
				_sourceLink.Widget.WidgetType = WidgetType.CustomWidget;
				_sourceLink.Widget.Inverted = checkEditInvert.Checked;
				_sourceLink.Widget.Image = pbCustomWidget.Image;
			}
			else if (radioButtonWidgetTypeDisabled.Checked)
			{
				_sourceLink.Widget.WidgetType = WidgetType.NoWidget;
				_sourceLink.Widget.Image = null;
			}
			_sourceLink.Banner.Enable = !_sourceLink.Widget.Enabled && _sourceLink.Banner.Enable;
		}

		private void FormEditLinkSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
		}

		private void OnWidgetTypeChanged(object sender, EventArgs e)
		{
			pbCustomWidget.Enabled = radioButtonWidgetTypeCustom.Checked;
			pbAutoWidget.Enabled = radioButtonWidgetTypeAuto.Checked;
			xtraTabControlWidgets.Enabled = radioButtonWidgetTypeCustom.Checked;
			pnSearch.Enabled = radioButtonWidgetTypeCustom.Checked;
			checkEditInvert.Enabled = radioButtonWidgetTypeCustom.Checked;
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			pbCustomWidget.Image = e.Image;
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
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