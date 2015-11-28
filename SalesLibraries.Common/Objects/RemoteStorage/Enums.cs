namespace SalesLibraries.Common.Objects.RemoteStorage
{
	public enum AppTypeEnum
	{
		None = 0,
		FileManager,
		SalesDepot,
	}

	public enum DataActualityState
	{
		None,
		NotExisted,
		Outdated,
		Updated
	}
}
