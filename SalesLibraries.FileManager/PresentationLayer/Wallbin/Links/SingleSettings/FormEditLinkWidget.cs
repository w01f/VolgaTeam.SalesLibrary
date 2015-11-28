using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
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

			pbSelectedWidget.Image = _sourceLink.Widget.Enable ? _sourceLink.Widget.Image : null;
			checkBoxEnableWidget.Checked = _sourceLink.Widget.Enable;
		}

		private void SaveData()
		{
			_sourceLink.Widget.Enable = checkBoxEnableWidget.Checked;
			_sourceLink.Widget.Image = pbSelectedWidget.Image;
			_sourceLink.Banner.Enable = !_sourceLink.Widget.Enable && _sourceLink.Banner.Enable;
		}

		private void FormEditLinkSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
		}

		private void checkBoxEnableWidget_CheckedChanged(object sender, EventArgs e)
		{
			pbSelectedWidget.Enabled = checkBoxEnableWidget.Checked;
			xtraTabControlWidgets.Enabled = checkBoxEnableWidget.Checked;
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			pbSelectedWidget.Image = e.Image;
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormEditLinkWidget_Shown(object sender, EventArgs e)
		{
			labelControlInfo.ForeColor = Color.Gray;
		}
	}
}