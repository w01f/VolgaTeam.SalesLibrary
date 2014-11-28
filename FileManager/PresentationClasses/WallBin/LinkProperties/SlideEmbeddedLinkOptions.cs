using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public partial class SlideEmbeddedLinkOptions : LinkEmbeddedOptions
	{
		public SlideEmbeddedLinkOptions()
		{
			InitializeComponent();
		}

		public SlideEmbeddedLinkOptions(LibraryLink data)
			: base(data)
		{
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
		{
			ckDoNotGeneratePreview.Checked = !_data.ExtendedProperties.GeneratePreviewImages;
			ckDoNotGenerateText.Checked = !_data.ExtendedProperties.GenerateContentText;
		}

		public override void SaveData()
		{
			base.SaveData();
			_data.ExtendedProperties.GeneratePreviewImages = !ckDoNotGeneratePreview.Checked;
			_data.ExtendedProperties.GenerateContentText = !ckDoNotGenerateText.Checked;
		}
	}
}
