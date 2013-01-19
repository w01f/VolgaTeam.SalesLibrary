using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.BusinessClasses
{
	public class LibraryLink : ILibraryLink
	{
		private bool _linkAvailabel;
		private bool _linkAvailabilityChecked;

		protected string _linkLocalPath = string.Empty;
		private string _linkRemotePath = string.Empty;
		private string _note = string.Empty;
		private Image _widget;

		#region Compatibility with old versions
		private Image _oldBanner;
		private bool _oldEnableBanner;
		#endregion

		public LibraryLink(LibraryFolder parent)
		{
			Name = string.Empty;
			Parent = parent;
			RootId = Guid.Empty;
			Identifier = Guid.NewGuid();
			RelativePath = string.Empty;
			Type = FileTypes.Other;
			Order = 0;
			IsBold = false;
			IsDead = false;
			CriteriaOverlap = string.Empty;
			SearchTags = new LibraryFileSearchTags();
			ExpirationDateOptions = new ExpirationDateOptions();
			FileCard = new FileCard(this);
			PreviewContainer = null;
			SetProperties();
		}

		public PresentationPreviewContainer PreviewContainer { get; set; }
		public string LocalPath
		{
			get
			{
				if (string.IsNullOrEmpty(_linkLocalPath) && LinkAvailable)
					GetLocalCopy();
				return _linkLocalPath;
			}
		}

		public bool LinkAvailable
		{
			get
			{
				if (!_linkAvailabilityChecked)
				{
					switch (Type)
					{
						case FileTypes.BuggyPresentation:
						case FileTypes.Excel:
						case FileTypes.FriendlyPresentation:
						case FileTypes.MediaPlayerVideo:
						case FileTypes.Other:
						case FileTypes.Presentation:
						case FileTypes.PDF:
						case FileTypes.QuickTimeVideo:
						case FileTypes.Word:
						case FileTypes.OvernightsLink:
							_linkAvailabel = File.Exists(OriginalPath);
							break;
						case FileTypes.Folder:
							_linkAvailabel = Directory.Exists(OriginalPath);
							break;
						default:
							_linkAvailabel = true;
							break;
					}
					_linkAvailabilityChecked = true;
				}
				return _linkAvailabel;
			}
		}
		public string PreviewStoragePath
		{
			get { return Parent.Parent.Parent.Folder.FullName; }
		}

		#region ILibraryLink Members
		public string Name { get; set; }
		public LibraryFolder Parent { get; set; }
		public Guid RootId { get; set; }
		public Guid Identifier { get; set; }
		public string RelativePath { get; set; }
		public FileTypes Type { get; set; }
		public int Order { get; set; }
		public bool IsBold { get; set; }
		public bool IsDead { get; set; }
		public bool EnableWidget { get; set; }
		public string CriteriaOverlap { get; set; }
		public DateTime AddDate { get; set; }
		public DateTime LastChanged { get; set; }

		public LibraryFileSearchTags SearchTags { get; set; }
		public SearchGroup CustomKeywords { get; protected set; }
		public ExpirationDateOptions ExpirationDateOptions { get; set; }
		public PresentationProperties PresentationProperties { get; set; }
		public LineBreakProperties LineBreakProperties { get; set; }
		public AttachmentProperties AttachmentProperties { get; set; }
		public BannerProperties BannerProperties { get; set; }
		public FileCard FileCard { get; set; }

		public string OriginalPath
		{
			get
			{
				if (string.IsNullOrEmpty(_linkRemotePath))
				{
					if (Type == FileTypes.Url || Type == FileTypes.Network)
						return RelativePath;
					else if (Type == FileTypes.LineBreak)
						return string.Empty;
					else
						return ((Parent != null ? Parent.Parent.Parent.GetRootFolder(RootId).Folder.FullName : string.Empty) + @"\" + RelativePath).Replace(@"\\", @"\").Replace(@"\\", @"\");
				}
				else
					return _linkRemotePath;
			}
			set { _linkRemotePath = value; }
		}

		public string DisplayName
		{
			get
			{
				if (IsDead && Parent.Parent.Parent.EnableInactiveLinks)
				{
					if (Parent.Parent.Parent.InactiveLinksBoldWarning)
					{
						if (!Name.Contains("INACTIVE!"))
							return "INACTIVE! " + Name;
						else
							return Name;
					}
					else if (Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak)
						return string.Empty;
					else
						return Name;
				}
				else if (ExpirationDateOptions.EnableExpirationDate && ExpirationDateOptions.LabelLinkWhenExpired && IsExpired)
					return "EXPIRED! " + Name;
				else
					return Name;
			}
			set { Name = value; }
		}

		public string NameWithExtension
		{
			get
			{
				if (Type == FileTypes.Url || Type == FileTypes.Network || Type == FileTypes.Folder)
					return Name;
				else if (Type == FileTypes.LineBreak)
					return string.Empty;
				else
					return Path.GetFileName(OriginalPath);
			}
		}

		public string NameWithoutExtesion
		{
			get
			{
				if (Type == FileTypes.Url || Type == FileTypes.Network || Type == FileTypes.Folder)
					return Name;
				else if (Type == FileTypes.LineBreak)
					return string.Empty;
				else
				{
					return Path.GetFileNameWithoutExtension(OriginalPath);
				}
			}
		}

		public string Extension
		{
			get
			{
				switch (Type)
				{
					case FileTypes.BuggyPresentation:
					case FileTypes.FriendlyPresentation:
					case FileTypes.MediaPlayerVideo:
					case FileTypes.Other:
					case FileTypes.Presentation:
					case FileTypes.QuickTimeVideo:
					case FileTypes.Excel:
					case FileTypes.PDF:
					case FileTypes.Word:
					case FileTypes.OvernightsLink:
						return Path.GetExtension(OriginalPath);
					default:
						return string.Empty;
				}
			}
		}

		public string Note
		{
			get
			{
				if (IsDead && Parent.Parent.Parent.EnableInactiveLinks && (Parent.Parent.Parent.InactiveLinksBoldWarning || Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak))
					return string.Empty;
				else
					return _note;
			}
			set { _note = value; }
		}

		public bool DisplayAsBold
		{
			get
			{
				if (IsDead && Parent.Parent.Parent.EnableInactiveLinks && Parent.Parent.Parent.InactiveLinksBoldWarning)
					return true;
				else if (ExpirationDateOptions.EnableExpirationDate && IsExpired && ExpirationDateOptions.LabelLinkWhenExpired)
					return true;
				else
					return IsBold;
			}
		}

		public bool IsExpired
		{
			get
			{
				if (ExpirationDateOptions.EnableExpirationDate && ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
					return ((long)ExpirationDateOptions.ExpirationDate.Subtract(DateTime.Now).TotalMilliseconds) < 0;
				else
					return false;
			}
		}

		public Image Widget
		{
			get
			{
				if (EnableWidget && _widget != null)
					return _widget;
				else if (Parent != null)
					return Parent.Parent.Parent.AutoWidgets.Where(x => x.Extension.ToLower().Equals(!string.IsNullOrEmpty(Extension) ? Extension.Substring(1).ToLower() : string.Empty)).Select(y => y.Widget).FirstOrDefault();
				else
					return null;
			}
			set { _widget = value; }
		}

		public string Format
		{
			get
			{
				string format = string.Empty;
				switch (Extension.Replace(".", string.Empty).ToLower())
				{
					case "ppt":
					case "pptx":
						format = "ppt";
						break;
					case "doc":
					case "docx":
						format = "doc";
						break;
					case "xls":
					case "xlsx":
						format = "xls";
						break;
					case "pdf":
						format = "pdf";
						break;
					case "mpeg":
					case "wmv":
					case "avi":
					case "wmz":
					case "mpg":
					case "asf":
					case "mov":
					case "m4v":
					case "flv":
					case "ogv":
					case "ogm":
					case "ogx":
						format = "video";
						break;
					case "mp4":
						format = "mp4";
						break;
					case "png":
						format = "png";
						break;
					case "jpg":
					case "jpeg":
						format = "jpeg";
						break;
					case "url":
						format = "url";
						break;
					default:
						format = "other";
						break;
				}
				return format;
			}
		}

		public virtual ILibraryLink Clone(LibraryFolder parent)
		{
			var file = new LibraryLink(parent);
			file.OriginalPath = _linkLocalPath;
			file.Name = Name;
			file.Note = Note;
			file.Order = Order;
			file.IsBold = IsBold;
			file.EnableWidget = EnableWidget;
			file.Widget = Widget;
			file.RootId = RootId;
			file.RelativePath = RelativePath;
			file.Type = Type;
			file.AddDate = AddDate;
			file.SearchTags = SearchTags;
			file.CustomKeywords = CustomKeywords;
			file.ExpirationDateOptions = ExpirationDateOptions;
			file.PresentationProperties = PresentationProperties;
			file.LineBreakProperties = LineBreakProperties.Clone(file);
			file.AttachmentProperties = AttachmentProperties.Clone(file);
			file.BannerProperties = BannerProperties.Clone(file);
			file.FileCard = FileCard.Clone(file);
			return file;
		}

		public virtual string Serialize()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.AppendLine(@"<DisplayName>" + Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</DisplayName>");
			result.AppendLine(@"<Note>" + _note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
			result.AppendLine(@"<IsDead>" + IsDead + @"</IsDead>");
			result.AppendLine(@"<IsBold>" + IsBold + @"</IsBold>");
			result.AppendLine(@"<RootId>" + RootId.ToString() + @"</RootId>");
			result.AppendLine(@"<LocalPath>" + _linkRemotePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</LocalPath>");
			result.AppendLine(@"<RelativePath>" + RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RelativePath>");
			result.AppendLine(@"<Type>" + (int)Type + @"</Type>");
			result.AppendLine(@"<Order>" + Order + @"</Order>");
			result.AppendLine(@"<EnableWidget>" + EnableWidget + @"</EnableWidget>");
			result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
			result.AppendLine(SearchTags.Serialize());
			result.AppendLine(@"<ExpirationDateOptions>" + ExpirationDateOptions.Serialize() + @"</ExpirationDateOptions>");
			result.AppendLine(@"<FileCard>" + FileCard.Serialize() + @"</FileCard>");
			if (PreviewContainer != null)
				result.AppendLine(@"<PreviewContainer>" + PreviewContainer.Serialize() + @"</PreviewContainer>");
			if (PresentationProperties != null)
				result.AppendLine(@"<PresentationProperties>" + PresentationProperties.Serialize() + @"</PresentationProperties>");
			if (LineBreakProperties != null)
				result.AppendLine(@"<LineBreakProperties>" + LineBreakProperties.Serialize() + @"</LineBreakProperties>");
			if (BannerProperties != null && BannerProperties.Configured)
				result.AppendLine(@"<BannerProperties>" + BannerProperties.Serialize() + @"</BannerProperties>");
			return result.ToString();
		}

		public virtual void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt = 0;
			DateTime tempDate = DateTime.Now;
			Guid tempGuid;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "DisplayName":
						Name = childNode.InnerText;
						break;
					case "Note":
						_note = childNode.InnerText;
						break;
					case "IsBold":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsBold = tempBool;
						break;
					case "IsDead":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsDead = tempBool;
						break;
					case "RootId":
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							RootId = tempGuid;
						break;
					case "LocalPath":
						_linkRemotePath = childNode.InnerText;
						break;
					case "RelativePath":
						RelativePath = childNode.InnerText;
						break;
					case "Type":
						if (int.TryParse(childNode.InnerText, out tempInt))
						{
							Type = (FileTypes)tempInt;
							if (Type == FileTypes.LineBreak)
								LineBreakProperties = new LineBreakProperties(this);
						}
						break;
					case "Order":
						if (int.TryParse(childNode.InnerText, out tempInt))
							Order = tempInt;
						break;
					case "EnableWidget":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableWidget = tempBool;
						break;
					case "Widget":
						if (string.IsNullOrEmpty(childNode.InnerText) && EnableWidget)
							_widget = null;
						else if (!string.IsNullOrEmpty(childNode.InnerText))
							_widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "AddDate":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							AddDate = tempDate;
						break;
					case "LastChanged":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							LastChanged = tempDate;
						break;
					case "SearchTags":
						SearchTags.Deserialize(childNode);
						break;
					case "ExpirationDateOptions":
						ExpirationDateOptions.Deserialize(childNode);
						break;
					case "FileCard":
						FileCard.Deserialize(childNode);
						break;
					case "PreviewContainer":
						PreviewContainer = new PresentationPreviewContainer(this);
						PreviewContainer.Deserialize(childNode);
						break;
					case "PresentationProperties":
						PresentationProperties = new PresentationProperties();
						PresentationProperties.Deserialize(childNode);
						break;
					case "LineBreakProperties":
						LineBreakProperties = new LineBreakProperties(this);
						LineBreakProperties.Font = new Font(Parent.WindowFont, Parent.WindowFont.Style);
						LineBreakProperties.Deserialize(childNode);
						break;
					case "BannerProperties":
						BannerProperties = new BannerProperties(this);
						BannerProperties.Deserialize(childNode);
						break;

						#region Compatibility with old versions
					case "EnableBanner":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_oldEnableBanner = tempBool;
						break;
					case "Banner":
						if (string.IsNullOrEmpty(childNode.InnerText))
							_oldBanner = null;
						else
							_oldBanner = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
						#endregion
				}
			}

			if (BannerProperties == null)
				InitBannerProperties();

			if (Type == FileTypes.Other || Type == FileTypes.MediaPlayerVideo || Type == FileTypes.QuickTimeVideo)
				SetProperties();
		}
		#endregion

		public void InitBannerProperties()
		{
			BannerProperties = new BannerProperties(this);
			try
			{
				BannerProperties.Font = new Font(Parent.WindowFont, Parent.WindowFont.Style);
				BannerProperties.ForeColor = Parent.ForeWindowColor;
				BannerProperties.Text = DisplayName;

				BannerProperties.Enable = _oldEnableBanner;
				BannerProperties.Image = _oldBanner;
				if (LineBreakProperties != null)
				{
					BannerProperties.Enable |= LineBreakProperties.EnableBanner;
					if (LineBreakProperties.Banner != null)
						BannerProperties.Image = LineBreakProperties.Banner;
				}
			}
			catch {}
		}

		public void SetProperties()
		{
			switch (Extension.ToUpper())
			{
				case ".PPT":
				case ".PPTX":
					Type = FileTypes.Presentation;
					break;
				case ".DOC":
				case ".DOCX":
					Type = FileTypes.Word;
					break;
				case ".XLS":
				case ".XLSX":
					Type = FileTypes.Excel;
					break;
				case ".PDF":
					Type = FileTypes.PDF;
					break;
				case ".MPEG":
				case ".WMV":
				case ".AVI":
				case ".WMZ":
				case ".MPG":
					Type = FileTypes.MediaPlayerVideo;
					break;
				case ".ASF":
				case ".MOV":
				case ".MP4":
				case ".M4V":
				case ".FLV":
				case ".OGV":
				case ".OGM":
				case ".OGX":
					Type = FileTypes.QuickTimeVideo;
					break;
				case ".URL":
					Type = FileTypes.Url;
					break;
				default:
					Type = FileTypes.Other;
					break;
			}
		}

		public void RemoveFromCollection()
		{
			Parent.Files.Remove(this);
		}

		private void GetLocalCopy()
		{
			if (LinkAvailable)
			{
				if (SettingsManager.Instance.UseRemoteConnection)
				{
					var thread = new Thread(delegate()
						                        {
							                        switch (Type)
							                        {
								                        case FileTypes.BuggyPresentation:
								                        case FileTypes.Excel:
								                        case FileTypes.FriendlyPresentation:
								                        case FileTypes.MediaPlayerVideo:
								                        case FileTypes.Other:
								                        case FileTypes.Presentation:
								                        case FileTypes.PDF:
								                        case FileTypes.QuickTimeVideo:
								                        case FileTypes.Word:
								                        case FileTypes.OvernightsLink:
									                        _linkLocalPath = Path.Combine(SettingsManager.Instance.LocalLibraryCacheFolder, NameWithExtension);
									                        try
									                        {
										                        File.Copy(OriginalPath, _linkLocalPath, true);
									                        }
									                        catch
									                        {
										                        _linkLocalPath = string.Empty;
									                        }
									                        break;
								                        case FileTypes.Folder:
									                        _linkLocalPath = OriginalPath;
									                        break;
								                        default:
									                        _linkLocalPath = string.Empty;
									                        break;
							                        }
						                        });
					thread.Start();
					Application.DoEvents();
					while (thread.IsAlive)
						Application.DoEvents();
				}
				else
					_linkLocalPath = OriginalPath;
			}
			else
				_linkLocalPath = string.Empty;
		}
	}

	public class LibraryFolderLink : LibraryLink, ILibraryFolderLink
	{
		public LibraryFolderLink(LibraryFolder parent)
			: base(parent)
		{
			FolderContent = new List<ILibraryLink>();
		}

		#region ILibraryFolderLink Members
		public List<ILibraryLink> FolderContent { get; private set; }

		public override ILibraryLink Clone(LibraryFolder parent)
		{
			var file = new LibraryFolderLink(parent);
			file.OriginalPath = _linkLocalPath;
			file.Name = Name;
			file.Note = Note;
			file.Order = Order;
			file.IsBold = IsBold;
			file.EnableWidget = EnableWidget;
			file.Widget = Widget;
			file.RootId = RootId;
			file.RelativePath = RelativePath;
			file.Type = Type;
			file.AddDate = AddDate;
			file.SearchTags = SearchTags;
			file.CustomKeywords = CustomKeywords;
			file.ExpirationDateOptions = ExpirationDateOptions;
			file.PresentationProperties = PresentationProperties;
			file.LineBreakProperties = LineBreakProperties.Clone(file);
			file.AttachmentProperties = AttachmentProperties.Clone(file);
			file.BannerProperties = BannerProperties.Clone(file);
			file.FileCard = FileCard.Clone(file);
			file.FolderContent.AddRange(FolderContent.Select(x => x.Clone(parent)));
			return file;
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			XmlNode contentNode = node.SelectSingleNode("FolderContent");
			if (contentNode != null)
				foreach (XmlNode fileNode in contentNode)
				{
					ILibraryLink file = Parent.Parent.Parent.GetLinkInstance(Parent, fileNode);
					file.Deserialize(fileNode);
					FolderContent.Add(file);
				}

			UpdateFolderContent();
		}

		public override string Serialize()
		{
			UpdateFolderContent();

			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<FolderContent>");
			foreach (ILibraryLink link in FolderContent)
				result.AppendLine(@"<File>" + link.Serialize() + @"</File>");
			result.AppendLine(@"</FolderContent>");
			return result.ToString();
		}
		#endregion

		public bool IsPreviewContainerAlive(IPreviewContainer previewContainer)
		{
			bool alive = false;
			foreach (ILibraryLink file in FolderContent)
			{
				alive = file.OriginalPath.ToLower().Equals(previewContainer.OriginalPath.ToLower());
				if (!alive && file is LibraryFolderLink)
					alive = (file as LibraryFolderLink).IsPreviewContainerAlive(previewContainer);
				if (alive)
					break;
			}
			return alive;
		}

		public void UpdateFolderContent()
		{
			var existedPaths = new List<string>();
			if (Directory.Exists(OriginalPath))
			{
				foreach (DirectoryInfo folder in Directory.GetDirectories(OriginalPath).Select(folderPath => new DirectoryInfo(folderPath)))
				{
					existedPaths.Add(folder.FullName);
					if (FolderContent.Any(x => x.OriginalPath.ToLower().Equals(folder.FullName.ToLower()))) continue;
					var libraryFile = new LibraryFolderLink(Parent);
					libraryFile.Name = folder.Name;
					libraryFile.RootId = RootId;
					RootFolder rootFolder = Parent.Parent.Parent.GetRootFolder(RootId);
					libraryFile.RelativePath = (rootFolder.IsDrive ? @"\" : string.Empty) + folder.FullName.Replace(rootFolder.Folder.FullName, string.Empty);
					libraryFile.Type = FileTypes.Folder;
					libraryFile.InitBannerProperties();
					FolderContent.Add(libraryFile);
				}
				foreach (FileInfo file in Directory.GetFiles(OriginalPath).Select(filePath => new FileInfo(filePath)))
				{
					existedPaths.Add(file.FullName);
					if (FolderContent.Any(x => x.OriginalPath.ToLower().Equals(file.FullName.ToLower()))) continue;
					var libraryFile = new LibraryLink(Parent);
					libraryFile.Name = file.Name;
					libraryFile.RootId = RootId;
					RootFolder rootFolder = Parent.Parent.Parent.GetRootFolder(RootId);
					libraryFile.RelativePath = (rootFolder.IsDrive ? @"\" : string.Empty) + file.FullName.Replace(rootFolder.Folder.FullName, string.Empty);
					libraryFile.SetProperties();
					libraryFile.InitBannerProperties();
					libraryFile.Parent.Parent.Parent.GetPreviewContainer(libraryFile.OriginalPath);
					FolderContent.Add(libraryFile);
				}
			}
			FolderContent.RemoveAll(x => !existedPaths.Any(y => y.ToLower().Equals(x.OriginalPath.ToLower())));
			for (int i = 0; i < FolderContent.Count; i++)
				FolderContent[i].Order = i;
		}
	}
}