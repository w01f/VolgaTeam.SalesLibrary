using System.Linq;

namespace SalesLibraries.Common.Objects.Graphics
{
	public class RegularImageGroup : SourceFolderImageGroup
	{
		public RegularImageGroup(IImageSourceList parentList, string sourcePath) : base(parentList, sourcePath) { }

		public override void LoadImages<TImageSource>()
		{
			base.LoadImages<TImageSource>();

			foreach (var imageSource in Images)
			{
				imageSource.AddToFavs += (o, e) =>
				{
					var favoritesImagesGroup = ParentList.Items.OfType<FavoriteImageGroup>().FirstOrDefault();
					if (favoritesImagesGroup == null) return;
					favoritesImagesGroup.AddImageSource(imageSource);
				};
			}
		}
	}
}
