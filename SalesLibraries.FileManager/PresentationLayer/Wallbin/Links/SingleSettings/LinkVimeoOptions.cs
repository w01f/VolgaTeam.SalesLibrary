﻿using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(VimeoLink))]
	//public sealed partial class LinkVimeoOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkVimeoOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private VimeoLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Vimeo</size>", Logo = Resources.LinkAddVimeo };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkVimeoOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkVimeoOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (VimeoLink)sourceLink;

			textEditName.EditValue = _data.Name;
			textEditPath.EditValue = _data.RelativePath;
			checkEditForcePreview.Checked = ((HyperLinkSettings)_data.Settings).ForcePreview;
			buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _data.PreviewContainerName);
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			_data.RelativePath = textEditPath.EditValue as String;
			((HyperLinkSettings)_data.Settings).ForcePreview = checkEditForcePreview.Checked;
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _data.LinkInfoDisplayName, Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				_data.ClearPreviewContainer();
				var previewContainer = _data.GetPreviewContainer();
				var previewGenerator = previewContainer.GetPreviewContentGenerator();
				try
				{
					previewContainer.UpdatePreviewContent(previewGenerator, cancelationToken);
				}
				catch { }
			});
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1} now updated for the server!", _data.LinkInfoDisplayName, Environment.NewLine));
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.PreviewContainerPath);
			}
			catch { }
		}
	}
}
