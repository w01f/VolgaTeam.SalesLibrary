using System.Drawing;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings
{
	public class HyperlinkPreviewContainerSettings : BasePreviewContainerSettings
	{
		private Image _customThumbnail;
		public Image CustomThumbnail
		{
			get => _customThumbnail;
			set
			{
				if (_customThumbnail != value)
					OnSettingsChanged();
				_customThumbnail = value;
			}
		}
	}
}
