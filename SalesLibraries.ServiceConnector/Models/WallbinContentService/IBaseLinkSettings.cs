namespace SalesLibraries.ServiceConnector.WallbinContentService
{
	public interface IBaseLinkSettings
	{
		/// <remarks/>
		string note { get; set; }

		/// <remarks/>
		string hoverNote { get; set; }

		/// <remarks/>
		bool isBold { get; set; }

		/// <remarks/>
		bool isItalic { get; set; }

		/// <remarks/>
		bool isUnderline { get; set; }

		/// <remarks/>
		bool isSpecialFormat { get; set; }

		/// <remarks/>
		Font font { get; set; }

		/// <remarks/>
		string foreColor { get; set; }

		/// <remarks/>
		bool isRestricted { get; set; }

		/// <remarks/>
		bool noShare { get; set; }

		/// <remarks/>
		string assignedUsers { get; set; }

		/// <remarks/>
		string deniedUsers { get; set; }
	}
}