namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface ICollectionItem
	{
		IChangable Parent { get; }
		int CollectionOrder { get; set; }
	}
}
