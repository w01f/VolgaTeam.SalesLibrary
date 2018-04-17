using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class Library : WallbinEntity, IDataSource
	{
		#region Persistent Properties

		public DateTime? SyncDate { get; set; }
		public string SettingsEncoded { get; set; }
		public string SyncSettingsEncoded { get; set; }
		public string CalendarEncoded { get; set; }
		public virtual ICollection<LibraryPage> Pages { get; set; }
		public virtual ICollection<BasePreviewContainer> PreviewContainers { get; set; }
		public virtual ICollection<LinkBundle> LinkBundles { get; set; }
		public virtual ICollection<LinkActionLog> LinkActions { get; set; }

		#endregion

		#region Nonpersistent Properties

		private LibrarySettings _settings;

		[NotMapped, JsonIgnore]
		public LibrarySettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<LibrarySettings>(this, SettingsEncoded));
			set => _settings = value;
		}

		private SyncSettings _syncSettings;

		[NotMapped, JsonIgnore]
		public SyncSettings SyncSettings
		{
			get => _syncSettings ?? (_syncSettings = SettingsContainer.CreateInstance<SyncSettings>(this, SyncSettingsEncoded));
			set => _syncSettings = value;
		}

		private CalendarSettings _calendar;

		[NotMapped, JsonIgnore]
		public CalendarSettings Calendar
		{
			get => _calendar ?? (_calendar = SettingsContainer.CreateInstance<CalendarSettings>(this, CalendarEncoded));
			set => _calendar = value;
		}

		[NotMapped, JsonIgnore]
		public LibraryContext Context { get; set; }

		[NotMapped, JsonIgnore]
		public Guid DataSourceId => ExtId;

		[NotMapped]
		public string Path { get; set; }

		[NotMapped]
		public string Name { get; set; }

		[NotMapped, JsonIgnore]
		public int Order => -1;

		#endregion

		public Library()
		{
			Pages = new List<LibraryPage>();
			PreviewContainers = new List<BasePreviewContainer>();
			LinkBundles = new List<LinkBundle>();
			LinkActions = new List<LinkActionLog>();
		}

		public override void BeforeSave()
		{
			if (NeedToSave)
			{
				SettingsEncoded = Settings.Serialize();
				SyncSettingsEncoded = SyncSettings.Serialize();
				CalendarEncoded = Calendar.Serialize();
			}
			foreach (var libraryPage in Pages)
				libraryPage.BeforeSave();
			foreach (var previewContainer in PreviewContainers)
				previewContainer.BeforeSave();
			foreach (var linkBundle in LinkBundles)
				linkBundle.BeforeSave();
			foreach (var linkActionLog in LinkActions)
				linkActionLog.BeforeSave();
			base.BeforeSave();
		}

		public override void AfterSave()
		{
			Settings = null;
			SyncSettings = null;
			Calendar = null;
		}

		public override void Save(LibraryContext context, IDbEntity<LibraryContext> current, bool withCommit = true)
		{
			var currentLibrary = (Library)current;
			Pages.Save(currentLibrary.Pages, context);
			Pages.Sort();
			PreviewContainers.Save(currentLibrary.PreviewContainers, context);
			LinkBundles.Save(currentLibrary.LinkBundles, context);
			LinkBundles.Sort();
			base.Save(context, current, withCommit);
		}

		public override void ResetParent() { }

		public string GetRootPath()
		{
			return Path;
		}

		#region Page Processing

		public void AddPage()
		{
			var page = CreateEntity<LibraryPage>(p =>
			{
				p.Library = this;
				p.Order = Pages.Count;
			});
			Pages.AddItem(page);
			page.Name = String.Format("Page {0}", page.Order + 1);
		}

		#endregion

		#region Data Sorces Processing

		public IEnumerable<IDataSource> GetDataSources()
		{
			return new[] { (IDataSource)this };
		}

		#endregion

		#region Preview Container Processing

		public IEnumerable<IPreviewableLink> GetPreviewableLinksBySourcePath(string sourcePath, bool onlyTopLevel = false)
		{
			return Pages
				.SelectMany(p => onlyTopLevel ? p.TopLevelLinks : p.AllGroupLinks)
				.OfType<IPreviewableLink>()
				.Where(link => String.Equals(link.PreviewSourcePath, sourcePath, StringComparison.InvariantCultureIgnoreCase));
		}

		public BasePreviewContainer GetPreviewContainerBySourcePath(string sourcePath)
		{
			var previewContainer = PreviewContainers
				.FirstOrDefault(container =>
					String.Equals(container.SourcePath, sourcePath, StringComparison.InvariantCultureIgnoreCase));
			if (previewContainer == null)
			{
				previewContainer = BasePreviewContainer.Create(sourcePath, this);
				previewContainer.Library = this;
				PreviewContainers.AddItem(previewContainer);
				previewContainer.InitDefaultSettings();
				MarkAsModified();
			}
			return previewContainer;
		}

		#endregion

		#region Links Processing

		public TLink GetLinkById<TLink>(Guid extId, bool onlyTopLevel = false) where TLink : BaseLibraryLink
		{
			return Pages
				.SelectMany(p => onlyTopLevel ? p.TopLevelLinks : p.AllGroupLinks)
				.OfType<TLink>()
				.FirstOrDefault(link => link.ExtId == extId);
		}

		public BaseLibraryLink GetLinkById(Guid extId, bool onlyTopLevel = false)
		{
			return Pages
				.SelectMany(p => onlyTopLevel ? p.TopLevelLinks : p.AllGroupLinks)
				.FirstOrDefault(link => link.ExtId == extId);
		}

		public void ResetLinksToDefault(IList<LinkSettingsGroupType> groupsForReset, Func<BaseLibraryLink, bool> linksFilter)
		{
			var linksToProcess = Pages.SelectMany(p => p.AllGroupLinks).Where(linksFilter).ToList();
			foreach (var libraryLink in linksToProcess)
				libraryLink.ResetToDefault(groupsForReset);
		}

		#endregion

		#region Link Bundles processing

		public void AddLinkBundle(string name)
		{
			var linkBundle = CreateEntity<LinkBundle>(bundle =>
			{
				bundle.Library = this;
				bundle.Order = LinkBundles.Count;
			});
			LinkBundles.AddItem(linkBundle);
			linkBundle.Name = name;
			MarkAsModified();
		}

		#endregion

		#region Link Action Log Processing

		public void LogLinklAction(LinkActionType actionType, BaseLibraryLink link)
		{
			var linkAction = new LinkActionLog();
			linkAction.Library = this;
			linkAction.ActionType = actionType;
			linkAction.Order = LinkActions.Count;
			linkAction.Settings.ExtId = link.ExtId;
			linkAction.Settings.Name = link.LinkInfoDisplayName;
			if (link is LibraryObjectLink)
				linkAction.Settings.Path = ((LibraryObjectLink)link).FullPath;
			LinkActions.AddItem(linkAction);
		}

		public void ClearLinkActionLog()
		{
			var actionLog = LinkActions.ToList();
			foreach (var linkActionLog in actionLog)
			{
				linkActionLog.Delete(Context);
				LinkActions.RemoveItem(linkActionLog);
				linkActionLog.ResetParent();
			}
		}
		#endregion

		public class LibrarySettings : SettingsContainer
		{
			public bool ApplyAppearanceForAllWindows { get; set; }
			public bool ApplyWidgetForAllWindows { get; set; }
			public bool ApplyWidgetColorForAllWindows { get; set; }
			public bool ApplyBannerForAllWindows { get; set; }
			public bool ApplyConvertSettingsForAllVideo { get; set; }
			public bool UserCrfForVideoConvert { get; set; }

			public List<AutoWidget> AutoWidgets { get; private set; }

			public VideoConvertSettings VideoConvertSettings { get; private set; }
			public FontSettings FontSettings { get; private set; }

			public LibrarySettings()
			{
				AutoWidgets = new List<AutoWidget>();
			}

			protected override void AfterConstruction()
			{
				base.AfterConstruction();
				VideoConvertSettings = new VideoConvertSettings();
				FontSettings = new FontSettings();
			}

			public override void AfterCreate()
			{
				base.AfterCreate();

				if (VideoConvertSettings == null)
					VideoConvertSettings = new VideoConvertSettings();
				VideoConvertSettings.SettingsContainer = this;

				if (FontSettings == null)
					FontSettings = new FontSettings();
				FontSettings.SettingsContainer = this;
			}
		}
	}
}
