using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkSettingsEmbedded : MetroForm
	{
		private readonly LibraryFileLink _sourceLink;
		private readonly LibraryFolderLink _parentFolderLink;
		private LinkSettingsType _editedSettingsType;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Notes,
			LinkSettingsType.Security,
			LinkSettingsType.Tags
		};

		public FormEditLinkSettingsEmbedded(BaseLibraryLink sourceLink, LibraryFolderLink parentFolderLink)
		{
			_parentFolderLink = parentFolderLink;
			_sourceLink = (LibraryFileLink)sourceLink;
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				xtraTabControl.AppearancePage.HeaderActive.Font = new Font(xtraTabControl.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControl.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControl.AppearancePage.HeaderActive.Font.Style);
				xtraTabControl.AppearancePage.Header.Font = new Font(xtraTabControl.AppearancePage.Header.Font.FontFamily, xtraTabControl.AppearancePage.Header.Font.Size - 2, xtraTabControl.AppearancePage.Header.Font.Style);
				xtraTabControl.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControl.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControl.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControl.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControl.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControl.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Style);
			}
		}

		public void InitForm(LinkSettingsType settingsType)
		{
			Width = 680;
			Height = 550;
			Text = _sourceLink.ToString();
			StartPosition = FormStartPosition.CenterScreen;

			_editedSettingsType = settingsType;

			var folderSettings = (LibraryFolderLinkSettings)_parentFolderLink.Settings;
			checkEditApplyForAll.Checked = folderSettings.SettingsTemplates
				.Any(st => st.SettingsType == settingsType && st.FileType == _sourceLink.Type);
			
			AddOptionPages(
				ObjectIntendHelper.GetObjectInstances(
					typeof(ILinkSettingsEditControl),
					EntitySettingsResolver.ExtractObjectTypeFromProxy(_sourceLink.GetType()), _sourceLink)
					.OfType<ILinkSettingsEditControl>()
					.Where(lp =>
						lp.SupportedSettingsTypes.Contains(settingsType) && lp.AvailableForEmbedded)
					.OrderBy(lp => lp.Order));
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

		private void SaveData()
		{
			var folderSettings = (LibraryFolderLinkSettings)_parentFolderLink.Settings;
			foreach (var optionPage in xtraTabControl.TabPages.OfType<ILinkSettingsEditControl>())
			{
				optionPage.SaveData();

				folderSettings.ProcessUniverslaLinkSettings(_editedSettingsType, _sourceLink, checkEditApplyForAll.Checked);
			}
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