using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public partial class SlideLinkOptions : LinkPreviewableOptions
	{
		public SlideLinkOptions()
		{
			InitializeComponent();
		}

		public SlideLinkOptions(LibraryLink data)
			: base(data)
		{
			InitializeComponent();
			LoadData();
		}

		private new void LoadData()
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
