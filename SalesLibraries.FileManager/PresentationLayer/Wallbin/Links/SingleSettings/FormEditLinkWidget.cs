﻿using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkWidget : MetroForm, ILinkSettingsEditForm
	{
		private readonly BaseLibraryLink _sourceLink;

		public LinkSettingsType[] EditableSettings
		{
			get
			{
				return new[]
				{
					LinkSettingsType.Widget,
				};
			}
		}

		public bool IsForEmbedded
		{
			get { return false; }
		}

		public FormEditLinkWidget(BaseLibraryLink sourceLink)
		{
			_sourceLink = sourceLink;
			InitializeComponent();
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
			foreach (var imageGroup in MainController.Instance.Lists.Widgets.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlWidgets.TabPages.Add(tabPage);
			}

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
	}
}