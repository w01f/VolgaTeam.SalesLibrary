using System;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin;

namespace SalesLibraries.Business.Contexts.Wallbin.Cloud
{
	static class CloudDataImportHelper
	{
		public static void ImportCloudData(this CloudLibraryContext target, LibraryDataPackage source)
		{
			Library targetLibrary;
			if (!target.HasData || !target.Library.ExtId.Equals(source.Library.Id))
			{
				targetLibrary = WallbinEntity.CreateEntity<Library>(library =>
				{
					library.Context = target;
					library.Path = target.DataSourceFolderPath;
				});
				target.Libraries.Add(targetLibrary);
			}
			else
				targetLibrary = target.Library;
			targetLibrary.ImportCloudData(source.Library);

			foreach (var sourceLibraryPage in source.Pages)
			{
				var targetPage = WallbinEntity.CreateEntity<LibraryPage>();
				targetPage.ImportCloudData(sourceLibraryPage);
				targetPage.Library = targetLibrary;
				targetLibrary.Pages.Add(targetPage);
			}
		}

		#region Library
		public static void ImportCloudData(this Library target,
			ServiceConnector.Models.Rest.Wallbin.Entities.Library source)
		{
			target.ExtId = source.Id;
			target.Name = source.Name;
			target.LastModified = source.LastModified ?? DateTime.Now;
			target.Settings.ImportCloudData(source.Settings);
		}

		public static void ImportCloudData(this Library.LibrarySettings target,
			ServiceConnector.Models.Rest.Wallbin.Settings.LibrarySettings source)
		{
			target.ApplyAppearanceForAllWindows = source.ApplyAppearanceForAllWindows.HasValue && source.ApplyAppearanceForAllWindows.Value;
			target.ApplyBannerForAllWindows = source.ApplyBannerForAllWindows.HasValue && source.ApplyBannerForAllWindows.Value;
			target.ApplyWidgetForAllWindows = source.ApplyWidgetForAllWindows.HasValue && source.ApplyWidgetForAllWindows.Value;

			target.AutoWidgets.Clear();
			foreach (var sourceAutoWidget in source.AutoWidgets)
			{
				var targetAutoWidget = new AutoWidget();
				targetAutoWidget.ImportCloudData(sourceAutoWidget);
				target.AutoWidgets.Add(targetAutoWidget);
			}
		}

		public static void ImportCloudData(this AutoWidget target,
			ServiceConnector.Models.Rest.Wallbin.Settings.AutoWidget source)
		{
			target.Extension = source.Extension;
			target.Inverted = source.Inverted;
			target.Widget = source.OriginalImage;
		}
		#endregion

		#region Page
		public static void ImportCloudData(this LibraryPage target,
			ServiceConnector.Models.Rest.Wallbin.Entities.LibraryPage source)
		{
			target.ExtId = source.Id;
			target.Name = source.Name;
			target.Order = source.Order;

			target.Settings.ImportCloudData(source.Settings);
		}

		public static void ImportCloudData(this LibraryPage.LibraryPageSettings target,
			ServiceConnector.Models.Rest.Wallbin.Settings.LibraryPageSettings source)
		{
			target.EnableColumnTitles = source.EnableColumnTitles.HasValue && source.EnableColumnTitles.Value;
			target.ApplyForAllColumnTitles = source.ApplyForAllColumnTitles.HasValue && source.ApplyForAllColumnTitles.Value;
		}
		#endregion
	}
}
