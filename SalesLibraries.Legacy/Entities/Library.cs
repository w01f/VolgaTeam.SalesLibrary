using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class Library
	{
		private RootFolder _rootFolder;

		public List<LibraryLink> DeadLinks { get; private set; }
		public List<LibraryLink> ExpiredLinks { get; private set; }
		public OvernightsCalendar OvernightsCalendar { get; set; }
		public IPadManager IPadManager { get; private set; }

		public Guid Identifier { get; set; }
		public string Name { get; set; }
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
		public bool FullSync { get; set; }
		public bool VideoConversionWarning { get; set; }
		public bool EnableInactiveLinks { get; set; }
		public bool InactiveLinksBoldWarning { get; set; }
		public bool ReplaceInactiveLinksWithLineBreak { get; set; }
		public bool InactiveLinksMessageAtStartup { get; set; }
		public bool SendEmail { get; set; }

		public bool IsConfigured { get; set; }

		public List<RootFolder> ExtraFolders { get; private set; }
		public List<LibraryPage> Pages { get; private set; }
		public List<string> EmailList { get; private set; }
		public List<AutoWidget> AutoWidgets { get; private set; }
		public List<UniversalPreviewContainer> PreviewContainers { get; private set; }

		public RootFolder RootFolder
		{
			get
			{
				if (_rootFolder != null) return _rootFolder;
				_rootFolder = new RootFolder(this);
				_rootFolder.RootId = Guid.Empty;
				_rootFolder.Folder = Folder;
				return _rootFolder;
			}
		}

		public RootFolder GetRootFolder(Guid folderId)
		{
			var folder = ExtraFolders.FirstOrDefault(x => x.RootId.Equals(folderId));
			return folder ?? RootFolder;
		}

		#region Program Manager Settings
		public bool EnableProgramManagerSync { get; set; }
		public string ProgramManagerLocation { get; set; }
		#endregion

		public Library(string name, DirectoryInfo folder, bool useDirectAccess, int directAccessFileAgeLimit)
		{
			Identifier = Guid.NewGuid();
			Folder = folder;
			UseDirectAccess = useDirectAccess;
			DirectAccessFileBottomDate = directAccessFileAgeLimit > 0 ? DateTime.Now.AddDays(-directAccessFileAgeLimit) : DateTime.MinValue;
			Name = name;
			IsConfigured = false;
			ExtraFolders = new List<RootFolder>();
			Pages = new List<LibraryPage>();
			EmailList = new List<string>();
			DeadLinks = new List<LibraryLink>();
			ExpiredLinks = new List<LibraryLink>();
			AutoWidgets = new List<AutoWidget>();
			OvernightsCalendar = new OvernightsCalendar(this);
			IPadManager = new IPadManager();
			PreviewContainers = new List<UniversalPreviewContainer>();
			Load();
		}

		private void Load()
		{
			BrandingText = string.Empty;
			SyncDate = DateTime.Now;
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
			ExtraFolders.Clear();

			string file = Path.Combine(Folder.FullName, Constants.StorageFileName);
			if (File.Exists(file))
			{
				var document = new XmlDocument();
				document.Load(file);

				var node = document.SelectSingleNode(@"/Library/Name");
				if (node != null)
					Name = node.InnerText;
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
				node = document.SelectSingleNode(@"/Library/SyncDate");
				if (node != null)
				{
					DateTime tempDate;
					if (DateTime.TryParse(node.InnerText, out tempDate))
						SyncDate = tempDate;
				}
				node = document.SelectSingleNode(@"/Library/ApplyAppearanceForAllWindows");
				bool tempBool;
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
				node = document.SelectSingleNode(@"/Library/FullSync");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						FullSync = tempBool;
				node = document.SelectSingleNode(@"/Library/VideoConversionWarning");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						VideoConversionWarning = tempBool;
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

				node = document.SelectSingleNode(@"/Library/PreviewContainers");
				if (node != null)
					foreach (XmlNode childNode in node.ChildNodes)
					{
						var previewContainer = new UniversalPreviewContainer();
						previewContainer.Deserialize(childNode);
						if (!PreviewContainers.Any(x => x.Identifier.Equals(previewContainer.Identifier)))
							PreviewContainers.Add(previewContainer);
					}

				node = document.SelectSingleNode(@"/Library/Pages");
				if (node != null)
					foreach (XmlNode childNode in node.ChildNodes)
					{
						var page = new LibraryPage(this);
						page.Deserialize(childNode);
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
					OvernightsCalendar.Deserialize(node);

				node = document.SelectSingleNode(@"/Library/IPadContentManager");
				if (node != null)
					IPadManager.Deserialize(node);
				else
				{
					node = document.SelectSingleNode(@"/Library/IPadManager");
					if (node != null)
						IPadManager.Deserialize(node);
				}

				#region Program Manager Settings
				node = document.SelectSingleNode(@"/Library/EnableProgramManagerSync");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableProgramManagerSync = tempBool;

				node = document.SelectSingleNode(@"/Library/ProgramManagerLocation");
				if (node != null)
					ProgramManagerLocation = node.InnerText;
				#endregion

				IsConfigured = true;
			}
			if (Pages.Count == 0)
				Pages.Add(new LibraryPage(this));
		}
	}
}