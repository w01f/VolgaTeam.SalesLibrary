using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public partial class VideoEmbeddedLinkOptions : LinkEmbeddedOptions
	{
		public VideoEmbeddedLinkOptions()
		{
			InitializeComponent();
		}

		public VideoEmbeddedLinkOptions(LibraryLink data)
			: base(data)
		{
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
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
