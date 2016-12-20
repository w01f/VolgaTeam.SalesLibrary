using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
{
	class DocumentSettingsLoader : BaseLibraryObjectLoader
	{
		private DocumentSettingsEditor SettingsEditor => (DocumentSettingsEditor)_editor;

		private DocumentLinkSettings Settings => (DocumentLinkSettings)TargetLink.Settings;

		public DocumentSettingsLoader(BaseContextMenuEditor editor, LibraryObjectLink targetLink) : base(editor, targetLink) { }

		protected override void SetMenuItemsViibility()
		{
			SettingsEditor.ItemDoNotGeneratePreview.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemDoNotGenerateContentText.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemForcePreview.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLink()
		{
			base.LoadLink();

			_loading = true;

			SettingsEditor.ItemIsArchiveResource.Checked = Settings.IsArchiveResource;
			SettingsEditor.ItemDoNotGeneratePreview.Checked = !Settings.GeneratePreviewImages;
			SettingsEditor.ItemDoNotGenerateContentText.Checked = !Settings.GenerateContentText;
			SettingsEditor.ItemForcePreview.Checked = Settings.ForcePreview;

			SettingsEditor.ItemDoNotGeneratePreview.Enabled =
			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForcePreview.Enabled = !Settings.IsArchiveResource;

			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			Settings.IsArchiveResource = SettingsEditor.ItemIsArchiveResource.Checked;
			if (!Settings.IsArchiveResource)
			{
				Settings.GeneratePreviewImages = !SettingsEditor.ItemDoNotGeneratePreview.Checked;
				Settings.GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
				Settings.ForcePreview = SettingsEditor.ItemForcePreview.Checked;
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
				SettingsEditor.ItemForcePreview.Enabled = !Settings.IsArchiveResource;

			RaiseSettingsChanged();
		}
	}
}
