using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OvernightsCalendarViewer.BusinessClasses
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
			Load();
		}

		public LibraryPackage Parent { get; private set; }
		public bool SyncLinkedFiles { get; set; }
		public OvernightsCalendar OvernightsCalendar { get; set; }
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

		public RootFolder GetRootFolder(Guid folderId)
		{
			var folder = ExtraFolders.FirstOrDefault(x => x.RootId.Equals(folderId));
			if (folder != null)
				return folder;
			return RootFolder;
		}

		public ILibraryLink GetLinkInstance(LibraryFolder parentFolder, XmlNode data)
		{
			throw new NotImplementedException();
		}

		private void Load()
		{
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
				LibraryManager.Instance.OldFormatDetected = false;
				var node = document.SelectSingleNode(@"/Library/Name");
				if (node != null)
					_name = node.InnerText;
				node = document.SelectSingleNode(@"/Library/Identifier");
				if (node != null)
				{
					Guid tempGuid;
					if (Guid.TryParse(node.InnerText, out tempGuid))
						Identifier = tempGuid;
				}
				node = document.SelectSingleNode(@"/Library/BrandingText");
				if (node != null)
					BrandingText = node.InnerText;

				node = document.SelectSingleNode(@"/Library/OvernightsCalendar");
				if (node != null)
				{
					OvernightsCalendar.Deserialize(node);
					OvernightsCalendar.RootFolder = new DirectoryInfo(Path.Combine(Folder.FullName, Constants.OvernightsCalendarRootFolderName));
				}
			}
			IsConfigured = OvernightsCalendar.Enabled && OvernightsCalendar.RootFolder.Exists;
		}

		#region IPreviewStorage Members

		public List<IPreviewContainer> PreviewContainers { get; private set; }
		public IPreviewContainer GetPreviewContainer(string originalPath)
		{
			throw new NotImplementedException();
		}

		public IPreviewGenerator GetPreviewGenerator(IPreviewContainer previewContainer)
		{
			throw new NotImplementedException();
		}
		
		public void UpdatePreviewableObject(string originalPath, DateTime lastChanged)
		{
			throw new NotImplementedException();
		}

		public void UpdatePreviewContainers()
		{
			throw new NotImplementedException();
		}

		public string StoragePath
		{
			get { return Folder.FullName; }
		}

		#endregion
	}
}