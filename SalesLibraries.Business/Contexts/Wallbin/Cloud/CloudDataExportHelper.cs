using System;
using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin;

namespace SalesLibraries.Business.Contexts.Wallbin.Cloud
{
	static class CloudDataExportHelper
	{
		public static IVersioned ExportCloudData(this WallbinEntity source)
		{
			IVersioned target;
			if (source is Library)
			{
				target = new ServiceConnector.Models.Rest.Wallbin.Entities.Library();
				((ServiceConnector.Models.Rest.Wallbin.Entities.Library)target).ExportCloudData((Library)source);
			}
			//if (source is ColumnTitle)
			//	changeSet.ObjectType = ObjectType.Column;
			else if (source is LibraryPage)
			{
				target = new ServiceConnector.Models.Rest.Wallbin.Entities.LibraryPage();
				((ServiceConnector.Models.Rest.Wallbin.Entities.LibraryPage)target).ExportCloudData((LibraryPage)source);
			}
			else
				throw new CloudLibraryException("Local Entity type is undefined");
			//if (source is LibraryFolder)
			//	changeSet.ObjectType = ObjectType.Folder;
			//if (source is BaseLibraryLink)
			//	changeSet.ObjectType = ObjectType.Link;
			//if (source is BasePreviewContainer)
			//	changeSet.ObjectType = ObjectType.PreviewContainer;
			return target;
		}

		#region Library
		private static void ExportCloudData(this ServiceConnector.Models.Rest.Wallbin.Entities.Library target,
			Library source)
		{
			target.Id = source.ExtId;
			target.Name = source.Name;
			target.LastModified = source.LastModified;

			target.Settings = new ServiceConnector.Models.Rest.Wallbin.Settings.LibrarySettings();
			target.Settings.ExportCloudData(source.Settings);
		}

		public static void ExportCloudData(this ServiceConnector.Models.Rest.Wallbin.Settings.LibrarySettings target,
			Library.LibrarySettings source)
		{
			target.ApplyAppearanceForAllWindows = source.ApplyAppearanceForAllWindows;
			target.ApplyBannerForAllWindows = source.ApplyBannerForAllWindows;
			target.ApplyWidgetForAllWindows = source.ApplyWidgetForAllWindows;

			target.AutoWidgets = source.AutoWidgets
				.Select(sourceAutoWidget =>
				{
					var targetAutoWidget = new ServiceConnector.Models.Rest.Wallbin.Settings.AutoWidget();
					targetAutoWidget.ExportCloudData(sourceAutoWidget);
					return targetAutoWidget;
				})
				.ToArray();
		}

		public static void ExportCloudData(this ServiceConnector.Models.Rest.Wallbin.Settings.AutoWidget target,
			 AutoWidget source)
		{
			target.Extension = source.Extension;
			target.Inverted = source.Inverted;
			target.OriginalImage = source.Widget;
			target.DisplayedImage = source.DisplayedImage;
		}
		#endregion

		#region Library Page
		private static void ExportCloudData(this ServiceConnector.Models.Rest.Wallbin.Entities.LibraryPage target,
			LibraryPage source)
		{
			target.Id = source.ExtId;
			target.LibraryId = source.Library.ExtId;
			target.Name = source.Name;
			target.Order = source.Order;
			target.LastModified = source.LastModified;

			target.Settings = new ServiceConnector.Models.Rest.Wallbin.Settings.LibraryPageSettings();
			target.Settings.ExportCloudData(source.Settings);
		}

		public static void ExportCloudData(this ServiceConnector.Models.Rest.Wallbin.Settings.LibraryPageSettings target,
			LibraryPage.LibraryPageSettings source)
		{
			target.EnableColumnTitles = source.EnableColumnTitles;
			target.ApplyForAllColumnTitles = source.ApplyForAllColumnTitles;
		}
		#endregion
	}
}
