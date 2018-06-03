namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings
{
	public class FilePreviewContainerSettings : BasePreviewContainerSettings
	{
		public OneDrivePreviewSettings OneDriveSettings { get; private set; }

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			OneDriveSettings = new OneDrivePreviewSettings();
		}

		public override void AfterCreate()
		{
			base.AfterCreate();
			if (OneDriveSettings == null)
				OneDriveSettings = new OneDrivePreviewSettings();
			OneDriveSettings.SettingsContainer = this;
		}
	}
}
