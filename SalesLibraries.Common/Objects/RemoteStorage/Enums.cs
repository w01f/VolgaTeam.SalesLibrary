namespace SalesLibraries.Common.Objects.RemoteStorage
{
	public enum AppTypeEnum
	{
		None = 0,
		FileManager,
		CloudAdmin,
		SalesDepot,
		SiteManager
	}

	public enum DataActualityState
	{
		None,
		NotExisted,
		Outdated,
		Updated
	}
}
