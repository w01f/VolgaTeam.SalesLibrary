using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public partial class ExcelEmbeddedLinkOptions : LinkEmbeddedOptions
	{
		public ExcelEmbeddedLinkOptions()
		{
			InitializeComponent();
		}

		public ExcelEmbeddedLinkOptions(LibraryLink data)
			: base(data)
		{
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
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
