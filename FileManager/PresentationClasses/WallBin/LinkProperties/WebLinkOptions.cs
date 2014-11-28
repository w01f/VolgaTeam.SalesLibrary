using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public partial class WebLinkOptions : LinkBaseOptions
	{
		public WebLinkOptions()
		{
			InitializeComponent();
		}

		public WebLinkOptions(LibraryLink data)
			: base(data)
		{
			InitializeComponent();
			LoadData();
		}

		private new void LoadData()
		{
			ckIsUrl365.Checked = _data.ExtendedProperties.IsUrl365;
		}

		public override void SaveData()
		{
			base.SaveData();
			_data.ExtendedProperties.IsUrl365 = ckIsUrl365.Checked;
		}
	}
}
