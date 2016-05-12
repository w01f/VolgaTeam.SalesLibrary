namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface ICollectionItem
	{
		IChangable Parent { get; set; }
		int CollectionOrder { get; set; }
	}
}
