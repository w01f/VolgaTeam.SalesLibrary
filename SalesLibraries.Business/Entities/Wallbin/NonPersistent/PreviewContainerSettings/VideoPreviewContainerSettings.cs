namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings
{
	public class VideoPreviewContainerSettings : BasePreviewContainerSettings
	{
		public VideoConvertSettings VideoConvertSettings { get; private set; }

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			VideoConvertSettings = new VideoConvertSettings();
		}

		public override void AfterCreate()
		{
			base.AfterCreate();
			if (VideoConvertSettings == null)
				VideoConvertSettings = new VideoConvertSettings();
			VideoConvertSettings.SettingsContainer = this;
		}
	}
}
