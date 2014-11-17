using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using SalesDepot.CoreObjects.InteropClasses;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class LibraryLink : ILibraryLink
	{
		private bool _enableWidget;
		private bool _isDead;
		private DateTime _lastChanged = DateTime.MinValue;
		protected string _linkLocalPath = string.Empty;
		private string _name = string.Empty;
		private int _order;
		private Image _widget;

		#region Compatibility with old versions
		private Image _oldBanner;
		private bool _oldEnableBanner;
		#endregion

		public LibraryLink(LibraryFolder parent)
		{
			Parent = parent;
			RootId = Guid.Empty;
			Identifier = Guid.NewGuid();
			RelativePath = string.Empty;
			Type = FileTypes.Other;
			AddDate = DateTime.Now;
			ExtendedProperties = new LinkExtendedProperties(this);
			SearchTags = new LibraryFileSearchTags();
			ExpirationDateOptions = new ExpirationDateOptions();
			CustomKeywords = new CustomKeywords();
			SuperFilters = new List<SuperFilter>();

			SetProperties();
		}

		public string PropertiesName
		{
			get
			{
				if (Type == FileTypes.Url || Type == FileTypes.Network || Type == FileTypes.Folder || Type == FileTypes.LineBreak)
					return _name;
				return Path.GetFileName(OriginalPath);
			}
		}

		public string PreviewStoragePath
		{
			get { return Parent.Parent.Parent.Folder.FullName; }
		}

		#region ILibraryFile Members
		public LibraryFolder Parent { get; set; }
		public Guid RootId { get; set; }
		public Guid Identifier { get; set; }
		public string RelativePath { get; set; }
		public FileTypes Type { get; set; }
		public DateTime AddDate { get; set; }
		public string CriteriaOverlap { get; set; }
		public LinkExtendedProperties ExtendedProperties { get; set; }
		public LibraryFileSearchTags SearchTags { get; set; }
		public SearchGroup CustomKeywords { get; protected set; }
		public List<SuperFilter> SuperFilters { get; protected set; }
		public ExpirationDateOptions ExpirationDateOptions { get; set; }
		public PresentationProperties PresentationProperties { get; set; }
		public LineBreakProperties LineBreakProperties { get; set; }
		public BannerProperties BannerProperties { get; set; }
		
		public PresentationPreviewContainer PreviewContainer { get; set; }

		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
					LastChanged = DateTime.Now;
				_name = value;
			}
		}

		public int Order
		{
			get { return _order; }
			set
			{
				if (_order != value)
					LastChanged = DateTime.Now;
				_order = value;
			}
		}

		public bool IsDead
		{
			get { return _isDead; }
			set
			{
				if (_isDead != value)
					LastChanged = DateTime.Now;
				_isDead = value;
			}
		}

		public bool EnableWidget
		{
			get { return _enableWidget; }
			set
			{
				if (_enableWidget != value)
					LastChanged = DateTime.Now;
				_enableWidget = value;
			}
		}

		public Image Widget
		{
			get
			{
				if (_enableWidget && _widget != null)
					return _widget;
				return Parent != null ? Parent.Parent.Parent.AutoWidgets.Where(x => x.Extension.ToLower().Equals(!string.IsNullOrEmpty(Extension) ? Extension.Substring(1).ToLower() : string.Empty)).Select(y => y.Widget).FirstOrDefault() : null;
			}
			set
			{
				if (_widget != value)
					LastChanged = DateTime.Now;
				_widget = value;
			}
		}

		public DateTime LastChanged
		{
			get { return _lastChanged; }
			set
			{
				_lastChanged = value;
				Parent.LastChanged = _lastChanged;
			}
		}

		public string OriginalPath
		{
			get
			{
				if (string.IsNullOrEmpty(_linkLocalPath))
				{
					if (Type == FileTypes.Url || Type == FileTypes.Network)
						return RelativePath;
					if (Type == FileTypes.LineBreak)
						return string.Empty;
					return ((Parent != null ? Parent.Parent.Parent.GetRootFolder(RootId).Folder.FullName : string.Empty) + @"\" + RelativePath).Replace(@"\\", @"\").Replace(@"\\", @"\");
				}
				return _linkLocalPath;
			}
			set { _linkLocalPath = value; }
		}

		public string WebPath
		{
			get { return Parent.Parent.Parent.RootFolder.RootId == RootId ? RelativePath : (@"\" + (Path.Combine(Constants.ExtraFoldersRootFolderName, RootId.ToString()) + @"\" + RelativePath).Replace(@"\\", @"\").Replace(@"\\", @"\")); }
		}

		public string DisplayName
		{
			get
			{
				if (_isDead && Parent.Parent.Parent.EnableInactiveLinks)
				{
					if (Parent.Parent.Parent.InactiveLinksBoldWarning)
					{
						if (!_name.Contains("INACTIVE!"))
							return "INACTIVE! " + _name;
						return _name;
					}
					return Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak ? string.Empty : _name;
				}
				if (ExpirationDateOptions.EnableExpirationDate && ExpirationDateOptions.LabelLinkWhenExpired && ExpirationDateOptions.IsExpired)
					return "EXPIRED! " + _name;
				return _name;
			}
		}

		public string NameWithExtension
		{
			get
			{
				if (Type == FileTypes.Url || Type == FileTypes.Network || Type == FileTypes.Folder)
					return _name;
				return Type == FileTypes.LineBreak ? string.Empty : Path.GetFileName(OriginalPath);
			}
		}

		public string NameWithoutExtesion
		{
			get
			{
				if (Type == FileTypes.Url || Type == FileTypes.Network || Type == FileTypes.Folder)
					return _name;
				return Type == FileTypes.LineBreak ? string.Empty : Path.GetFileNameWithoutExtension(OriginalPath);
			}
		}

		public string Extension
		{
			get
			{
				switch (Type)
				{
					case FileTypes.Presentation:
					case FileTypes.BuggyPresentation:
					case FileTypes.FriendlyPresentation:
					case FileTypes.MediaPlayerVideo:
					case FileTypes.Other:
					case FileTypes.QuickTimeVideo:
						return Path.GetExtension(OriginalPath);
					default:
						return string.Empty;
				}
			}
		}

		public bool HasTags
		{
			get { return HasCategories || HasKeywords; }
		}

		public bool HasCategories
		{
			get { return !string.IsNullOrEmpty(SearchTags.AllTags); }
		}

		public bool HasKeywords
		{
			get { return CustomKeywords.Tags.Count > 0; }
		}

		public string Format
		{
			get
			{
				string format;
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
					case "wmv":
						format = "wmv";
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
					case "key":
						format = "key";
						break;
					case "mp3":
						format = "mp3";
						break;
					default:
						switch (Type)
						{
							case FileTypes.Url:
								format = ExtendedProperties.IsUrl365 ? "url365" : "url";
								break;
							default:
								format = "other";
								break;
						}
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
			file.Order = Order;
			file.EnableWidget = EnableWidget;
			file.Widget = Widget;
			file.RootId = RootId;
			file.RelativePath = RelativePath;
			file.Type = Type;
			file.AddDate = AddDate;
			file.ExtendedProperties = ExtendedProperties.Clone(file);
			file.SearchTags = SearchTags;
			file.CustomKeywords = CustomKeywords;
			file.ExpirationDateOptions = ExpirationDateOptions;
			file.PresentationProperties = PresentationProperties;
			if (LineBreakProperties != null)
				file.LineBreakProperties = LineBreakProperties.Clone(file);
			file.BannerProperties = BannerProperties.Clone(file);
			file.SuperFilters.AddRange(SuperFilters.Select(sf => new SuperFilter() { Name = sf.Name }));
			return file;
		}

		public virtual string Serialize()
		{
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier.ToString() + @"</Identifier>");
			result.AppendLine(@"<DisplayName>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</DisplayName>");
			result.AppendLine(@"<IsDead>" + _isDead + @"</IsDead>");
			result.AppendLine(@"<RootId>" + RootId + @"</RootId>");
			result.AppendLine(@"<LocalPath>" + _linkLocalPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</LocalPath>");
			result.AppendLine(@"<RelativePath>" + RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RelativePath>");
			result.AppendLine(@"<Type>" + (int)Type + @"</Type>");
			result.AppendLine(@"<Order>" + _order + @"</Order>");
			result.AppendLine(@"<EnableWidget>" + _enableWidget + @"</EnableWidget>");
			result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
			result.AppendLine(@"<AddDate>" + AddDate + @"</AddDate>");
			result.AppendLine(@"<LastChanged>" + (_lastChanged != DateTime.MinValue ? _lastChanged.ToString() : DateTime.Now.ToString()) + @"</LastChanged>");
			result.AppendLine(@"<ExtendedProperties>" + ExtendedProperties.Serialize() + @"</ExtendedProperties>");
			result.Append(SearchTags.Serialize());
			result.Append(CustomKeywords.Serialize());
			result.AppendLine(@"<SuperFilters>");
			foreach (var superFilter in SuperFilters)
				result.AppendLine(@"<Filter>" + superFilter.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Filter>");
			result.AppendLine(@"</SuperFilters>");
			result.AppendLine(@"<ExpirationDateOptions>" + ExpirationDateOptions.Serialize() + @"</ExpirationDateOptions>");

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
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				int tempInt;
				Guid tempGuid;
				DateTime tempDate;
				switch (childNode.Name)
				{
					case "Identifier":
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "DisplayName":
						_name = childNode.InnerText;
						break;
					case "RootId":
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							RootId = tempGuid;
						break;
					case "LocalPath":
						_linkLocalPath = childNode.InnerText;
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
							_order = tempInt;
						break;
					case "EnableWidget":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_enableWidget = tempBool;
						break;
					case "Widget":
						if (string.IsNullOrEmpty(childNode.InnerText) && _enableWidget)
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
							_lastChanged = tempDate;
						break;

					case "ExtendedProperties":
						ExtendedProperties.Deserialize(childNode);
						break;
					#region Compatibility with old version of Sales Depot
					case "Note":
						ExtendedProperties.Note = childNode.InnerText;
						break;
					case "IsBold":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.IsBold = tempBool;
						break;
					case "ForcePreview":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.ForcePreview = tempBool;
						break;
					case "IsUrl365":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.IsUrl365 = tempBool;
						break;
					case "IsForbidden":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.IsForbidden = tempBool;
						break;
					case "IsRestricted":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.IsRestricted = tempBool;
						break;
					case "NoShare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.NoShare = tempBool;
						break;
					case "AssignedUsers":
						ExtendedProperties.AssignedUsers = childNode.InnerText;
						break;
					case "DeniedUsers":
						ExtendedProperties.DeniedUsers = childNode.InnerText;
						break;
					case "GeneratePreviewImages":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.GeneratePreviewImages = tempBool;
						break;
					case "GenerateContentText":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ExtendedProperties.GenerateContentText = tempBool;
						break;
					#endregion

					case "SearchTags":
						SearchTags.Deserialize(childNode);
						break;
					case BusinessClasses.CustomKeywords.TagName:
						CustomKeywords.Deserialize(childNode);
						break;
					case "SuperFilters":
						SuperFilters.Clear();
						SuperFilters.AddRange(childNode.ChildNodes.OfType<XmlNode>().Select(n => new SuperFilter { Name = n.InnerText }));
						break;
					case "ExpirationDateOptions":
						ExpirationDateOptions.Deserialize(childNode);
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
						LineBreakProperties.Deserialize(childNode);
						break;
					case "BannerProperties":
						BannerProperties = new BannerProperties(this);
						BannerProperties.Deserialize(childNode);
						break;
				}
			}

			if (BannerProperties == null)
				InitBannerProperties();

			SetProperties();

			if ((Type == FileTypes.BuggyPresentation ||
				Type == FileTypes.FriendlyPresentation ||
				Type == FileTypes.Presentation ||
				Type == FileTypes.Other ||
				Type == FileTypes.MediaPlayerVideo ||
				Type == FileTypes.QuickTimeVideo) &&
				!(ExtendedProperties.IsForbidden ||
					!(!ExtendedProperties.IsRestricted ||
					((!String.IsNullOrEmpty(ExtendedProperties.AssignedUsers) ||
					!String.IsNullOrEmpty(ExtendedProperties.DeniedUsers)))))
				)
				Parent.Parent.Parent.GetPreviewContainer(OriginalPath);
		}
		#endregion

		public void InitBannerProperties()
		{
			BannerProperties = new BannerProperties(this);
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

		public void SetProperties()
		{
			if (Type != FileTypes.Folder && Type != FileTypes.LineBreak && Type != FileTypes.Url && Type != FileTypes.Network)
			{
				switch (Extension.ToUpper())
				{
					case ".PPT":
					case ".PPTX":
						Type = FileTypes.Presentation;
						break;
					case ".MPEG":
					case ".WMV":
					case ".AVI":
					case ".WMZ":
						Type = FileTypes.MediaPlayerVideo;
						break;
					case ".ASF":
					case ".MOV":
					case ".MP4":
					case ".MPG":
					case ".M4V":
					case ".FLV":
					case ".OGV":
					case ".OGM":
					case ".OGX":
						Type = FileTypes.QuickTimeVideo;
						break;
					default:
						Type = FileTypes.Other;
						break;
				}
			}
		}

		public void GetPresentationProperties()
		{
			PowerPointHelper.Instance.GetPresentationProperties(this);
		}

		public void CheckIfDead()
		{
			switch (Type)
			{
				case FileTypes.BuggyPresentation:
				case FileTypes.FriendlyPresentation:
				case FileTypes.Presentation:
				case FileTypes.QuickTimeVideo:
				case FileTypes.MediaPlayerVideo:
				case FileTypes.Other:
					IsDead = !File.Exists(OriginalPath);
					break;
				case FileTypes.Folder:
					IsDead = !Directory.Exists(OriginalPath);
					break;
			}
		}

		public void RemoveFromCollection()
		{
			Parent.Files.Remove(this);
			Parent.LastChanged = DateTime.Now;
		}
	}

	public class LibraryFolderLink : LibraryLink, ILibraryFolderLink
	{
		public List<ILibraryLink> FolderContent { get; private set; }

		public bool IsPreviewContainerAlive(IPreviewContainer previewContainer)
		{
			bool alive = false;
			foreach (var file in FolderContent)
			{
				alive = file.OriginalPath.ToLower().Equals(previewContainer.OriginalPath.ToLower());
				if (!alive && file is LibraryFolderLink)
					alive = (file as LibraryFolderLink).IsPreviewContainerAlive(previewContainer);
				if (alive)
					break;
			}
			return alive;
		}

		public LibraryFolderLink(LibraryFolder parent)
			: base(parent)
		{
			FolderContent = new List<ILibraryLink>();
		}

		public override ILibraryLink Clone(LibraryFolder parent)
		{
			var file = new LibraryFolderLink(parent);
			file.OriginalPath = _linkLocalPath;
			file.Name = Name;
			file.Order = Order;
			file.EnableWidget = EnableWidget;
			file.Widget = Widget;
			file.RootId = RootId;
			file.RelativePath = RelativePath;
			file.Type = Type;
			file.AddDate = AddDate;
			file.ExtendedProperties = ExtendedProperties.Clone(file);
			file.CustomKeywords = CustomKeywords;
			file.ExpirationDateOptions = ExpirationDateOptions;
			file.PresentationProperties = PresentationProperties;
			file.LineBreakProperties = LineBreakProperties.Clone(file);
			file.BannerProperties = BannerProperties.Clone(file);
			file.FolderContent.AddRange(FolderContent.Select(x => x.Clone(parent)));
			file.SuperFilters.AddRange(SuperFilters.Select(sf => new SuperFilter() { Name = sf.Name }));
			return file;
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			var contentNode = node.SelectSingleNode("FolderContent");
			if (contentNode != null)
				foreach (XmlNode fileNode in contentNode)
				{
					var file = Parent.Parent.Parent.GetLinkInstance(Parent, fileNode);
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
			foreach (var link in FolderContent)
				result.AppendLine(@"<File>" + link.Serialize() + @"</File>");
			result.AppendLine(@"</FolderContent>");
			return result.ToString();
		}

		public void UpdateFolderContent()
		{
			var existedPaths = new List<string>();
			if (Directory.Exists(OriginalPath))
			{
				foreach (var folder in Directory.GetDirectories(OriginalPath).Select(folderPath => new DirectoryInfo(folderPath)))
				{
					existedPaths.Add(folder.FullName);
					if (FolderContent.Any(x => x.OriginalPath.ToLower().Equals(folder.FullName.ToLower()))) continue;
					var libraryFile = new LibraryFolderLink(Parent);
					libraryFile.Name = folder.Name;
					libraryFile.RootId = RootId;
					var rootFolder = Parent.Parent.Parent.GetRootFolder(RootId);
					libraryFile.RelativePath = (rootFolder.IsDrive ? @"\" : string.Empty) + folder.FullName.Replace(rootFolder.Folder.FullName, string.Empty);
					libraryFile.Type = FileTypes.Folder;
					libraryFile.InitBannerProperties();
					FolderContent.Add(libraryFile);
				}
				foreach (var file in Directory.GetFiles(OriginalPath).Where(x => !x.ToLower().Contains("thumbs.db")).Select(filePath => new FileInfo(filePath)))
				{
					existedPaths.Add(file.FullName);
					if (FolderContent.Any(x => x.OriginalPath.ToLower().Equals(file.FullName.ToLower()))) continue;
					var libraryFile = new LibraryLink(Parent);
					libraryFile.Name = file.Name;
					libraryFile.RootId = RootId;
					var rootFolder = Parent.Parent.Parent.GetRootFolder(RootId);
					libraryFile.RelativePath = (rootFolder.IsDrive ? @"\" : string.Empty) + file.FullName.Replace(rootFolder.Folder.FullName, string.Empty);
					libraryFile.SetProperties();
					libraryFile.InitBannerProperties();
					libraryFile.Parent.Parent.Parent.GetPreviewContainer(libraryFile.OriginalPath);
					libraryFile.GetPresentationProperties();
					libraryFile.PreviewContainer = new PresentationPreviewContainer(libraryFile);
					FolderContent.Add(libraryFile);
				}
			}
			var linksToRemove = FolderContent.Where(x => !existedPaths.Any(y => y.ToLower().Equals(x.OriginalPath.ToLower())));
			foreach (var link in linksToRemove.OfType<LibraryLink>().Where(x => x.PreviewContainer != null))
				link.PreviewContainer.ClearContent();
			FolderContent.RemoveAll(x => !existedPaths.Any(y => y.ToLower().Equals(x.OriginalPath.ToLower())));
			for (int i = 0; i < FolderContent.Count; i++)
				FolderContent[i].Order = i;
		}

		public IEnumerable<LibraryLink> GetWholeContent()
		{
			var wholeContent = new List<LibraryLink>();
			wholeContent.AddRange(FolderContent.Where(x => x.Type != FileTypes.Folder).OfType<LibraryLink>());
			wholeContent.AddRange(FolderContent.Where(x => x.Type == FileTypes.Folder).OfType<LibraryFolderLink>().SelectMany(x => x.GetWholeContent()));
			return wholeContent;
		}
	}
}