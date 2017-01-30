using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class DocumentSettingsLoader : BaseLibraryObjectLoader
	{
		private DocumentSettingsEditor SettingsEditor => (DocumentSettingsEditor)_editor;

		public DocumentSettingsLoader(BaseContextMenuEditor editor, IList<LibraryObjectLink> targetLinks) : base(editor, targetLinks) { }

		public override void SetMenuItemsViibility()
		{
			SettingsEditor.ItemDoNotGeneratePreview.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemDoNotGenerateContentText.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemForcePreview.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLinks()
		{
			if (!TargetLinks.Any()) return;

			_loading = true;

			var settings = TargetLinks.Select(link => link.Settings).OfType<DocumentLinkSettings>().ToList();

			SettingsEditor.ItemIsArchiveResource.Checked = settings.All(s => s.IsArchiveResource);
			SettingsEditor.ItemDoNotGeneratePreview.Checked = settings.All(s => !s.GeneratePreviewImages);
			SettingsEditor.ItemDoNotGenerateContentText.Checked = settings.All(s => !s.GenerateContentText);
			SettingsEditor.ItemForcePreview.Checked = settings.All(s => s.ForcePreview);

			SettingsEditor.ItemDoNotGeneratePreview.Enabled =
			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForcePreview.Enabled = !SettingsEditor.ItemIsArchiveResource.Checked;

			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			foreach (var linkSettings in TargetLinks.Select(link => link.Settings).OfType<DocumentLinkSettings>())
			{
				linkSettings.IsArchiveResource = SettingsEditor.ItemIsArchiveResource.Checked;
				if (!linkSettings.IsArchiveResource)
				{
					linkSettings.GeneratePreviewImages = !SettingsEditor.ItemDoNotGeneratePreview.Checked;
					linkSettings.GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
					linkSettings.ForcePreview = SettingsEditor.ItemForcePreview.Checked;
				}
				else
				{
					_loading = true;
					SettingsEditor.ItemDoNotGeneratePreview.Checked = true;
					SettingsEditor.ItemDoNotGenerateContentText.Checked = true;
					SettingsEditor.ItemForcePreview.Checked = true;
					_loading = false;
				}
			}

			SettingsEditor.ItemDoNotGeneratePreview.Enabled =
			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForcePreview.Enabled = !SettingsEditor.ItemIsArchiveResource.Checked;

			RaiseSettingsChanged();
		}
	}
}
