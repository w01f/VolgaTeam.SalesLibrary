using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkSettingsRegular : MetroForm, ILinkSetSettingsEditForm
	{
		private readonly BaseLibraryLink _sourceLink;
		private readonly ILinksGroup _sourceLinkGroup;
		private readonly FileTypes? _defaultLinkType;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.AdvancedSettings,
			LinkSettingsType.ExpirationDate,
			LinkSettingsType.Notes,
			LinkSettingsType.Security,
			LinkSettingsType.Tags,
			LinkSettingsType.AdminSettings,
		};

		private FormEditLinkSettingsRegular()
		{
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

				xtraTabControl.AppearancePage.HeaderActive.Font = new Font(xtraTabControl.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControl.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControl.AppearancePage.HeaderActive.Font.Style);
				xtraTabControl.AppearancePage.Header.Font = new Font(xtraTabControl.AppearancePage.Header.Font.FontFamily, xtraTabControl.AppearancePage.Header.Font.Size - 2, xtraTabControl.AppearancePage.Header.Font.Style);
				xtraTabControl.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControl.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControl.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControl.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControl.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControl.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Style);

				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			}
		}

		public FormEditLinkSettingsRegular(BaseLibraryLink sourceLink) : this()
		{
			_sourceLink = sourceLink;
		}

		public FormEditLinkSettingsRegular(ILinksGroup linkGroup, FileTypes? defaultLinkType = null) : this()
		{
			_sourceLinkGroup = linkGroup;
			_sourceLink = _sourceLinkGroup.AllLinks
				.FirstOrDefault(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value);
			_defaultLinkType = defaultLinkType;
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			Width = 680;
			Height = 670;
			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Link-Settings", false, false);
			Text = _sourceLinkGroup == null ? _sourceLink.ToString() : "Multi-Link Settings";
			StartPosition = FormStartPosition.CenterParent;
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
				case LinkSettingsType.AdvancedSettings:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Advanced Settings</size>" };
				case LinkSettingsType.ExpirationDate:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Do you want to set an Expiration Date?</size>" };
				case LinkSettingsType.Notes:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Link Settings</size>" };
				case LinkSettingsType.Security:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Security</size>" };
				case LinkSettingsType.Tags:
					return new SettingsEditorHeaderInfo { Title = "<size=+4>Tags</size>" };
			}
			return new SettingsEditorHeaderInfo { Title = "<size=+4>Link Settings</size>" };
		}

		private void AddOptionPages(IEnumerable<ILinkSettingsEditControl> pages)
		{
			var optionPages = pages.ToArray();
			foreach (var optionPage in optionPages)
			{
				optionPage.LoadData();
				optionPage.ForceCloseRequested += (o, e) => ForceClose();
			}
			xtraTabControl.ShowTabHeader = optionPages.Length > 1 ? DefaultBoolean.True : DefaultBoolean.False;
			xtraTabControl.TabPages.AddRange(optionPages.OfType<XtraTabPage>().ToArray());
		}

		private object[] GetEditControlParams()
		{
			return _sourceLinkGroup == null ?
				new object[] { _sourceLink } :
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