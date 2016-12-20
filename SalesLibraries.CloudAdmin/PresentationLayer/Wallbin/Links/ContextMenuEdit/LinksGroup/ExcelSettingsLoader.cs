using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	class ExcelSettingsLoader : BaseLibraryObjectsLoader
	{
		private ExcelSettingsEditor SettingsEditor => (ExcelSettingsEditor)_editor;

		public ExcelSettingsLoader(BaseContextMenuEditor editor, IEnumerable<ExcelLink> targetLinks) : base(editor, targetLinks) { }

		protected override void SetMenuItemsViibility()
		{
			_editor.ItemsContainer.Visibility = TargetLinks.Any() ? BarItemVisibility.Always : BarItemVisibility.Never;
		}

		public override void LoadLinks()
		{
			base.LoadLinks();

			if (!TargetLinks.Any()) return;

			_loading = true;

			var differentSettings = TargetLinks
				.Select(l => l.Settings)
				.OfType<ExcelLinkSettings>()
				.Distinct()
				.ToList();

			var settingsTemplate = differentSettings.Count == 1
				? differentSettings.First()
				: BaseLinkSettings.CreateEmpty<ExcelLinkSettings>(TargetLinks.First());


			SettingsEditor.ItemIsArchiveResource.Checked = settingsTemplate.IsArchiveResource;
			SettingsEditor.ItemDoNotGenerateContentText.Checked = !settingsTemplate.GenerateContentText;
			SettingsEditor.ItemForceDownload.Checked = settingsTemplate.ForceDownload;
			SettingsEditor.ItemForceOpen.Checked = settingsTemplate.ForceOpen;

			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForceDownload.Enabled = !settingsTemplate.IsArchiveResource;

			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			foreach (var targetLink in TargetLinks)
			{
				((ExcelLinkSettings)targetLink.Settings).IsArchiveResource = SettingsEditor.ItemIsArchiveResource.Checked;
				if (!((ExcelLinkSettings)targetLink.Settings).IsArchiveResource)
				{
					((ExcelLinkSettings)targetLink.Settings).GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
					((ExcelLinkSettings)targetLink.Settings).ForceDownload = SettingsEditor.ItemForceDownload.Checked;
				}
				else
				{
					_loading = true;
					SettingsEditor.ItemDoNotGenerateContentText.Checked = true;
					SettingsEditor.ItemForceDownload.Checked = true;
					_loading = false;
				}

				((ExcelLinkSettings)targetLink.Settings).ForceOpen = SettingsEditor.ItemForceOpen.Checked;

				SettingsEditor.ItemDoNotGenerateContentText.Enabled =
					SettingsEditor.ItemForceDownload.Enabled = !((ExcelLinkSettings)targetLink.Settings).IsArchiveResource;
			}

			RaiseSettingsChanged();
		}
	}
}
