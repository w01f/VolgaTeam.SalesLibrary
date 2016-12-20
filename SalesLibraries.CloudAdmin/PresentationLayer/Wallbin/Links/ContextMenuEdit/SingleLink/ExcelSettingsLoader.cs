using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink
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

			SettingsEditor.ItemIsArchiveResource.Checked = Settings.IsArchiveResource;
			SettingsEditor.ItemDoNotGenerateContentText.Checked = !Settings.GenerateContentText;
			SettingsEditor.ItemForceDownload.Checked = Settings.ForceDownload;
			SettingsEditor.ItemForceOpen.Checked = Settings.ForceOpen;

			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForceDownload.Enabled = !Settings.IsArchiveResource;

			_loading = false;
		}

		public void OnValuesChanged()
		{
			if (_loading) return;

			Settings.IsArchiveResource = SettingsEditor.ItemIsArchiveResource.Checked;
			if (!Settings.IsArchiveResource)
			{
				Settings.GenerateContentText = !SettingsEditor.ItemDoNotGenerateContentText.Checked;
				Settings.ForceDownload = SettingsEditor.ItemForceDownload.Checked;
			}
			else
			{
				_loading = true;
				SettingsEditor.ItemDoNotGenerateContentText.Checked = true;
				SettingsEditor.ItemForceDownload.Checked = true;
				_loading = false;
			}

			Settings.ForceOpen = SettingsEditor.ItemForceOpen.Checked;

			SettingsEditor.ItemDoNotGenerateContentText.Enabled =
				SettingsEditor.ItemForceDownload.Enabled = !Settings.IsArchiveResource;
			
			RaiseSettingsChanged();
		}
	}
}
