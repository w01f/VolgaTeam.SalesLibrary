using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class ExcelSettingsLoader : BaseLibraryObjectLoader
	{
		private ExcelSettingsEditor SettingsEditor => (ExcelSettingsEditor)_editor;

		public ExcelSettingsLoader(BaseContextMenuEditor editor, IList<LibraryObjectLink> targetLinks) : base(editor, targetLinks) { }

		public override void SetMenuItemsViibility()
		{
			SettingsEditor.ItemDoNotGenerateContentText.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemForceDownload.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemForceOpen.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLinks()
		{
			if (!TargetLinks.Any()) return;

			_loading = true;

			var settings = TargetLinks.Select(link => link.Settings).OfType<ExcelLinkSettings>().ToList();

			SettingsEditor.ItemIsArchiveResource.Checked = settings.All(s => s.IsArchiveResource);
			SettingsEditor.ItemDoNotGenerateContentText.Checked = settings.All(s => !s.GenerateContentText);
			SettingsEditor.ItemForceDownload.Checked = settings.All(s => s.ForceDownload);
			SettingsEditor.ItemForceOpen.Checked = settings.All(s => s.ForceOpen);

			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForceDownload.Enabled = !SettingsEditor.ItemIsArchiveResource.Checked;

			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			foreach (var linkSettings in TargetLinks.Select(link => link.Settings).OfType<ExcelLinkSettings>())
			{
				linkSettings.IsArchiveResource = SettingsEditor.ItemIsArchiveResource.Checked;
				if (!linkSettings.IsArchiveResource)
				{
					linkSettings.GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
					linkSettings.ForceDownload = SettingsEditor.ItemForceDownload.Checked;
				}
				else
				{
					_loading = true;
					SettingsEditor.ItemDoNotGenerateContentText.Checked = true;
					SettingsEditor.ItemForceDownload.Checked = true;
					_loading = false;
				}

				linkSettings.ForceOpen = SettingsEditor.ItemForceOpen.Checked;
			}

			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForceDownload.Enabled = !SettingsEditor.ItemIsArchiveResource.Checked;
			
			RaiseSettingsChanged();
		}
	}
}
