using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public partial class ExcelLinkOptions : LinkPreviewableOptions
	{
		public ExcelLinkOptions()
		{
			InitializeComponent();
		}

		public ExcelLinkOptions(LibraryLink data)
			: base(data)
		{
			InitializeComponent();
			LoadData();
		}

		private new void LoadData()
		{
			ckDoNotGenerateText.Checked = !_data.ExtendedProperties.GenerateContentText;
		}

		public override void SaveData()
		{
			base.SaveData();
			_data.ExtendedProperties.GenerateContentText = !ckDoNotGenerateText.Checked;
		}
	}
}
