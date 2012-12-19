﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class Library : ILibrary
	{
		private RootFolder _rootFolder = null;

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
		public bool EnableInactiveLinks { get; set; }
		public bool InactiveLinksBoldWarning { get; set; }
		public bool ReplaceInactiveLinksWithLineBreak { get; set; }
		public bool InactiveLinksMessageAtStartup { get; set; }
		public bool SendEmail { get; set; }

		public bool IsConfigured { get; set; }

		public List<RootFolder> ExtraFolders { get; private set; }
		public List<LibraryPage> Pages { get; private set; }
		public List<string> EmailList { get; private set; }
		public List<ILibraryFile> DeadLinks { get; private set; }
		public List<ILibraryFile> ExpiredLinks { get; private set; }
		public List<AutoWidget> AutoWidgets { get; private set; }

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

		public OvernightsCalendar OvernightsCalendar { get; set; }

		public IPadManager IPadManager { get; private set; }

		public RootFolder RootFolder
		{
			get
			{
				if (_rootFolder == null)
				{
					_rootFolder = new RootFolder(this);
					_rootFolder.RootId = Guid.Empty;
					_rootFolder.Folder = this.Folder;
				}
				return _rootFolder;
			}
		}

		public Library(string name, DirectoryInfo folder)
		{
			this.Identifier = Guid.NewGuid();
			this.Folder = folder;
			this.Name = name;
			this.IsConfigured = false;
			this.ExtraFolders = new List<RootFolder>();
			this.Pages = new List<LibraryPage>();
			this.EmailList = new List<string>();
			this.DeadLinks = new List<ILibraryFile>();
			this.ExpiredLinks = new List<ILibraryFile>();
			this.AutoWidgets = new List<AutoWidget>();
			this.PreviewContainers = new List<IPreviewContainer>();

			#region Auto Sync Settings
			this.SyncScheduleRecords = new List<SyncScheduleRecord>();

			//Obsolte, using for compatibility with old versions
			this.SyncTimes = new List<TimePoint>();
			#endregion

			this.OvernightsCalendar = new OvernightsCalendar(this);
			this.IPadManager = new IPadManager(this);

			Init();
		}

		public void Init()
		{
			Load();
			ProcessDeadLinks();
			ProcessExpiredLinks();
		}

		private void Load()
		{
			DateTime tempDate = DateTime.Now;
			bool tempBool = false;
			Guid tempGuid;

			this.BrandingText = string.Empty;
			this.SyncDate = DateTime.Now;
			this.MinimizeOnSync = true;
			this.CloseAfterSync = true;
			this.ShowProgressDuringSync = true;
			this.EnableInactiveLinks = true;
			this.InactiveLinksBoldWarning = true;
			this.ReplaceInactiveLinksWithLineBreak = false;
			this.InactiveLinksMessageAtStartup = true;
			this.SendEmail = false;
			this.Pages.Clear();
			this.EmailList.Clear();
			this.AutoWidgets.Clear();
			this.EnableAutoSync = false;
			this.SyncScheduleRecords.Clear();
			this.ExtraFolders.Clear();
			this.PreviewContainers.Clear();

			bool fileBusy = false;
			string file = Path.Combine(this.Folder.FullName, Constants.StorageFileName);
			int counter = 0;
			do
			{
				try
				{
					if (File.Exists(file))
					{
						XmlDocument document = new XmlDocument();
						document.Load(file);

						XmlNode node = document.SelectSingleNode(@"/Library/Name");
						if (node != null)
							this.Name = node.InnerText;
						node = document.SelectSingleNode(@"/Library/Identifier");
						if (node != null)
							if (Guid.TryParse(node.InnerText, out tempGuid))
								this.Identifier = tempGuid;
						node = document.SelectSingleNode(@"/Library/BrandingText");
						if (node != null)
							this.BrandingText = node.InnerText;
						node = document.SelectSingleNode(@"/Library/SyncDate");
						if (node != null)
							if (DateTime.TryParse(node.InnerText, out tempDate))
								this.SyncDate = tempDate;
						node = document.SelectSingleNode(@"/Library/ApplyAppearanceForAllWindows");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.ApplyAppearanceForAllWindows = tempBool;
						node = document.SelectSingleNode(@"/Library/ApplyWidgetForAllWindows");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.ApplyWidgetForAllWindows = tempBool;
						node = document.SelectSingleNode(@"/Library/ApplyBannerForAllWindows");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.ApplyBannerForAllWindows = tempBool;
						node = document.SelectSingleNode(@"/Library/MinimizeOnSync");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.MinimizeOnSync = tempBool;
						node = document.SelectSingleNode(@"/Library/CloseAfterSync");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.CloseAfterSync = tempBool;
						node = document.SelectSingleNode(@"/Library/ShowProgressDuringSync");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.ShowProgressDuringSync = tempBool;
						node = document.SelectSingleNode(@"/Library/EnableInactiveLinks");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.EnableInactiveLinks = tempBool;
						node = document.SelectSingleNode(@"/Library/InactiveLinksBoldWarning");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.InactiveLinksBoldWarning = tempBool;
						node = document.SelectSingleNode(@"/Library/ReplaceInactiveLinksWithLineBreak");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.ReplaceInactiveLinksWithLineBreak = tempBool;
						node = document.SelectSingleNode(@"/Library/InactiveLinksMessageAtStartup");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.InactiveLinksMessageAtStartup = tempBool;
						node = document.SelectSingleNode(@"/Library/SendEmail");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.SendEmail = tempBool;

						node = document.SelectSingleNode(@"/Library/ExtraRoots");
						if (node != null)
							foreach (XmlNode childNode in node.ChildNodes)
							{
								RootFolder folder = new RootFolder(this);
								folder.Deserialize(childNode);
								this.ExtraFolders.Add(folder);
							}

						node = document.SelectSingleNode(@"/Library/PreviewContainers");
						if (node != null)
							foreach (XmlNode childNode in node.ChildNodes)
							{
								UniversalPreviewContainer previewContainer = new UniversalPreviewContainer(this);
								previewContainer.Deserialize(childNode);
								this.PreviewContainers.Add(previewContainer);
							}

						node = document.SelectSingleNode(@"/Library/Pages");
						if (node != null)
							foreach (XmlNode childNode in node.ChildNodes)
							{
								LibraryPage page = new LibraryPage(this);
								page.Deserialize(childNode);
								this.Pages.Add(page);
							}
						node = document.SelectSingleNode(@"/Library/EmailList");
						if (node != null)
							foreach (XmlNode childNode in node.ChildNodes)
							{
								if (childNode.Name.Equals("Email"))
									this.EmailList.Add(childNode.InnerText);
							}
						node = document.SelectSingleNode(@"/Library/AutoWidgets");
						if (node != null)
							foreach (XmlNode childNode in node.ChildNodes)
							{
								AutoWidget autoWidget = new AutoWidget();
								autoWidget.Deserialize(childNode);
								this.AutoWidgets.Add(autoWidget);
							}

						#region Auto Sync Settings
						node = document.SelectSingleNode(@"/Library/EnableAutoSync");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.EnableAutoSync = tempBool;
						node = document.SelectSingleNode(@"/Library/SyncSchedule");
						if (node != null)
							foreach (XmlNode syncTimeNode in node.ChildNodes)
							{
								if (syncTimeNode.Name.Equals("SyncScheduleRecord"))
								{
									SyncScheduleRecord synctTime = new SyncScheduleRecord();
									synctTime.Deserialize(syncTimeNode);
									this.SyncScheduleRecords.Add(synctTime);
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
									TimePoint synctTime = new TimePoint();
									synctTime.Deserialize(syncTimeNode);
									this.SyncTimes.Add(synctTime);
								}
							}
							if (this.SyncTimes.Count > 0)
							{
								DateTime[] syncTimes = this.SyncTimes.Select(x => new DateTime(1, 1, 1, x.Time.Hour, x.Time.Minute, 0)).Distinct().ToArray();
								foreach (DateTime syncTime in syncTimes)
								{
									SyncScheduleRecord syncScheduleRecord = new SyncScheduleRecord();
									syncScheduleRecord.Time = syncTime;

									DayOfWeek[] days = this.SyncTimes.Where(x => x.Time.Hour.Equals(syncTime.Hour) && x.Time.Minute.Equals(syncTime.Minute)).Select(x => x.Day).ToArray();
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
									this.SyncScheduleRecords.Add(syncScheduleRecord);
								}
							}
						}
						#endregion

						node = document.SelectSingleNode(@"/Library/OvernightsCalendar");
						if (node != null)
							this.OvernightsCalendar.Deserialize(node);

						node = document.SelectSingleNode(@"/Library/IPadContentManager");
						if (node != null)
							this.IPadManager.Deserialize(node);
						else
						{
							node = document.SelectSingleNode(@"/Library/IPadManager");
							if (node != null)
								this.IPadManager.Deserialize(node);
						}

						#region Program Manager Settings
						node = document.SelectSingleNode(@"/Library/EnableProgramManagerSync");
						if (node != null)
							if (bool.TryParse(node.InnerText, out tempBool))
								this.EnableProgramManagerSync = tempBool;

						node = document.SelectSingleNode(@"/Library/ProgramManagerLocation");
						if (node != null)
							this.ProgramManagerLocation = node.InnerText;
						#endregion

						this.IsConfigured = true;
					}
					if (this.Pages.Count == 0)
						this.Pages.Add(new LibraryPage(this));

					fileBusy = false;
				}
				catch
				{
					counter++;
					fileBusy = true;
					System.Threading.Thread.Sleep(1000);
				}
			}
			while (fileBusy && counter < 5);
		}

		public void Save()
		{
			StringBuilder xml = new StringBuilder();
			xml.AppendLine("<Library>");
			xml.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
			xml.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
			xml.AppendLine(@"<UseDirectAccess>" + this.UseDirectAccess + @"</UseDirectAccess>");
			xml.AppendLine(@"<DirectAccessFileBottomDate>" + this.DirectAccessFileBottomDate.ToString() + @"</DirectAccessFileBottomDate>");
			xml.AppendLine(@"<RootFolder>" + this.Folder.FullName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RootFolder>");
			xml.AppendLine(@"<BrandingText>" + this.BrandingText.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</BrandingText>");
			xml.AppendLine(@"<SyncDate>" + this.SyncDate + @"</SyncDate>");
			xml.AppendLine(@"<ApplyAppearanceForAllWindows>" + this.ApplyAppearanceForAllWindows + @"</ApplyAppearanceForAllWindows>");
			xml.AppendLine(@"<ApplyWidgetForAllWindows>" + this.ApplyWidgetForAllWindows + @"</ApplyWidgetForAllWindows>");
			xml.AppendLine(@"<ApplyBannerForAllWindows>" + this.ApplyBannerForAllWindows + @"</ApplyBannerForAllWindows>");
			xml.AppendLine(@"<MinimizeOnSync>" + this.MinimizeOnSync + @"</MinimizeOnSync>");
			xml.AppendLine(@"<CloseAfterSync>" + this.CloseAfterSync + @"</CloseAfterSync>");
			xml.AppendLine(@"<ShowProgressDuringSync>" + this.ShowProgressDuringSync + @"</ShowProgressDuringSync>");
			xml.AppendLine(@"<EnableInactiveLinks>" + this.EnableInactiveLinks + @"</EnableInactiveLinks>");
			xml.AppendLine(@"<InactiveLinksBoldWarning>" + this.InactiveLinksBoldWarning + @"</InactiveLinksBoldWarning>");
			xml.AppendLine(@"<ReplaceInactiveLinksWithLineBreak>" + this.ReplaceInactiveLinksWithLineBreak + @"</ReplaceInactiveLinksWithLineBreak>");
			xml.AppendLine(@"<InactiveLinksMessageAtStartup>" + this.InactiveLinksMessageAtStartup + @"</InactiveLinksMessageAtStartup>");
			xml.AppendLine(@"<SendEmail>" + this.SendEmail + @"</SendEmail>");
			xml.AppendLine("<ExtraRoots>");
			foreach (RootFolder folder in this.ExtraFolders)
				xml.AppendLine(@"<ExtraRoot>" + folder.Serialize() + @"</ExtraRoot>");
			xml.AppendLine("</ExtraRoots>");
			xml.AppendLine("<Pages>");
			foreach (LibraryPage page in this.Pages)
				xml.AppendLine(@"<Page>" + page.Serialize() + @"</Page>");
			xml.AppendLine("</Pages>");
			xml.AppendLine("<EmailList>");
			foreach (string email in this.EmailList)
				xml.AppendLine(@"<Email>" + email.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Email>");
			xml.AppendLine("</EmailList>");
			xml.AppendLine("<AutoWidgets>");
			foreach (AutoWidget autoWidget in this.AutoWidgets)
				xml.AppendLine(@"<AutoWidget>" + autoWidget.Serialize() + @"</AutoWidget>");
			xml.AppendLine("</AutoWidgets>");
			xml.AppendLine("<PreviewContainers>");
			foreach (IPreviewContainer previewContainer in this.PreviewContainers)
				xml.AppendLine(@"<PreviewContainer>" + previewContainer.Serialize() + @"</PreviewContainer>");
			xml.AppendLine("</PreviewContainers>");

			#region Auto Sync Settings
			StringBuilder autoSyncSettings = new StringBuilder();
			autoSyncSettings.AppendLine("<AutoSyncSettings>");
			xml.AppendLine(@"<EnableAutoSync>" + this.EnableAutoSync.ToString() + @"</EnableAutoSync>");
			autoSyncSettings.AppendLine(@"<EnableAutoSync>" + this.EnableAutoSync.ToString() + @"</EnableAutoSync>");
			xml.AppendLine(@"<SyncSchedule>");
			autoSyncSettings.AppendLine(@"<SyncSchedule>");
			foreach (SyncScheduleRecord syncTime in this.SyncScheduleRecords)
			{
				xml.AppendLine(@"<SyncScheduleRecord>" + syncTime.Serialize() + @"</SyncScheduleRecord>");
				autoSyncSettings.AppendLine(@"<SyncScheduleRecord>" + syncTime.Serialize() + @"</SyncScheduleRecord>");
			}
			xml.AppendLine(@"</SyncSchedule>");
			autoSyncSettings.AppendLine(@"</SyncSchedule>");
			autoSyncSettings.AppendLine("</AutoSyncSettings>");
			#endregion

			xml.AppendLine(@"<OvernightsCalendar>" + this.OvernightsCalendar.Serialize() + @"</OvernightsCalendar>");
			xml.AppendLine(@"<IPadManager>" + this.IPadManager.Serialize() + @"</IPadManager>");

			#region Program Manager Settings
			xml.AppendLine(@"<EnableProgramManagerSync>" + this.EnableProgramManagerSync.ToString() + @"</EnableProgramManagerSync>");
			if (!string.IsNullOrEmpty(this.ProgramManagerLocation))
				xml.AppendLine(@"<ProgramManagerLocation>" + this.ProgramManagerLocation.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</ProgramManagerLocation>");
			#endregion

			xml.AppendLine(@"</Library>");

			using (StreamWriter sw = new StreamWriter(Path.Combine(this.Folder.FullName, Constants.StorageFileName), false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}
		}

		public void SaveLight()
		{
			StringBuilder xml = new StringBuilder();
			xml.AppendLine("<Library>");
			xml.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
			xml.AppendLine(@"</Library>");

			using (StreamWriter sw = new StreamWriter(Path.Combine(this.Folder.FullName, Constants.StorageLightFileName), false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}
		}

		public ILibraryFile GetLinkInstance(LibraryFolder parentFolder)
		{
			return new LibraryFile(parentFolder);
		}

		public void PrepareForRegularSynchronize()
		{
			if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
			{
				this.SyncDate = DateTime.Now;
				if (this.IsConfigured)
					Save();
				if (!this.UseDirectAccess)
				{
					GeneratePresentationPreviewFiles();
					ProcessAttachments();
					if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
						NotifyAboutExpiredLinks();
				}
			}
			if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
				Archive();
		}

		public void PrepareForIPadSynchronize()
		{
			if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
				if (!this.UseDirectAccess)
				{
					UpdatePreviewContainers();
					GenerateExtendedPreviewFiles();
					ProcessAttachments();
				}

			if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
			{
				this.IPadManager.SaveJson();
				this.SaveLight();
				Archive();
			}
		}

		private void ProcessDeadLinks()
		{
			this.DeadLinks.Clear();
			foreach (LibraryPage page in this.Pages)
				foreach (LibraryFolder folder in page.Folders)
				{
					foreach (LibraryFile file in folder.Files)
						file.CheckIfDead();
					this.DeadLinks.AddRange(folder.Files.Where(x => x.IsDead));
				}
		}

		private void ProcessExpiredLinks()
		{
			this.ExpiredLinks.Clear();
			foreach (LibraryPage page in this.Pages)
				foreach (LibraryFolder folder in page.Folders)
					this.ExpiredLinks.AddRange(folder.Files.Where(x => x.IsExpired));
		}

		public void ProcessPresentationProperties()
		{
			if (InteropClasses.PowerPointHelper.Instance.Connect())
			{
				foreach (LibraryPage page in this.Pages)
					foreach (LibraryFolder folder in page.Folders)
						foreach (LibraryFile file in folder.Files.Where(x => (x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.Presentation) && (x.PresentationProperties == null || File.GetLastWriteTime(x.OriginalPath) > x.PresentationProperties.LastUpdate)))
							file.GetPresentationPrperties();
				InteropClasses.PowerPointHelper.Instance.Disconnect();
				this.Save();
			}
		}

		public void ProcessAttachments()
		{
			List<Guid> actualAttachmentIds = new List<Guid>();

			foreach (LibraryPage page in this.Pages)
			{
				foreach (LibraryFolder folder in page.Folders)
				{
					foreach (LibraryFile file in folder.Files.Where(x => x.AttachmentProperties.Enable))
					{
						if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
						{
							foreach (LinkAttachment attachment in file.AttachmentProperties.FilesAttachments)
							{
								if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
								{
									if (attachment.IsSourceAvailable)
									{
										bool copy = true;
										if (attachment.IsDestinationAvailable)
										{
											DateTime sourceTimeStamp = File.GetLastWriteTime(attachment.OriginalPath);
											DateTime destinationTimeStamp = File.GetLastWriteTime(attachment.DestinationPath);
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
								else
									break;
							}
						}
						else
							break;
					}
					if (!((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive))
						break;
				}
				if (!((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive))
					break;
			}
			if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
				this.Save();

			if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
			{
				DirectoryInfo attachmentRootFolder = new DirectoryInfo(Path.Combine(this.Folder.FullName, Constants.AttachmentsRootFolderName));
				if (attachmentRootFolder.Exists)
					foreach (DirectoryInfo subFolder in attachmentRootFolder.GetDirectories().Where(x => !actualAttachmentIds.Select(y => y.ToString()).Contains(x.Name) && !x.FullName.Contains("_gsdata_")))
						ToolClasses.SyncManager.DeleteFolder(subFolder);
			}
		}

		private void GeneratePresentationPreviewFiles()
		{
			foreach (LibraryPage page in this.Pages)
			{
				foreach (LibraryFolder folder in page.Folders)
				{
					foreach (LibraryFile file in folder.Files.Where(x => x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.Presentation))
					{
						if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
						{
							if (file.PreviewContainer == null)
								file.PreviewContainer = new PresentationPreviewContainer(file);
							file.PreviewContainer.UpdateContent();
						}
						else
							break;
					}
					if (!((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive))
						break;
				}
				if (!((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive))
					break;
			}
			if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
				this.Save();
		}

		private void GenerateExtendedPreviewFiles()
		{
			foreach (IPreviewContainer previewContainer in this.PreviewContainers.Where(x => x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.Presentation || x.Type == FileTypes.Other))
			{
				if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
				{
					previewContainer.UpdateContent();
				}
				else
					break;
			}
		}

		public void NotifyAboutExpiredLinks()
		{
			ProcessExpiredLinks();
			//if (this.ExpiredLinks.Where(x => x.ExpirationDateOptions.SendEmailWhenSync).Count() > 0)
			//{
			//    if (InteropClasses.OutlookHelper.Instance.Connect())
			//    {
			//        InteropClasses.OutlookHelper.Instance.CreateMessage(this.EmailList.ToArray(), string.Join(Environment.NewLine, this.ExpiredLinks.Where(x => x.ExpirationDateOptions.SendEmailWhenSync).Select(y => y.OriginalPath)), this.SendEmail);
			//        InteropClasses.OutlookHelper.Instance.Disconnect();
			//    }
			//    else
			//        FileManager.AppManager.Instance.ShowWarning("Cannot open Outlook");
			//}
		}

		private void Archive()
		{
			DateTime archiveDateTime = DateTime.Now;
			string archiveFolder = Path.Combine(AutoSynchronizer.ConfigurationClasses.SettingsManager.Instance.ArhivePath, archiveDateTime.ToString("MMddyy") + "-" + archiveDateTime.ToString("hhmmsstt"));
			try
			{
				if (!Directory.Exists(archiveFolder))
					Directory.CreateDirectory(archiveFolder);

				foreach (FileInfo file in this.Folder.GetFiles("*.xml"))
					file.CopyTo(Path.Combine(archiveFolder, file.Name), true);

				foreach (FileInfo file in this.Folder.GetFiles("*.json"))
					file.CopyTo(Path.Combine(archiveFolder, file.Name), true);
			}
			catch
			{
			}
		}

		public RootFolder GetRootFolder(Guid folderId)
		{
			RootFolder folder = this.ExtraFolders.Where(x => x.RootId.Equals(folderId)).FirstOrDefault();
			if (folder != null)
				return folder;
			else
				return this.RootFolder;
		}

		#region IPreviewStorage Members
		public List<IPreviewContainer> PreviewContainers { get; private set; }
		public string StoragePath
		{
			get
			{
				return this.Folder.FullName;
			}
		}

		public IPreviewContainer GetPreviewContainer(string originalPath)
		{
			IPreviewContainer previewContainer = this.PreviewContainers.Where(x => x.OriginalPath.ToLower().Equals(originalPath.ToLower())).FirstOrDefault();
			if (previewContainer == null)
			{
				previewContainer = new UniversalPreviewContainer(this);
				previewContainer.OriginalPath = originalPath;
				this.PreviewContainers.Add(previewContainer);
			}
			return previewContainer;
		}

		public IPreviewGenerator GetPreviewGenerator(IPreviewContainer previewContainer)
		{
			SalesDepot.CoreObjects.BusinessClasses.IPreviewGenerator previewGenerator = null;
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
			foreach (var file in this.Pages.SelectMany(page => page.Folders.SelectMany(folder => folder.Files.Cast<LibraryFile>())))
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
			foreach (var previewContainer in this.PreviewContainers)
			{
				bool alive = false;
				foreach (var page in this.Pages)
				{
					foreach (var folder in page.Folders)
					{
						foreach (LibraryFile file in folder.Files)
						{
							alive = file.OriginalPath.ToLower().Equals(previewContainer.OriginalPath.ToLower());
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
				if (alive) continue;
				previewContainer.ClearContent();
				previewContainer.OriginalPath = string.Empty;
			}
			this.PreviewContainers.RemoveAll(x => string.IsNullOrEmpty(x.OriginalPath));
			this.Save();
		}
		#endregion
	}
}
