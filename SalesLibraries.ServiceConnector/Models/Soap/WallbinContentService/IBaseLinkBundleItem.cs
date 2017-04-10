namespace SalesLibraries.ServiceConnector.WallbinContentService
{
	public interface IBaseLinkBundleItem
	{
		/// <remarks/>
		string id { get; set; }

		/// <remarks/>
		int itemType { get; set; }

		/// <remarks/>
		int collectionOrder { get; set; }

		/// <remarks/>
		string image { get; set; }

		/// <remarks/>
		string title { get; set; }

		/// <remarks/>
		string hoverTip { get; set; }

		/// <remarks/>
		bool useAsThumbnail { get; set; }
	}
}
