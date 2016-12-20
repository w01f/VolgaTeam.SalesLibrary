using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	class DocumentSettingsLoader : BaseLibraryObjectsLoader
	{
		private DocumentSettingsEditor SettingsEditor => (DocumentSettingsEditor)_editor;

		public DocumentSettingsLoader(BaseContextMenuEditor editor, IEnumerable<DocumentLink> targetLinks) : base(editor, targetLinks) { }

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
				.OfType<DocumentLinkSettings>()
				.Distinct<DocumentLinkSettings>()
				.ToList();

			var settingsTemplate = differentSettings.Count == 1
				? differentSettings.First()
				: BaseLinkSettings.CreateEmpty<DocumentLinkSettings>(TargetLinks.First());

			SettingsEditor.ItemIsArchiveResource.Checked = settingsTemplate.IsArchiveResource;
			SettingsEditor.ItemDoNotGeneratePreview.Checked = !settingsTemplate.GeneratePreviewImages;
			SettingsEditor.ItemDoNotGenerateContentText.Checked = !settingsTemplate.GenerateContentText;
			SettingsEditor.ItemForcePreview.Checked = settingsTemplate.ForcePreview;

			SettingsEditor.ItemDoNotGeneratePreview.Enabled =
			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForcePreview.Enabled = !settingsTemplate.IsArchiveResource;

			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			foreach (var targetLink in TargetLinks)
			{
				((DocumentLinkSettings)targetLink.Settings).IsArchiveResource = SettingsEditor.ItemIsArchiveResource.Checked;
				if (!((DocumentLinkSettings)targetLink.Settings).IsArchiveResource)
				{
					((DocumentLinkSettings)targetLink.Settings).GeneratePreviewImages = !SettingsEditor.ItemDoNotGeneratePreview.Checked;
					((DocumentLinkSettings)targetLink.Settings).GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
					((DocumentLinkSettings)targetLink.Settings).ForcePreview = SettingsEditor.ItemForcePreview.Checked;
				}
				else
				{
					_loading = true;
					SettingsEditor.ItemDoNotGeneratePreview.Checked = true;
					SettingsEditor.ItemDoNotGenerateContentText.Checked = true;
					SettingsEditor.ItemForcePreview.Checked = true;
					_loading = false;
				}

				SettingsEditor.ItemDoNotGeneratePreview.Enabled =
				SettingsEditor.ItemDoNotGenerateContentText.Enabled =
					SettingsEditor.ItemForcePreview.Enabled = !((DocumentLinkSettings)targetLink.Settings).IsArchiveResource;
			}

			RaiseSettingsChanged();
		}
	}
}
