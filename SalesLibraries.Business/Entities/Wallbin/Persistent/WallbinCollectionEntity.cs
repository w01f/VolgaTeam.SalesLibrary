using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public abstract class WallbinCollectionEntity : WallbinEntity, ICollectionItem
	{
		public abstract IChangable Parent { get; set; }
		public abstract int CollectionOrder { get; set; }

		public override void MarkAsModified()
		{
			base.MarkAsModified();
			if (Parent != null)
				Parent.MarkAsModified();
		}
	}
}
