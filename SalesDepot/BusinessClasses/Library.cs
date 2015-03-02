using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.BusinessClasses
{
	public class Library : ILibrary
	{
		private string _name;
		private RootFolder _rootFolder;

		public Library(LibraryPackage parent, string name, DirectoryInfo folder)
		{
			Parent = parent;
			Identifier = Guid.NewGuid();
			Folder = folder;
			_name = name;
			IsConfigured = false;
			ExtraFolders = new List<RootFolder>();
			Pages = new List<LibraryPage>();
			EmailList = new List<string>();
			AutoWidgets = new List<AutoWidget>();
			OvernightsCalendar = new OvernightsCalendar(this);
			ProgramManager = new ProgramScheduleManager(this);
			Load();
		}

		public LibraryPackage Parent { get; private set; }
		public bool SyncLinkedFiles { get; set; }
		public OvernightsCalendar OvernightsCalendar { get; set; }
		public ProgramScheduleManager ProgramManager { get; set; }
		public Guid Identifier { get; set; }
		public DirectoryInfo Folder { get; set; }
		public bool UseDirectAccess { get; set; }
		public DateTime DirectAccessFileBottomDate { get; set; }
		public string BrandingText { get; set; }
		public DateTime SyncDate { get; set; }

		public bool ApplyAppearanceForAllWindows { get; set; }
		public bool ApplyWidgetForAllWindows { get; set; }
		public bool ApplyBannerForAllWindows { get; set; }
		public bool MinimizeOnSync { get; set; }
		public bool CloseAfterSync { get; set; }
		public bool ShowProgressDuringSync { get; set; }
		public bool EnableInactiveLinks { get; set; }
		public bool InactiveLinksBoldWarning { get; set; }
		public bool ReplaceInactiveLinksWithLineBreak { get; set; }
		public bool InactiveLinksMessageAtStartup { get; set; }
		public bool SendEmail { get; set; }

		public bool IsConfigured { get; set; }

		public List<RootFolder> ExtraFolders { get; private set; }
		public List<LibraryPage> Pages { get; set; }
		public List<string> EmailList { get; set; }
		public List<AutoWidget> AutoWidgets { get; set; }

		public string Name
		{
			get
			{
				if (_name.Equals(Constants.WholeDriveFilesStorage))
					return Parent.Name;
				return _name;
			}
		}

		public RootFolder RootFolder
		{
			get
			{
				if (_rootFolder == null)
				{
					_rootFolder = new RootFolder(this);
					_rootFolder.RootId = Guid.Empty;
					_rootFolder.Folder = Folder;
				}
				return _rootFolder;
			}
		}

		public ILibraryLink GetLinkInstance(LibraryFolder parentFolder, XmlNode data)
		{
			ILibraryLink libraryFile = null;
			XmlNode typeNode = data.SelectSingleNode("Type");
			int tempInt;
			if (typeNode != null && int.TryParse(typeNode.InnerText, out tempInt))
			{
				var type = (FileTypes) tempInt;
				if (type == FileTypes.Folder)
					libraryFile = new LibraryFolderLink(parentFolder);
			}
			return libraryFile ?? new LibraryLink(parentFolder);
		}

		public RootFolder GetRootFolder(Guid folderId)
		{
			RootFolder folder = ExtraFolders.Where(x => x.RootId.Equals(folderId)).FirstOrDefault();
			if (folder != null)
				return folder;
			return RootFolder;
		}

		private void Load()
		{
			DateTime tempDate = DateTime.Now;
			bool tempBool = false;
			Guid tempGuid;

			BrandingText = string.Empty;
			UseDirectAccess = false;
			SyncDate = DateTime.Now;
			SyncLinkedFiles = true;
			MinimizeOnSync = true;
			CloseAfterSync = true;
			ShowProgressDuringSync = true;
			EnableInactiveLinks = true;
			InactiveLinksBoldWarning = true;
			ReplaceInactiveLinksWithLineBreak = false;
			InactiveLinksMessageAtStartup = true;
			SendEmail = false;
			Pages.Clear();
			EmailList.Clear();
			AutoWidgets.Clear();

			string file = Path.Combine(Folder.FullName, Constants.StorageFileName);
			if (File.Exists(file))
			{
				var document = new XmlDocument();
				document.Load(file);
				XmlNode node = document.SelectSingleNode(@"/Cache");
				if (node != null)
				{
					var oldFormatLibrary = new OldFormatLibrary(_name, Folder);
					document.LoadXml(oldFormatLibrary.SerializeInNewFormat());
					LibraryManager.Instance.OldFormatDetected = true;
				}
				else
					LibraryManager.Instance.OldFormatDetected = false;
				node = document.SelectSingleNode(@"/Library/Name");
				if (node != null)
					_name = node.InnerText;
				node = document.SelectSingleNode(@"/Library/Identifier");
				if (node != null)
					if (Guid.TryParse(node.InnerText, out tempGuid))
						Identifier = tempGuid;
				node = document.SelectSingleNode(@"/Library/UseDirectAccess");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						UseDirectAccess = tempBool;
				if (UseDirectAccess)
				{
					node = document.SelectSingleNode(@"/Library/RootFolder");
					if (node != null)
						Folder = new DirectoryInfo(node.InnerText);
					node = document.SelectSingleNode(@"/Library/DirectAccessFileBottomDate");
					if (node != null)
						if (DateTime.TryParse(node.InnerText, out tempDate))
							DirectAccessFileBottomDate = tempDate;
				}
				node = document.SelectSingleNode(@"/Library/BrandingText");
				if (node != null)
					BrandingText = node.InnerText;
				node = document.SelectSingleNode(@"/Library/SyncDate");
				if (node != null)
					if (DateTime.TryParse(node.InnerText, out tempDate))
						SyncDate = tempDate;
				node = document.SelectSingleNode(@"/Library/SyncLinkedFiles");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SyncLinkedFiles = tempBool;
				node = document.SelectSingleNode(@"/Library/ApplyAppearanceForAllWindows");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ApplyAppearanceForAllWindows = tempBool;
				node = document.SelectSingleNode(@"/Library/ApplyWidgetForAllWindows");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ApplyWidgetForAllWindows = tempBool;
				node = document.SelectSingleNode(@"/Library/ApplyBannerForAllWindows");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ApplyBannerForAllWindows = tempBool;
				node = document.SelectSingleNode(@"/Library/MinimizeOnSync");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						MinimizeOnSync = tempBool;
				node = document.SelectSingleNode(@"/Library/CloseAfterSync");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						CloseAfterSync = tempBool;
				node = document.SelectSingleNode(@"/Library/ShowProgressDuringSync");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowProgressDuringSync = tempBool;
				node = document.SelectSingleNode(@"/Library/EnableInactiveLinks");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableInactiveLinks = tempBool;
				node = document.SelectSingleNode(@"/Library/InactiveLinksBoldWarning");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						InactiveLinksBoldWarning = tempBool;
				node = document.SelectSingleNode(@"/Library/ReplaceInactiveLinksWithLineBreak");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ReplaceInactiveLinksWithLineBreak = tempBool;
				node = document.SelectSingleNode(@"/Library/InactiveLinksMessageAtStartup");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						InactiveLinksMessageAtStartup = tempBool;
				node = document.SelectSingleNode(@"/Library/SendEmail");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SendEmail = tempBool;

				node = document.SelectSingleNode(@"/Library/ExtraRoots");
				if (node != null)
					foreach (XmlNode childNode in node.ChildNodes)
					{
						var folder = new RootFolder(this);
						folder.Deserialize(childNode);
						ExtraFolders.Add(folder);
					}
				node = document.SelectSingleNode(@"/Library/Pages");
				if (node != null)
					foreach (XmlNode childNode in node.ChildNodes)
					{
						var page = new LibraryPage(this);
						page.Deserialize(childNode);
						if (PermissionsManager.Instance.GetAvailablePages(Name).Contains(page.Name.Trim().ToLower()) || !PermissionsManager.Instance.Configured)
							Pages.Add(page);
					}
				node = document.SelectSingleNode(@"/Library/EmailList");
				if (node != null)
					foreach (XmlNode childNode in node.ChildNodes)
					{
						if (childNode.Name.Equals("Email"))
							EmailList.Add(childNode.InnerText);
					}
				node = document.SelectSingleNode(@"/Library/AutoWidgets");
				if (node != null)
					foreach (XmlNode childNode in node.ChildNodes)
					{
						var autoWidget = new AutoWidget();
						autoWidget.Deserialize(childNode);
						AutoWidgets.Add(autoWidget);
					}

				node = document.SelectSingleNode(@"/Library/OvernightsCalendar");
				if (node != null)
				{
					OvernightsCalendar.Deserialize(node);
					OvernightsCalendar.RootFolder = new DirectoryInfo(Path.Combine(Folder.FullName, Constants.OvernightsCalendarRootFolderName));
				}

				if (UseDirectAccess && !Folder.Exists)
					IsConfigured = false;
				else
					IsConfigured = true;
			}
			if (Pages.Count == 0)
				IsConfigured = false;
		}

		public ILibraryLink[] SearchByTags(LibraryFileSearchTags searchCriteria)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (LibraryPage page in Pages)
				searchFiles.AddRange(page.SearchByTags(searchCriteria));
			return searchFiles.ToArray();
		}

		public ILibraryLink[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (LibraryPage page in Pages)
				searchFiles.AddRange(page.SearchByName(template, fullMatchOnly, type));
			return searchFiles.ToArray();
		}

		public ILibraryLink[] SearchByDate(DateTime startDate, DateTime endDate)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (LibraryPage page in Pages)
				searchFiles.AddRange(page.SearchByDate(startDate, endDate));
			return searchFiles.ToArray();
		}

		#region IPreviewStorage Members

		public List<IPreviewContainer> PreviewContainers { get; private set; }

		public string StoragePath
		{
			get { return Folder.FullName; }
		}

		public IPreviewContainer GetPreviewContainer(string originalPath)
		{
			IPreviewContainer previewContainer = PreviewContainers.FirstOrDefault(x => x.OriginalPath.Equals(originalPath));
			if (previewContainer == null)
			{
				previewContainer = UniversalPreviewContainer.CreateInstance(this, originalPath);
				PreviewContainers.Add(previewContainer);
			}
			return previewContainer;
		}

		public IPreviewGenerator GetPreviewGenerator(IPreviewContainer previewContainer)
		{
			IPreviewGenerator previewGenerator = null;
			return previewGenerator;
		}

		public void UpdatePreviewableObject(string originalPath, DateTime lastChanged)
		{
			throw new NotImplementedException();
		}

		public void DeletePreviewableObject(string originalPath)
		{
			throw new NotImplementedException();
		}

		public void UpdatePreviewContainers()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}