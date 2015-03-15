using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using FileManager;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class Library : ILibrary
	{
		private RootFolder _rootFolder;

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
			DeadLinks = new List<ILibraryLink>();
			ExpiredLinks = new List<ILibraryLink>();
			AutoWidgets = new List<AutoWidget>();
			PreviewContainers = new List<IPreviewContainer>();

			OvernightsCalendar = new OvernightsCalendar(this);
			IPadManager = new IPadManager(this, SettingsManager.Instance.WebServiceSite, SettingsManager.Instance.WebServiceLogin, SettingsManager.Instance.WebServicePassword);

			Init();
		}

		public List<ILibraryLink> DeadLinks { get; private set; }
		public List<ILibraryLink> ExpiredLinks { get; private set; }
		public OvernightsCalendar OvernightsCalendar { get; set; }

		public IPadManager IPadManager { get; private set; }

		#region ILibrary Members
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
			var typeNode = data.SelectSingleNode("Type");
			int tempInt;
			if (typeNode != null && int.TryParse(typeNode.InnerText, out tempInt))
			{
				var type = (FileTypes)tempInt;
				if (type == FileTypes.Folder)
					libraryFile = new LibraryFolderLink(parentFolder);
			}
			return libraryFile ?? new LibraryLink(parentFolder);
		}

		public RootFolder GetRootFolder(Guid folderId)
		{
			var folder = ExtraFolders.FirstOrDefault(x => x.RootId.Equals(folderId));
			if (folder != null)
				return folder;
			return RootFolder;
		}

		public List<IPreviewContainer> PreviewContainers { get; private set; }
		public string StoragePath
		{
			get { return Folder.FullName; }
		}

		public IPreviewContainer GetPreviewContainer(string originalPath)
		{
			var previewContainer = PreviewContainers.FirstOrDefault(x => x.OriginalPath.ToLower().Equals(originalPath.ToLower()));
			if (previewContainer != null) return previewContainer;
			previewContainer = UniversalPreviewContainer.CreateInstance(this,originalPath);
			PreviewContainers.Add(previewContainer);
			return previewContainer;
		}

		public IPreviewGenerator GetPreviewGenerator(IPreviewContainer previewContainer)
		{
			IPreviewGenerator previewGenerator = null;
			switch (previewContainer.Extension.ToUpper())
			{
				case ".PPT":
				case ".PPTX":
					previewGenerator = new PowerPointPreviewGenerator(previewContainer);
					break;
				case ".DOC":
				case ".DOCX":
					previewGenerator = new WordPreviewGenerator(previewContainer);
					break;
				case ".XLS":
				case ".XLSX":
					previewGenerator = new ExcelPreviewGenerator(previewContainer);
					break;
				case ".PDF":
					previewGenerator = new PdfPreviewGenerator(previewContainer);
					break;
				case ".MPEG":
				case ".WMV":
				case ".AVI":
				case ".WMZ":
				case ".MPG":
				case ".ASF":
				case ".MOV":
				case ".MP4":
				case ".M4V":
				case ".FLV":
				case ".OGV":
				case ".OGM":
				case ".OGX":
					previewGenerator = new VideoPreviewGenerator(previewContainer);
					break;
			}
			return previewGenerator;
		}

		public void UpdatePreviewableObject(string originalPath, DateTime lastChanged)
		{
			foreach (var file in Pages.SelectMany(page => page.Folders.SelectMany(folder => folder.Files.Cast<LibraryLink>())))
			{
				if (file.OriginalPath.ToLower().Equals(originalPath.ToLower()))
					file.LastChanged = lastChanged;
			}
		}

		public void DeletePreviewableObject(string originalPath)
		{
			foreach (var file in Pages
				.SelectMany(page => page.Folders
					.SelectMany(folder => folder.Files.Cast<LibraryLink>())
				.Where(file=> String.Compare(file.OriginalPath, originalPath, StringComparison.OrdinalIgnoreCase) == 0)
				.ToList()))
				file.RemoveFromCollection();
		}

		public void UpdatePreviewContainers()
		{
			foreach (var previewContainer in PreviewContainers)
			{
				var alive = false;
				var generatePreviewImages = false;
				var generateContentText = false;
				var files = Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).ToList();
				foreach (var file in files.Union(files.OfType<LibraryFolderLink>().SelectMany(f => f.AllFiles)))
				{
					if (file.ExtendedProperties.IsForbidden ||
						!(!file.ExtendedProperties.IsRestricted ||
							((!String.IsNullOrEmpty(file.ExtendedProperties.AssignedUsers) ||
							!String.IsNullOrEmpty(file.ExtendedProperties.DeniedUsers))))) continue;
					alive = file.OriginalPath.ToLower().Equals(previewContainer.OriginalPath.ToLower());
					generatePreviewImages |= alive && file.ExtendedProperties.GeneratePreviewImages;
					generateContentText |= alive && file.ExtendedProperties.GenerateContentText;
					if (alive) break;
				}
				if (alive)
				{
					previewContainer.GenerateImages = generatePreviewImages;
					previewContainer.GenerateText = generateContentText;
					continue;
				}
				previewContainer.ClearContent();
				previewContainer.OriginalPath = string.Empty;
			}
			PreviewContainers.RemoveAll(x => string.IsNullOrEmpty(x.OriginalPath));
			Save();
		}
		#endregion

		#region Program Manager Settings
		public bool EnableProgramManagerSync { get; set; }
		public string ProgramManagerLocation { get; set; }
		#endregion

		public void Init()
		{
			CheckIfOldFormat();
			Load();
			ProcessDeadLinks();
			ProcessExpiredLinks();
		}

		private void CheckIfOldFormat()
		{
			string file = Path.Combine(Folder.FullName, Constants.StorageFileName);
			if (File.Exists(file))
			{
				var document = new XmlDocument();
				document.Load(file);
				XmlNode node = document.SelectSingleNode(@"/Cache");
				if (node != null)
				{
					var oldFormatLibrary = new OldFormatLibrary(Name, Folder);
					try
					{
						oldFormatLibrary.SaveInNewFormat();
					}
					catch (Exception ex)
					{
						AppManager.Instance.ShowWarning(ex.Message + Environment.NewLine + ex.Data.Values);
					}
				}
			}
		}

		private void Load()
		{
			DateTime tempDate = DateTime.Now;
			bool tempBool = false;
			Guid tempGuid;

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

				XmlNode node = document.SelectSingleNode(@"/Library/Name");
				if (node != null)
					Name = node.InnerText;
				node = document.SelectSingleNode(@"/Library/Identifier");
				if (node != null)
					if (Guid.TryParse(node.InnerText, out tempGuid))
						Identifier = tempGuid;
				node = document.SelectSingleNode(@"/Library/BrandingText");
				if (node != null)
					BrandingText = node.InnerText;
				node = document.SelectSingleNode(@"/Library/SyncDate");
				if (node != null)
					if (DateTime.TryParse(node.InnerText, out tempDate))
						SyncDate = tempDate;
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
						var previewContainer = UniversalPreviewContainer.CreateInstance(this, childNode);
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

		public void Save()
		{
			var xml = new StringBuilder();
			xml.AppendLine("<Library>");
			xml.AppendLine(@"<Name>" + Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
			xml.AppendLine(@"<Identifier>" + Identifier.ToString() + @"</Identifier>");
			xml.AppendLine(@"<UseDirectAccess>" + UseDirectAccess + @"</UseDirectAccess>");
			xml.AppendLine(@"<DirectAccessFileBottomDate>" + DirectAccessFileBottomDate.ToString() + @"</DirectAccessFileBottomDate>");
			xml.AppendLine(@"<RootFolder>" + Folder.FullName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RootFolder>");
			xml.AppendLine(@"<BrandingText>" + BrandingText.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</BrandingText>");
			xml.AppendLine(@"<SyncDate>" + SyncDate + @"</SyncDate>");
			xml.AppendLine(@"<ApplyAppearanceForAllWindows>" + ApplyAppearanceForAllWindows + @"</ApplyAppearanceForAllWindows>");
			xml.AppendLine(@"<ApplyWidgetForAllWindows>" + ApplyWidgetForAllWindows + @"</ApplyWidgetForAllWindows>");
			xml.AppendLine(@"<ApplyBannerForAllWindows>" + ApplyBannerForAllWindows + @"</ApplyBannerForAllWindows>");
			xml.AppendLine(@"<MinimizeOnSync>" + MinimizeOnSync + @"</MinimizeOnSync>");
			xml.AppendLine(@"<CloseAfterSync>" + CloseAfterSync + @"</CloseAfterSync>");
			xml.AppendLine(@"<ShowProgressDuringSync>" + ShowProgressDuringSync + @"</ShowProgressDuringSync>");
			xml.AppendLine(@"<FullSync>" + FullSync + @"</FullSync>");
			xml.AppendLine(@"<VideoConversionWarning>" + VideoConversionWarning + @"</VideoConversionWarning>");
			xml.AppendLine(@"<EnableInactiveLinks>" + EnableInactiveLinks + @"</EnableInactiveLinks>");
			xml.AppendLine(@"<InactiveLinksBoldWarning>" + InactiveLinksBoldWarning + @"</InactiveLinksBoldWarning>");
			xml.AppendLine(@"<ReplaceInactiveLinksWithLineBreak>" + ReplaceInactiveLinksWithLineBreak + @"</ReplaceInactiveLinksWithLineBreak>");
			xml.AppendLine(@"<InactiveLinksMessageAtStartup>" + InactiveLinksMessageAtStartup + @"</InactiveLinksMessageAtStartup>");
			xml.AppendLine(@"<SendEmail>" + SendEmail + @"</SendEmail>");
			xml.AppendLine("<ExtraRoots>");
			foreach (var folder in ExtraFolders)
				xml.AppendLine(@"<ExtraRoot>" + folder.Serialize() + @"</ExtraRoot>");
			xml.AppendLine("</ExtraRoots>");
			xml.AppendLine("<Pages>");
			foreach (var page in Pages)
				xml.AppendLine(@"<Page>" + page.Serialize() + @"</Page>");
			xml.AppendLine("</Pages>");
			xml.AppendLine("<EmailList>");
			foreach (string email in EmailList)
				xml.AppendLine(@"<Email>" + email.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Email>");
			xml.AppendLine("</EmailList>");
			xml.AppendLine("<AutoWidgets>");
			foreach (var autoWidget in AutoWidgets)
				xml.AppendLine(@"<AutoWidget>" + autoWidget.Serialize() + @"</AutoWidget>");
			xml.AppendLine("</AutoWidgets>");
			xml.AppendLine("<PreviewContainers>");
			foreach (var previewContainer in PreviewContainers)
				xml.AppendLine(@"<PreviewContainer>" + previewContainer.Serialize() + @"</PreviewContainer>");
			xml.AppendLine("</PreviewContainers>");

			xml.AppendLine(@"<OvernightsCalendar>" + OvernightsCalendar.Serialize() + @"</OvernightsCalendar>");
			xml.AppendLine(@"<IPadManager>" + IPadManager.Serialize() + @"</IPadManager>");

			#region Program Manager Settings
			xml.AppendLine(@"<EnableProgramManagerSync>" + EnableProgramManagerSync.ToString() + @"</EnableProgramManagerSync>");
			if (!string.IsNullOrEmpty(ProgramManagerLocation))
				xml.AppendLine(@"<ProgramManagerLocation>" + ProgramManagerLocation.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</ProgramManagerLocation>");
			#endregion

			xml.AppendLine(@"</Library>");

			using (var sw = new StreamWriter(Path.Combine(Folder.FullName, Constants.StorageFileName), false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}
		}

		public void SaveLight()
		{
			var xml = new StringBuilder();
			xml.AppendLine("<Library>");
			xml.AppendLine(@"<Identifier>" + Identifier + @"</Identifier>");
			xml.AppendLine(@"</Library>");

			using (var sw = new StreamWriter(Path.Combine(Folder.FullName, Constants.StorageLightFileName), false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}
		}

		public void PrepareForRegularSynchronize()
		{
			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
			{
				SyncDate = DateTime.Now;
				if (IsConfigured)
					Save();
				if (!UseDirectAccess)
				{
					ProcessPresentationProperties();
					GeneratePresentationPreviewFiles();
					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						NotifyAboutExpiredLinks();
				}
			}
			if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) return;
		}

		public void PrepareForIPadSynchronize()
		{
			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				if (!UseDirectAccess)
				{
					UpdatePreviewContainers();
					GenerateExtendedPreviewFiles();
				}

			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
			{
				IPadManager.SaveJson();
				SaveLight();
			}
		}

		public void ProcessDeadLinks()
		{
			DeadLinks.Clear();
			foreach (var folder in Pages.SelectMany(page => page.Folders))
			{
				foreach (LibraryLink file in folder.Files)
					file.CheckIfDead();
				DeadLinks.AddRange(folder.Files.Where(x => x.IsDead));
			}
		}

		private void ProcessExpiredLinks()
		{
			ExpiredLinks.Clear();
			foreach (var folder in Pages.SelectMany(page => page.Folders))
				ExpiredLinks.AddRange(folder.Files.Where(x => x.ExpirationDateOptions.IsExpired));
		}

		public void ProcessPresentationProperties()
		{
			var processFiles = new List<LibraryLink>();
			var files = Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).OfType<LibraryLink>();
			processFiles.AddRange(files
				.Where(file => file.Type == FileTypes.Presentation && (file.PresentationProperties == null || File.GetLastWriteTime(file.OriginalPath) > file.PresentationProperties.LastUpdate))
				.Union(files.OfType<LibraryFolderLink>().SelectMany(f => f.AllFiles))
					.Where(file => file.Type == FileTypes.Presentation && (file.PresentationProperties == null || File.GetLastWriteTime(file.OriginalPath) > file.PresentationProperties.LastUpdate))
					.OfType<LibraryLink>()
				);
			if (!processFiles.Any()) return;
			if (!PowerPointHelper.Instance.Connect()) return;
			foreach (var file in processFiles)
				file.GetPresentationProperties();
			PowerPointHelper.Instance.Disconnect();
			Save();
		}

		private void GeneratePresentationPreviewFiles()
		{
			var links = new List<LibraryLink>();
			var files = Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).OfType<LibraryLink>();
			links.AddRange(files.Where(file => file.Type == FileTypes.Presentation).Union(files.OfType<LibraryFolderLink>().SelectMany(f => f.AllFiles)).Where(file => file.Type == FileTypes.Presentation).OfType<LibraryLink>());
			foreach (var file in links)
			{
				if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
				if (file.PreviewContainer == null)
					file.PreviewContainer = new PresentationPreviewContainer(file);
				file.PreviewContainer.UpdateContent();
			}
			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				Save();
		}

		private void GenerateExtendedPreviewFiles()
		{
			foreach (var previewContainer in PreviewContainers
				.Where(x => x.Type == FileTypes.Presentation || x.Type == FileTypes.Other))
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					previewContainer.UpdateContent();
				}
				else
					break;
			}
		}

		public void DeleteDeadLinks(Guid[] deadLinkIdentifiers)
		{
			foreach (LibraryLink link in DeadLinks.Where(x => deadLinkIdentifiers.Contains(x.Identifier)))
				link.RemoveFromCollection();
			ProcessDeadLinks();
		}

		public void DeleteExpiredLinks(Guid[] expiredLinkIdentifiers)
		{
			foreach (LibraryLink link in ExpiredLinks.Where(x => expiredLinkIdentifiers.Contains(x.Identifier)))
				link.RemoveFromCollection();
			ProcessExpiredLinks();
		}

		public void NotifyAboutExpiredLinks()
		{
			ProcessExpiredLinks();
			if (!ExpiredLinks.Any(x => x.ExpirationDateOptions.SendEmailWhenSync)) return;
			if (OutlookHelper.Instance.Connect())
			{
				OutlookHelper.Instance.CreateMessage(EmailList.ToArray(), string.Join(Environment.NewLine, ExpiredLinks.Where(x => x.ExpirationDateOptions.SendEmailWhenSync).Select(y => y.OriginalPath)), SendEmail);
				OutlookHelper.Instance.Disconnect();
			}
			else
				AppManager.Instance.ShowWarning("Cannot open Outlook");
		}

		public void AddPage()
		{
			var page = new LibraryPage(this);
			page.Order = Pages.Count;
			Pages.Add(page);
		}

		public void UpPage(int position)
		{
			if (position > 0)
			{
				Pages[position].Order--;
				Pages[position - 1].Order++;
				Pages.Sort((x, y) => x.Order.CompareTo(y.Order));
			}
		}

		public void DownPage(int position)
		{
			if (position < Pages.Count - 1)
			{
				Pages[position].Order++;
				Pages[position + 1].Order--;
				Pages.Sort((x, y) => x.Order.CompareTo(y.Order));
			}
		}

		public void RebuildPagesOrder()
		{
			for (int i = 0; i < Pages.Count; i++)
				Pages[i].Order = i;
		}

		public void AddExtraRoot()
		{
			var folder = new RootFolder(this);
			folder.RootId = Guid.NewGuid();
			folder.Order = ExtraFolders.Count;
			ExtraFolders.Add(folder);
		}

		public void UpExtraRoot(int position)
		{
			if (position > 0)
			{
				ExtraFolders[position].Order--;
				ExtraFolders[position - 1].Order++;
				ExtraFolders.Sort((x, y) => x.Order.CompareTo(y.Order));
			}
		}

		public void DownExtraRoot(int position)
		{
			if (position < ExtraFolders.Count - 1)
			{
				ExtraFolders[position].Order++;
				ExtraFolders[position + 1].Order--;
				ExtraFolders.Sort((x, y) => x.Order.CompareTo(y.Order));
			}
		}

		public void RebuildExtraFoldersOrder()
		{
			for (int i = 0; i < ExtraFolders.Count; i++)
				ExtraFolders[i].Order = i;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}