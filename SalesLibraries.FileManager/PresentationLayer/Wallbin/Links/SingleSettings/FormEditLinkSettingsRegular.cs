using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkSettingsRegular : MetroForm, ILinkSetSettingsEditForm
	{
		private readonly BaseLibraryLink _sourceLink;
		private readonly ILinksGroup _sourceLinkGroup;
		private readonly LinkType? _defaultLinkType;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.ExpirationDate,
			LinkSettingsType.Notes,
			LinkSettingsType.Security,
			LinkSettingsType.AdminSettings,
		};

		private FormEditLinkSettingsRegular()
		{
			InitializeComponent();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public FormEditLinkSettingsRegular(BaseLibraryLink sourceLink) : this()
		{
			_sourceLink = sourceLink;
			Width = Width - layoutControlItemLinksTree.Width;
			layoutControlItemLinksTree.Visibility = LayoutVisibility.Never;
		}

		public FormEditLinkSettingsRegular(ILinksGroup linkGroup, LinkType? defaultLinkType = null) : this()
		{
			_sourceLinkGroup = linkGroup;
			_sourceLink = _sourceLinkGroup.AllGroupLinks
				.FirstOrDefault(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value);
			_defaultLinkType = defaultLinkType;

			layoutControlItemLinksTree.Visibility = LayoutVisibility.Always;
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			StartPosition = FormStartPosition.CenterParent;

			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder.LocalPath, String.Format("Site Admin-Link-Settings-{0}", _sourceLinkGroup != null ? "Link-Group" : "Single-Link"), false, false);

			Text = _sourceLinkGroup == null ? _sourceLink.ToString() : _sourceLinkGroup.LinkGroupName ?? "Multi-Link Settings";

			AddOptionPages(
				ObjectIntendHelper.GetObjectInstances(
						typeof(TEditControl),
						_sourceLink == null ?
							_defaultLinkType.HasValue ? BaseLibraryLink.GetObjectTypeByLinkType(_defaultLinkType.Value) : null :
							EntitySettingsResolver.ExtractObjectTypeFromProxy(_sourceLink.GetType()),
						GetEditControlParams())
					.OfType<ILinkSettingsEditControl>()
					.Where(lp =>
						lp.SupportedSettingsTypes.Contains(settingsType))
					.OrderBy(lp => lp.Order));

			var headerInfo = GetFormTitle(settingsType);
			labelControlTitle.Text = String.Format("{0}{1}",
				headerInfo.Logo != null ? "  " : String.Empty,
				headerInfo.Title);
			labelControlTitle.Appearance.Image = headerInfo.Logo;

			if (_sourceLinkGroup != null)
			{
				linksTreeSelector.LinkSelected += (o, e) =>
				{
					SaveData();
					foreach (var settingsEditControl in xtraTabControl.TabPages.OfType<ILinkSetSettingsEditControl>())
						settingsEditControl.LoadData(linksTreeSelector.SelectedLinks);
				};
				linksTreeSelector.LoadData(_sourceLinkGroup, _defaultLinkType);
			}
			else
				foreach (var settingsEditControl in xtraTabControl.TabPages.OfType<ILinkSettingsEditControl>())
					settingsEditControl.LoadData(_sourceLink);
		}

		private SettingsEditorHeaderInfo GetFormTitle(LinkSettingsType settingsType)
		{
			var customEditorHeaderInfo = xtraTabControl.TabPages
				.OfType<ILinkSettingsEditControl>()
				.Where(editor => editor.HeaderInfo != null)
				.Select(editor => editor.HeaderInfo)
				.FirstOrDefault();

			if (customEditorHeaderInfo != null)
				return customEditorHeaderInfo;

			switch (settingsType)
			{
				case LinkSettingsType.ExpirationDate:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Do you want to set an Expiration Date?</size>" };
				case LinkSettingsType.Notes:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Link Settings</size>" };
				case LinkSettingsType.Security:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Security</size>" };
				default:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Link Settings</size>" };
			}
		}

		private void AddOptionPages(IEnumerable<ILinkSettingsEditControl> pages)
		{
			var optionPages = pages.ToArray();
			foreach (var optionPage in optionPages)
				optionPage.ForceCloseRequested += (o, e) => ForceClose();
			xtraTabControl.ShowTabHeader = optionPages.Length > 1 ? DefaultBoolean.True : DefaultBoolean.False;
			xtraTabControl.TabPages.AddRange(optionPages.OfType<XtraTabPage>().ToArray());
		}

		private object[] GetEditControlParams()
		{
			return _sourceLinkGroup == null ?
				(_defaultLinkType.HasValue ? new object[] { _defaultLinkType } : null) :
				new object[] { _sourceLinkGroup, _defaultLinkType };
		}

		private void SaveData()
		{
			foreach (var optionPage in xtraTabControl.TabPages.OfType<ILinkSettingsEditControl>())
				optionPage.SaveData();
		}

		private void ForceClose()
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormEditLinkSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
		}
	}
}