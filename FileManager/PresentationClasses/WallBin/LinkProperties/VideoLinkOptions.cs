using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public partial class VideoLinkOptions : LinkPreviewableOptions
	{
		public VideoLinkOptions()
		{
			InitializeComponent();
		}

		public VideoLinkOptions(LibraryLink data)
			: base(data)
		{
			InitializeComponent();
			LoadData();
		}

		private new void LoadData()
		{
			ckForcePreview.Checked = _data.ExtendedProperties.ForcePreview;
		}

		public override void SaveData()
		{
			base.SaveData();
			_data.ExtendedProperties.ForcePreview = ckForcePreview.Checked;
		}
	}
}
