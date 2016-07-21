using System;
using System.Drawing;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.Business.Contexts.Wallbin.Local
{
	static class LegacyImportHelper
	{
		public static void ImportLegacyData(this Library target, string libraryPath)
		{
			if (!File.Exists(Path.Combine(libraryPath, Constants.LegacyStorageFileName))) return;

			var legacyLibrary = new Legacy.Entities.Library(target.Name, new DirectoryInfo(libraryPath), false, 0);
			target.ExtId = legacyLibrary.Identifier;
			target.SyncDate = legacyLibrary.SyncDate;

			target.Settings.ApplyAppearanceForAllWindows = legacyLibrary.ApplyAppearanceForAllWindows;
			target.Settings.ApplyBannerForAllWindows = legacyLibrary.ApplyBannerForAllWindows;
			target.Settings.ApplyWidgetForAllWindows = legacyLibrary.ApplyWidgetForAllWindows;

			target.SyncSettings.CloseAfterSync = legacyLibrary.CloseAfterSync;
			target.SyncSettings.MinimizeOnSync = legacyLibrary.MinimizeOnSync;
			target.SyncSettings.ShowProgress = legacyLibrary.ShowProgressDuringSync;

			target.InactiveLinksSettings.Enable = legacyLibrary.EnableInactiveLinks;
			target.InactiveLinksSettings.ShowBoldWarning = legacyLibrary.InactiveLinksBoldWarning;
			target.InactiveLinksSettings.ShowMessageAtStartup = legacyLibrary.InactiveLinksMessageAtStartup;
			target.InactiveLinksSettings.ReplaceInactiveLinksWithLineBreak = legacyLibrary.ReplaceInactiveLinksWithLineBreak;
			target.InactiveLinksSettings.SendEmail = legacyLibrary.SendEmail;
			target.InactiveLinksSettings.EmailList.AddRange(legacyLibrary.EmailList);

			target.ProgramData.Enable = legacyLibrary.EnableProgramManagerSync;
			target.ProgramData.Path = legacyLibrary.ProgramManagerLocation;

			target.Calendar.Enabled = legacyLibrary.OvernightsCalendar.Enabled;
			target.Calendar.Path = legacyLibrary.OvernightsCalendar.RootFolder?.FullName;
			target.Calendar.CalendarBackColor = legacyLibrary.OvernightsCalendar.CalendarBackColor;
			target.Calendar.CalendarBorderColor = legacyLibrary.OvernightsCalendar.CalendarBorderColor;
			target.Calendar.CalendarHeaderBackColor = legacyLibrary.OvernightsCalendar.CalendarHeaderBackColor;
			target.Calendar.CalendarHeaderForeColor = legacyLibrary.OvernightsCalendar.CalendarHeaderForeColor;
			target.Calendar.DeadLinksForeColor = legacyLibrary.OvernightsCalendar.DeadLinksForeColor;
			target.Calendar.MonthBodyBackColor = legacyLibrary.OvernightsCalendar.MonthBodyBackColor;
			target.Calendar.MonthBodyForeColor = legacyLibrary.OvernightsCalendar.MonthBodyForeColor;
			target.Calendar.MonthHeaderBackColor = legacyLibrary.OvernightsCalendar.MonthHeaderBackColor;
			target.Calendar.MonthHeaderForeColor = legacyLibrary.OvernightsCalendar.MonthHeaderForeColor;
			target.Calendar.SweepBackColor = legacyLibrary.OvernightsCalendar.SweepBackColor;
			target.Calendar.SweepForeColor = legacyLibrary.OvernightsCalendar.SweepForeColor;

			foreach (var previewContainer in legacyLibrary.PreviewContainers)
			{
				var targetPreviewContainer = BasePreviewContainer.Create(Path.Combine(target.Path, previewContainer.OriginalPath), target);
				if (targetPreviewContainer == null) continue;
				targetPreviewContainer.ExtId = Guid.Parse(previewContainer.Identifier);
				targetPreviewContainer.Library = target;
				target.PreviewContainers.AddItem(targetPreviewContainer);
			}

			foreach (var legacyPage in legacyLibrary.Pages)
			{
				var targetPage = WallbinEntity.CreateEntity<LibraryPage>();
				targetPage.Library = target;
				targetPage.ImportLegacyData(legacyPage);
				target.Pages.Add(targetPage);
			}

			foreach (var autoWidget in legacyLibrary.AutoWidgets)
			{
				var targetAutoWidget = new AutoWidget();
				targetAutoWidget.ImportLegacyData(autoWidget);
				target.Settings.AutoWidgets.Add(targetAutoWidget);
			}
		}

		public static void ImportLegacyData(this LibraryPage target, Legacy.Entities.LibraryPage legacy)
		{
			target.ExtId = legacy.Identifier;
			target.Name = legacy.Name;
			target.Order = legacy.Order;
			target.Settings.EnableColumnTitles = legacy.EnableColumnTitles;
			target.Settings.ApplyForAllColumnTitles = legacy.ApplyForAllColumnTitles;

			foreach (var libraryFolder in legacy.Folders)
			{
				var targetFolder = WallbinEntity.CreateEntity<LibraryFolder>();
				targetFolder.Page = target;
				targetFolder.ImportLegacyData(libraryFolder);
				target.Folders.Add(targetFolder);
			}

			if (legacy.EnableColumnTitles)
				foreach (var columnTitle in legacy.ColumnTitles)
				{
					var targetColumnTitle = WallbinEntity.CreateEntity<ColumnTitle>();
					targetColumnTitle.Page = target;
					targetColumnTitle.ImportLegacyData(columnTitle);
					target.ColumnTitles.Add(targetColumnTitle);
				}
		}

		public static void ImportLegacyData(this LibraryFolder target, Legacy.Entities.LibraryFolder legacy)
		{
			target.ExtId = legacy.Identifier;
			target.Name = legacy.Name;
			target.RowOrder = (Int32)legacy.RowOrder;
			target.ColumnOrder = legacy.ColumnOrder;
			target.AddDate = legacy.AddDate;

			target.Settings.BackgroundHeaderColor = legacy.BackgroundHeaderColor;
			target.Settings.BackgroundWindowColor = legacy.BackgroundWindowColor;
			target.Settings.BorderColor = legacy.BorderColor;
			target.Settings.ForeHeaderColor = legacy.ForeHeaderColor;
			target.Settings.ForeWindowColor = legacy.ForeWindowColor;
			target.Settings.HeaderAlignment = (Alignment)(Int32)legacy.HeaderAlignment;
			target.Settings.HeaderFont = (Font)legacy.HeaderFont.Clone();
			target.Settings.WindowFont = (Font)legacy.WindowFont.Clone();

			target.Widget.WidgetType = legacy.EnableWidget ? WidgetType.CustomWidget : WidgetType.NoWidget;
			target.Widget.Image = (Image)legacy.Widget?.Clone();

			if (legacy.BannerProperties != null)
			{
				target.Banner.Enable = legacy.BannerProperties.Enable;
				if (target.Banner.Enable)
				{
					target.Banner.Image = (Image)(legacy.BannerProperties.Image != null ? legacy.BannerProperties.Image.Clone() : null);
					target.Banner.ImageAlignement = (Alignment)(Int32)legacy.BannerProperties.ImageAlignement;
					target.Banner.ShowText = legacy.BannerProperties.ShowText;
					if (target.Banner.ShowText)
					{
						target.Banner.Font = (Font)legacy.BannerProperties.Font.Clone();
						target.Banner.ForeColor = legacy.BannerProperties.ForeColor;
						target.Banner.Text = legacy.BannerProperties.Text;
					}
				}
			}

			foreach (var legacyLink in legacy.Files)
			{
				BaseLibraryLink libraryLink;
				switch (legacyLink.Type)
				{
					case Legacy.Entities.FileTypes.LineBreak:
						libraryLink = WallbinEntity.CreateEntity<LineBreak>();
						break;
					case Legacy.Entities.FileTypes.Url:
						libraryLink = WebLink.Create(legacyLink.OriginalPath);
						break;
					case Legacy.Entities.FileTypes.Network:
						libraryLink = NetworkLink.Create(legacyLink.OriginalPath);
						break;
					case Legacy.Entities.FileTypes.Folder:
						libraryLink = WallbinEntity.CreateEntity<LibraryFolderLink>();
						break;
					default:
						libraryLink = LibraryFileLink.Create(legacyLink.OriginalPath);
						break;
				}
				libraryLink.Folder = target;
				libraryLink.ImportLegacyData(legacyLink);
				target.Links.Add(libraryLink);
			}
			target.Links.OfType<PreviewableLink>().ToList().ForEach(previewableLink => previewableLink.ParentLibrary.GetPreviewContainerBySourcePath(previewableLink.FullPath));
		}

		public static void ImportLegacyData(this ColumnTitle target, Legacy.Entities.ColumnTitle legacy)
		{
			target.ColumnOrder = legacy.ColumnOrder;

			target.Settings.ShowText = legacy.EnableText;
			target.Settings.Text = legacy.Name;
			target.Settings.BackgroundColor = legacy.BackgroundColor;
			target.Settings.ForeColor = legacy.ForeColor;
			target.Settings.HeaderAlignment = (Alignment)(Int32)legacy.HeaderAlignment;
			target.Settings.HeaderFont = (Font)legacy.HeaderFont.Clone();

			target.Widget.WidgetType = legacy.EnableWidget ? WidgetType.CustomWidget : WidgetType.NoWidget;
			target.Widget.Image = (Image)(legacy.Widget != null ? legacy.Widget.Clone() : null);

			if (legacy.BannerProperties != null)
			{
				target.Banner.Enable = legacy.BannerProperties.Enable;
				if (target.Banner.Enable)
				{
					target.Banner.Image = (Image)(legacy.BannerProperties.Image != null ? legacy.BannerProperties.Image.Clone() : null);
					target.Banner.ImageAlignement = (Alignment)(Int32)legacy.BannerProperties.ImageAlignement;
					target.Banner.ShowText = legacy.BannerProperties.ShowText;
					if (target.Banner.ShowText)
					{
						target.Banner.Font = (Font)legacy.BannerProperties.Font.Clone();
						target.Banner.ForeColor = legacy.BannerProperties.ForeColor;
						target.Banner.Text = legacy.BannerProperties.Text;
					}
				}
			}
		}

		public static void ImportLegacyData(this AutoWidget target, Legacy.Entities.AutoWidget legacy)
		{
			target.Extension = legacy.Extension;
			target.Widget = legacy.Widget != null ? (Image)legacy.Widget.Clone() : null;
		}

		public static void ImportLegacyData(this BaseLibraryLink target, Legacy.Entities.LibraryLink legacy)
		{
			target.ExtId = legacy.Identifier;
			target.Name = legacy.Name;
			target.Order = legacy.Order;
			target.AddDate = legacy.AddDate;

			target.Tags.Categories.AddRange(legacy.SearchTags.SearchGroups.Select(legacyGroup =>
			{
				var group = new SearchGroup();
				group.Name = legacyGroup.Name;
				group.Description = legacyGroup.Description;
				group.Tags.AddRange(legacyGroup.Tags.Select(legacyTag => new SearchTag { Name = legacyTag.Name }));
				return group;
			}));
			target.Tags.Keywords.AddRange(legacy.CustomKeywords.Tags.Select(legacyTag => new SearchTag { Name = legacyTag.Name }));
			target.Tags.SuperFilters.AddRange(legacy.SuperFilters.Select(superFilter => superFilter.Name));

			target.Widget.WidgetType = legacy.EnableWidget ? WidgetType.CustomWidget : target.Widget.DefaultWidgetType;
			target.Widget.Image = (Image)(legacy.Widget != null ? legacy.Widget.Clone() : null);

			if (legacy.BannerProperties != null)
			{
				target.Banner.Enable = legacy.BannerProperties.Enable;
				if (target.Banner.Enable)
				{
					target.Banner.Image = (Image)(legacy.BannerProperties.Image != null ? legacy.BannerProperties.Image.Clone() : null);
					target.Banner.ImageAlignement = (Alignment)(Int32)legacy.BannerProperties.ImageAlignement;
					target.Banner.ShowText = legacy.BannerProperties.ShowText;
					if (target.Banner.ShowText)
					{
						target.Banner.Font = (Font)legacy.BannerProperties.Font.Clone();
						target.Banner.ForeColor = legacy.BannerProperties.ForeColor;
						target.Banner.Text = legacy.BannerProperties.Text;
					}
				}
			}

			target.Security.AssignedUsers = legacy.ExtendedProperties.AssignedUsers;
			target.Security.DeniedUsers = legacy.ExtendedProperties.DeniedUsers;
			target.Security.IsForbidden = legacy.ExtendedProperties.IsForbidden;
			target.Security.IsRestricted = legacy.ExtendedProperties.IsRestricted;
			target.Security.NoShare = legacy.ExtendedProperties.NoShare;

			if (target is LineBreak && legacy.LineBreakProperties != null)
			{
				target.Settings.Note = legacy.LineBreakProperties.Note;
				if (legacy.ExtendedProperties.Font != null)
					target.Settings.Font = (Font)legacy.LineBreakProperties.Font.Clone();
				target.Settings.ForeColor = legacy.LineBreakProperties.ForeColor;
			}
			if (target is LibraryObjectLink)
			{
				((LibraryObjectLink)target).RelativePath = legacy.RelativePath.StartsWith(Path.DirectorySeparatorChar.ToString()) ? legacy.RelativePath.Substring(1) : legacy.RelativePath;
				target.Settings.Note = !String.IsNullOrEmpty(legacy.ExtendedProperties.Note) ? legacy.ExtendedProperties.Note.Replace("-", String.Empty).Trim() : String.Empty;
				if (legacy.ExtendedProperties.Font != null)
					target.Settings.Font = (Font)legacy.ExtendedProperties.Font.Clone();
				if (legacy.ExtendedProperties.IsSpecialFormat)
					target.Settings.ForeColor = legacy.ExtendedProperties.ForeColor;
				((LibraryObjectLinkSettings)target.Settings).HoverNote = legacy.ExtendedProperties.HoverNote;
				if (legacy.ExtendedProperties.IsBold)
					((LibraryObjectLinkSettings)target.Settings).RegularFontStyle = FontStyle.Bold;
				((LibraryObjectLinkSettings)target.Settings).IsSpecialFormat = legacy.ExtendedProperties.IsSpecialFormat;
				((LibraryObjectLink)target).ExpirationSettings.Enable = legacy.ExpirationDateOptions.EnableExpirationDate;
				((LibraryObjectLink)target).ExpirationSettings.ExpirationDate = legacy.ExpirationDateOptions.ExpirationDate;
				((LibraryObjectLink)target).ExpirationSettings.MarkWhenExpired = legacy.ExpirationDateOptions.LabelLinkWhenExpired;
				((LibraryObjectLink)target).ExpirationSettings.SendEmailOnSync = legacy.ExpirationDateOptions.SendEmailWhenSync;
			}
			if (target is WebLink)
			{
				((WebLinkSettings)target.Settings).ForcePreview = legacy.ExtendedProperties.ForcePreview;
			}
			if (target is LibraryFileLink)
			{
				((LibraryFileLink)target).DataSourceId = !legacy.RootId.Equals(Guid.Empty) ? legacy.RootId : target.ParentLibrary.DataSourceId;
				((LibraryFileLink)target).IsDead = legacy.IsDead;
			}
			if (target is DocumentLink)
			{
				((DocumentLinkSettings)target.Settings).GenerateContentText = legacy.ExtendedProperties.GenerateContentText;
				((DocumentLinkSettings)target.Settings).GeneratePreviewImages = legacy.ExtendedProperties.GeneratePreviewImages;
			}
			if (target is ExcelLink)
			{
				((ExcelLinkSettings)target.Settings).GenerateContentText = legacy.ExtendedProperties.GenerateContentText;
			}
			if (target is VideoLink)
			{
				((VideoLinkSettings)target.Settings).ForcePreview = legacy.ExtendedProperties.ForcePreview;
			}
			if (target is PowerPointLink && legacy.PresentationProperties != null)
			{
				if (legacy.PreviewContainer != null)
					((PowerPointLinkSettings)target.Settings).Id = Guid.Parse(legacy.PreviewContainer.Identifier);
				((PowerPointLinkSettings)target.Settings).Width = legacy.PresentationProperties.Width;
				((PowerPointLinkSettings)target.Settings).Height = legacy.PresentationProperties.Height;
			}
			if (target is LibraryFolderLink && legacy is Legacy.Entities.LibraryFolderLink)
			{
				foreach (var legacyLink in ((Legacy.Entities.LibraryFolderLink)legacy).FolderContent)
				{
					LibraryFileLink libraryLink;
					switch (legacyLink.Type)
					{
						case Legacy.Entities.FileTypes.Folder:
							libraryLink = WallbinEntity.CreateEntity<LibraryFolderLink>();
							break;
						default:
							libraryLink = LibraryFileLink.Create(legacyLink.OriginalPath);
							break;
					}
					libraryLink.FolderLink = ((LibraryFolderLink)target);
					libraryLink.ImportLegacyData(legacyLink);
					((LibraryFolderLink)target).Links.Add(libraryLink);
				}
			}
		}
	}
}
