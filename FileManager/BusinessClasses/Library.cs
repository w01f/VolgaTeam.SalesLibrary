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

			#region Auto Sync Settings
			SyncScheduleRecords = new List<SyncScheduleRecord>();

			//Obsolte, using for compatibility with old versions
			SyncTimes = new List<TimePoint>();
			#endregion

			OvernightsCalendar = new OvernightsCalendar(this);
			IPadManager = new IPadManager(this);

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
			previewContainer = new UniversalPreviewContainer(this);
			previewContainer.OriginalPath = originalPath;
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
				var attachment = file.AttachmentProperties.FilesAttachments.FirstOrDefault(x => x.OriginalPath.ToLower().Equals(originalPath.ToLower()));
				if (attachment != null)
					attachment.LastChanged = lastChanged;
			}
		}

		public void UpdatePreviewContainers()
		{
			foreach (var previewContainer in PreviewContainers)
			{
				var alive = false;
				var onlyText = false;
				foreach (var page in Pages)
				{
					foreach (var folder in page.Folders)
					{
						foreach (LibraryLink file in folder.Files)
						{
							if (file.IsForbidden || !(!file.IsRestricted || (file.IsRestricted && !string.IsNullOrEmpty(file.AssignedUsers))))
								continue;
							if (file is LibraryFolderLink)
								alive = (file as LibraryFolderLink).IsPreviewContainerAlive(previewContainer);
							else
								alive = file.OriginalPath.ToLower().Equals(previewContainer.OriginalPath.ToLower());
							onlyText |= alive && file.DoNotGeneratePreview;
							if (!alive)
								alive = file.AttachmentProperties.FilesAttachments.FirstOrDefault(x => x.OriginalPath.ToLower().Equals(previewContainer.OriginalPath.ToLower())) != null;
							if (alive)
								break;
						}
						if (alive)
							break;
					}
					if (alive)
						break;
				}

				if (alive)
				{
					previewContainer.OnlyText = onlyText;
					continue;
				}
				previewContainer.ClearContent();
				previewContainer.OriginalPath = string.Empty;
			}
			PreviewContainers.RemoveAll(x => string.IsNullOrEmpty(x.OriginalPath));
			Save();
		}
		#endregion

		#region Auto Sync Settings
		public bool EnableAutoSync { get; set; }
		public List<SyncScheduleRecord> SyncScheduleRecords { get; private set; }

		//Obsolte, using for compatibility with old versions
		public List<TimePoint> SyncTimes { get; private set; }
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
			EnableAutoSync = false;
			SyncScheduleRecords.Clear();
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
						var previewContainer = new UniversalPreviewContainer(this);
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

				#region Auto Sync Settings
				node = document.SelectSingleNode(@"/Library/EnableAutoSync");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EnableAutoSync = tempBool;
				node = document.SelectSingleNode(@"/Library/SyncSchedule");
				if (node != null)
					foreach (XmlNode syncTimeNode in node.ChildNodes)
					{
						if (syncTimeNode.Name.Equals("SyncScheduleRecord"))
						{
							var synctTime = new SyncScheduleRecord();
							synctTime.Deserialize(syncTimeNode);
							SyncScheduleRecords.Add(synctTime);
						}
					}
				//Obsolte, using for compatibility with old versions
				node = document.SelectSingleNode(@"/Library/AutoSyncTimes");
				if (node != null)
				{
					foreach (XmlNode syncTimeNode in node.ChildNodes)
					{
						if (syncTimeNode.Name.Equals("SyncTime"))
						{
							var synctTime = new TimePoint();
							synctTime.Deserialize(syncTimeNode);
							SyncTimes.Add(synctTime);
						}
					}
					if (SyncTimes.Count > 0)
					{
						DateTime[] syncTimes = SyncTimes.Select(x => new DateTime(1, 1, 1, x.Time.Hour, x.Time.Minute, 0)).Distinct().ToArray();
						foreach (DateTime syncTime in syncTimes)
						{
							var syncScheduleRecord = new SyncScheduleRecord();
							syncScheduleRecord.Time = syncTime;

							DayOfWeek[] days = SyncTimes.Where(x => x.Time.Hour.Equals(syncTime.Hour) && x.Time.Minute.Equals(syncTime.Minute)).Select(x => x.Day).ToArray();
							foreach (DayOfWeek day in days)
							{
								switch (day)
								{
									case DayOfWeek.Monday:
										syncScheduleRecord.Monday = true;
										break;
									case DayOfWeek.Tuesday:
										syncScheduleRecord.Tuesday = true;
										break;
									case DayOfWeek.Wednesday:
										syncScheduleRecord.Wednesday = true;
										break;
									case DayOfWeek.Thursday:
										syncScheduleRecord.Thursday = true;
										break;
									case DayOfWeek.Friday:
										syncScheduleRecord.Friday = true;
										break;
									case DayOfWeek.Saturday:
										syncScheduleRecord.Saturday = true;
										break;
									case DayOfWeek.Sunday:
										syncScheduleRecord.Sunday = true;
										break;
								}
							}
							SyncScheduleRecords.Add(syncScheduleRecord);
						}
					}
				}
				#endregion

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

			#region Auto Sync Settings
			var autoSyncSettings = new StringBuilder();
			autoSyncSettings.AppendLine("<AutoSyncSettings>");
			xml.AppendLine(@"<EnableAutoSync>" + EnableAutoSync.ToString() + @"</EnableAutoSync>");
			autoSyncSettings.AppendLine(@"<EnableAutoSync>" + EnableAutoSync.ToString() + @"</EnableAutoSync>");
			xml.AppendLine(@"<SyncSchedule>");
			autoSyncSettings.AppendLine(@"<SyncSchedule>");
			foreach (var syncTime in SyncScheduleRecords)
			{
				xml.AppendLine(@"<SyncScheduleRecord>" + syncTime.Serialize() + @"</SyncScheduleRecord>");
				autoSyncSettings.AppendLine(@"<SyncScheduleRecord>" + syncTime.Serialize() + @"</SyncScheduleRecord>");
			}
			xml.AppendLine(@"</SyncSchedule>");
			autoSyncSettings.AppendLine(@"</SyncSchedule>");
			autoSyncSettings.AppendLine("</AutoSyncSettings>");
			SettingsManager.Instance.SaveAutoSyncSettings(autoSyncSettings.ToString());
			#endregion

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
					ProcessAttachments();
					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
						NotifyAboutExpiredLinks();
				}
			}
			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				Archive();
		}

		public void PrepareForIPadSynchronize()
		{
			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				if (!UseDirectAccess)
				{
					UpdatePreviewContainers();
					GenerateExtendedPreviewFiles();
					ProcessAttachments();
				}

			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
			{
				IPadManager.SaveJson();
				SaveLight();
				Archive();
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
				ExpiredLinks.AddRange(folder.Files.Where(x => x.IsExpired));
		}

		public void ProcessPresentationProperties()
		{
			var processFiles = new List<LibraryLink>();
			var wholeFiles = Pages.SelectMany(page => page.Folders).SelectMany(folder => folder.Files);
			processFiles.AddRange(wholeFiles.Where(file => (file.Type == FileTypes.BuggyPresentation || file.Type == FileTypes.FriendlyPresentation || file.Type == FileTypes.Presentation) && (file.PresentationProperties == null || File.GetLastWriteTime(file.OriginalPath) > file.PresentationProperties.LastUpdate)).OfType<LibraryLink>());
			processFiles.AddRange(wholeFiles.OfType<LibraryFolderLink>().SelectMany(x => x.GetWholeContent().Where(file => (file.Type == FileTypes.BuggyPresentation || file.Type == FileTypes.FriendlyPresentation || file.Type == FileTypes.Presentation) && (file.PresentationProperties == null || File.GetLastWriteTime(file.OriginalPath) > file.PresentationProperties.LastUpdate))));
			if (!processFiles.Any()) return;
			if (!PowerPointHelper.Instance.Connect()) return;
			foreach (var file in processFiles)
				file.GetPresentationProperties();
			PowerPointHelper.Instance.Disconnect();
			Save();
		}

		public void ProcessAttachments()
		{
			var actualAttachmentIds = new List<Guid>();

			foreach (var page in Pages)
			{
				foreach (var folder in page.Folders)
				{
					foreach (var file in folder.Files)
					{
						var attachmentProperties = file.AttachmentProperties;
						if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
						foreach (var attachment in attachmentProperties.FilesAttachments)
						{
							if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) break;
							if (!attachment.IsSourceAvailable) continue;
							var copy = true;
							if (attachment.IsDestinationAvailable)
							{
								var sourceTimeStamp = File.GetLastWriteTime(attachment.OriginalPath);
								var destinationTimeStamp = File.GetLastWriteTime(attachment.DestinationPath);
								if (sourceTimeStamp == destinationTimeStamp)
									copy = false;
							}
							else
							{
								if (!Directory.Exists(Path.GetDirectoryName(attachment.DestinationPath)))
									Directory.CreateDirectory(Path.GetDirectoryName(attachment.DestinationPath));
							}
							if (copy)
								try
								{
									File.Copy(attachment.OriginalPath, attachment.DestinationPath, true);
								}
								catch { }
							actualAttachmentIds.Add(attachment.Identifier);
						}
					}
					if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
						break;
				}
				if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
					break;
			}
			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				Save();

			if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) return;
			var attachmentRootFolder = new DirectoryInfo(Path.Combine(Folder.FullName, Constants.AttachmentsRootFolderName));
			if (attachmentRootFolder.Exists)
				foreach (var subFolder in attachmentRootFolder.GetDirectories().Where(x => !actualAttachmentIds.Select(y => y.ToString()).Contains(x.Name) && !x.FullName.Contains("_gsdata_")))
					SyncManager.DeleteFolder(subFolder);
		}

		private void GeneratePresentationPreviewFiles()
		{
			var links = new List<LibraryLink>();
			links.AddRange(Pages.SelectMany(page => page.Folders.SelectMany(folder => folder.Files)).OfType<LibraryLink>());
			links.AddRange(links.OfType<LibraryFolderLink>().SelectMany(x => x.GetWholeContent()).ToList());
			foreach (var file in links.Where(x => x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.Presentation))
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
			foreach (var previewContainer in PreviewContainers.Where(x => x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.Presentation || x.Type == FileTypes.Other))
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

		private void Archive()
		{
			var archiveDateTime = DateTime.Now;
			var archiveFolder = Path.Combine(SettingsManager.Instance.ArhivePath, archiveDateTime.ToString("MMddyy") + "-" + archiveDateTime.ToString("hhmmsstt"));
			try
			{
				if (!Directory.Exists(archiveFolder))
					Directory.CreateDirectory(archiveFolder);

				foreach (FileInfo file in Folder.GetFiles("*.xml"))
					file.CopyTo(Path.Combine(archiveFolder, file.Name), true);

				foreach (FileInfo file in Folder.GetFiles("*.json"))
					file.CopyTo(Path.Combine(archiveFolder, file.Name), true);
			}
			catch { }
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