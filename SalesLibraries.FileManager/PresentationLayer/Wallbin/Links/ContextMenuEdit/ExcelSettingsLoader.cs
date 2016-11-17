using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit
{
	class ExcelSettingsLoader : BaseLibraryObjectLoader
	{
		private ExcelSettingsEditor SettingsEditor => (ExcelSettingsEditor)_editor;

		private ExcelLinkSettings Settings => (ExcelLinkSettings)TargetLink.Settings;

		public ExcelSettingsLoader(BaseContextMenuEditor editor, LibraryObjectLink targetLink) : base(editor, targetLink) { }

		protected override void SetMenuItemsViibility()
		{
			SettingsEditor.ItemDoNotGenerateContentText.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemForceDownload.Visibility = BarItemVisibility.Always;
			SettingsEditor.ItemForceOpen.Visibility = BarItemVisibility.Always;
		}

		public override void LoadLink()
		{
			base.LoadLink();

			_loading = true;
			SettingsEditor.ItemDoNotGenerateContentText.Checked = !Settings.GenerateContentText;
			SettingsEditor.ItemForceDownload.Checked = Settings.ForceDownload;
			SettingsEditor.ItemForceOpen.Checked = Settings.ForceOpen;
			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			Settings.GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
			Settings.ForceDownload = SettingsEditor.ItemForceDownload.Checked;
			Settings.ForceOpen = SettingsEditor.ItemForceOpen.Checked;

			RaiseSettingsChanged();
		}
	}
}
