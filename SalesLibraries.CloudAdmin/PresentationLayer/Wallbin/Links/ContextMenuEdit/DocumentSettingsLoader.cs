using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit
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
			SettingsEditor.ItemForcePreview.Visibility = TargetLink is PowerPointLink ? BarItemVisibility.Never : BarItemVisibility.Always;
		}

		public override void LoadLink()
		{
			base.LoadLink();

			_loading = true;
			SettingsEditor.ItemDoNotGeneratePreview.Checked = !Settings.GeneratePreviewImages;
			SettingsEditor.ItemDoNotGenerateContentText.Checked = !Settings.GenerateContentText;
			SettingsEditor.ItemForcePreview.Checked = Settings.ForcePreview;
			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			Settings.GeneratePreviewImages = !SettingsEditor.ItemDoNotGeneratePreview.Checked;
			Settings.GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
			Settings.ForcePreview = SettingsEditor.ItemForcePreview.Checked;

			RaiseSettingsChanged();
		}
	}
}
