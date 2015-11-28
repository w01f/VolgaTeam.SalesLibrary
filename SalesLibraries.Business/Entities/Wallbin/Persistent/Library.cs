using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class Library : WallbinEntity, IDataSource
	{
		#region Persistent Properties
		[StringLength(128)]
		public string BrandingText { get; set; }
		public DateTime? SyncDate { get; set; }
		public string SettingsEncoded { get; set; }
		public string SyncSettingsEncoded { get; set; }
		public string InactiveLinksEncoded { get; set; }
		public string ProgramDataEncoded { get; set; }
		public string CalendarEncoded { get; set; }
		public virtual ICollection<LibraryPage> Pages { get; set; }
		public virtual ICollection<AdditionalDataSource> DataSources { get; set; }
		public virtual ICollection<BasePreviewContainer> PreviewContainers { get; set; }
		#endregion

		#region Nonpersistent Properties
		private LibrarySettings _settings;
		[NotMapped, JsonIgnore]
		public LibrarySettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LibrarySettings>(this, SettingsEncoded)); }
			set { _settings = value; }
		}

		private SyncSettings _syncSettings;
		[NotMapped, JsonIgnore]
		public SyncSettings SyncSettings
		{
			get { return _syncSettings ?? (_syncSettings = SettingsContainer.CreateInstance<SyncSettings>(this, SyncSettingsEncoded)); }
			set { _syncSettings = value; }
		}

		private InactiveLinksSettings _inactiveLinksSettings;
		[NotMapped, JsonIgnore]
		public InactiveLinksSettings InactiveLinksSettings
		{
			get { return _inactiveLinksSettings ?? (_inactiveLinksSettings = SettingsContainer.CreateInstance<InactiveLinksSettings>(this, InactiveLinksEncoded)); }
			set { _inactiveLinksSettings = value; }
		}

		private ProgramDataSettings _programData;
		[NotMapped, JsonIgnore]
		public ProgramDataSettings ProgramData
		{
			get { return _programData ?? (_programData = SettingsContainer.CreateInstance<ProgramDataSettings>(this, ProgramDataEncoded)); }
			set { _programData = value; }
		}

		private CalendarSettings _calendar;
		[NotMapped, JsonIgnore]
		public CalendarSettings Calendar
		{
			get { return _calendar ?? (_calendar = SettingsContainer.CreateInstance<CalendarSettings>(this, CalendarEncoded)); }
			set { _calendar = value; }
		}

		[NotMapped, JsonIgnore]
		public LibraryContext Context { get; set; }

		[NotMapped, JsonIgnore]
		public Guid DataSourceId
		{
			get { return ExtId; }
		}

		[NotMapped]
		public string Path { get; set; }

		[NotMapped]
		public string Name { get; set; }

		[NotMapped, JsonIgnore]
		public int Order
		{
			get { return -1; }
		}
		#endregion

		public Library()
		{
			Pages = new List<LibraryPage>();
			DataSources = new List<AdditionalDataSource>();
			PreviewContainers = new Collection<BasePreviewContainer>();
		}

		public override void BeforeSave()
		{
			SettingsEncoded = Settings.Serialize();
			SyncSettingsEncoded = SyncSettings.Serialize();
			InactiveLinksEncoded = InactiveLinksSettings.Serialize();
			ProgramDataEncoded = ProgramData.Serialize();
			CalendarEncoded = Calendar.Serialize();
			foreach (var libraryPage in Pages)
				libraryPage.BeforeSave();
			foreach (var dataSource in DataSources)
				dataSource.BeforeSave();
			foreach (var previewContainer in PreviewContainers)
				previewContainer.BeforeSave();
		}

		public override void AfterSave()
		{
			Settings = null;
			SyncSettings = null;
			InactiveLinksSettings = null;
			ProgramData = null;
			Calendar = null;
		}

		public override void Save(LibraryContext context, IDbEntity<LibraryContext> current, bool withCommit = true)
		{
			var currentLibrary = (Library)current;
			Pages.Save(currentLibrary.Pages, context);
			Pages.Sort();
			DataSources.Save(currentLibrary.DataSources, context);
			DataSources.Sort();
			PreviewContainers.Save(currentLibrary.PreviewContainers, context);
			DataSources.Sort();
			base.Save(context, current, withCommit);
		}

		public override void ResetParent() { }

		public string GetFilePath()
		{
			return Path;
		}

		#region Page Processing
		public void AddPage()
		{
			var page = new LibraryPage
			{
				Library = this,
				Order = Pages.Count
			};
			Pages.AddItem(page);
			page.Name = String.Format("Page {0}", page.Order + 1);
		}
		#endregion

		#region Data Sorces Processing
		public IEnumerable<IDataSource> GetDataSources()
		{
			return new[] { (IDataSource)this }.Union(DataSources);
		}

		public void AddDataSource()
		{
			var additionalDataSource = new AdditionalDataSource()
			{
				Library = this,
				Order = DataSources.Count
			};
			DataSources.Add(additionalDataSource);
		}
		#endregion

		#region Preview Container Processing
		public IEnumerable<PreviewableLink> GetPreviewableLinksBySourcePath(string sourcePath, bool onlyTopLevel = false)
		{
			return Pages
				.SelectMany(p => onlyTopLevel ? p.TopLevelLinks : p.AllLinks)
				.OfType<PreviewableLink>()
				.Where(link => String.Equals(link.FullPath, sourcePath, StringComparison.InvariantCultureIgnoreCase));
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
				MarkAsModified();
			}
			return previewContainer;
		}
		#endregion

		#region Links Processing
		public IEnumerable<TLink> GetLinkById<TLink>(Guid extId, bool onlyTopLevel = false) where TLink : BaseLibraryLink
		{
			return Pages
				.SelectMany(p => onlyTopLevel ? p.TopLevelLinks : p.AllLinks)
				.OfType<TLink>()
				.Where(link => link.ExtId == extId)
				.ToList();
		}

		public IEnumerable<BaseLibraryLink> GetLinkById(Guid extId, bool onlyTopLevel = false)
		{
			return Pages
				.SelectMany(p => onlyTopLevel ? p.TopLevelLinks : p.AllLinks)
				.Where(link => link.ExtId == extId)
				.ToList();
		}
		#endregion

		public class LibrarySettings : SettingsContainer
		{
			public bool ApplyAppearanceForAllWindows { get; set; }
			public bool ApplyWidgetForAllWindows { get; set; }
			public bool ApplyBannerForAllWindows { get; set; }

			public List<AutoWidget> AutoWidgets { get; private set; }

			public LibrarySettings()
			{
				AutoWidgets = new List<AutoWidget>();
			}
		}
	}
}
