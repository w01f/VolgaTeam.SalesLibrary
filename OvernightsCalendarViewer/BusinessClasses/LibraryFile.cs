using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OvernightsCalendarViewer.BusinessClasses
{
	public class LibraryLink : ILibraryLink
	{
		private string _assignedUsers;
		private string _deniedUsers;
		private bool _isForbidden;
		private bool _isRestricted;
		private bool _noShare;
		private bool _linkAvailabel;
		private bool _linkAvailabilityChecked;

		protected string _linkLocalPath = string.Empty;
		private string _linkRemotePath = string.Empty;
		private string _note = string.Empty;
		private Image _widget;
		private bool _generatePreviewImages;
		private bool _generateContentText;
		private bool _forcePreview;
		private bool _isUrl365;

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
			SuperFilters = new List<SuperFilter>();
			_generatePreviewImages = true;
			_generateContentText = true;
		}

		public string LocalPath
		{
			get { return OriginalPath; }
		}

		public bool LinkAvailable
		{
			get
			{
				if (!_linkAvailabilityChecked)
				{
					switch (Type)
					{
						case FileTypes.OvernightsLink:
							_linkAvailabel = File.Exists(OriginalPath);
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
		public List<SuperFilter> SuperFilters { get; protected set; }
		public ExpirationDateOptions ExpirationDateOptions { get; set; }
		public PresentationProperties PresentationProperties { get; set; }
		public LineBreakProperties LineBreakProperties { get; set; }
		public BannerProperties BannerProperties { get; set; }

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

		public string WebPath
		{
			get { return string.Empty; }
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
				if (Type == FileTypes.LineBreak)
					return string.Empty;
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
					default:
						format = "other";
						break;
				}
				return format;
			}
		}

		public bool IsForbidden
		{
			get { return _isForbidden; }
			set
			{
				if (_isForbidden != value)
					LastChanged = DateTime.Now;
				_isForbidden = value;
			}
		}

		public bool IsRestricted
		{
			get { return _isRestricted; }
			set
			{
				if (_isRestricted != value)
					LastChanged = DateTime.Now;
				_isRestricted = value;
			}
		}

		public bool NoShare
		{
			get { return _noShare; }
			set
			{
				if (_noShare != value)
					LastChanged = DateTime.Now;
				_noShare = value;
			}
		}

		public string AssignedUsers
		{
			get { return _assignedUsers; }
			set
			{
				if (_assignedUsers != value)
					LastChanged = DateTime.Now;
				_assignedUsers = value;
			}
		}

		public string DeniedUsers
		{
			get { return _deniedUsers; }
			set
			{
				if (_deniedUsers != value)
					LastChanged = DateTime.Now;
				_deniedUsers = value;
			}
		}

		public bool GeneratePreviewImages
		{
			get
			{
				return _generatePreviewImages &&
					(Type == FileTypes.BuggyPresentation ||
					Type == FileTypes.FriendlyPresentation ||
					Type == FileTypes.Presentation ||
					Type == FileTypes.Word ||
					Type == FileTypes.PDF ||
					((Type == FileTypes.Other && new[] { "ppt", "doc", "pdf" }.Contains(Format))));
			}
			set
			{
				if (_generatePreviewImages != value)
					LastChanged = DateTime.Now;
				_generatePreviewImages = value;
			}
		}

		public bool GenerateContentText
		{
			get { return _generateContentText; }
			set
			{
				if (_generateContentText != value)
					LastChanged = DateTime.Now;
				_generateContentText = value;
			}
		}

		public bool DoNotGeneratePreview
		{
			get { return !GeneratePreviewImages && !GenerateContentText; }
		}

		public bool ForcePreview
		{
			get { return _forcePreview; }
			set
			{
				if (_forcePreview != value)
					LastChanged = DateTime.Now;
				_forcePreview = value;
			}
		}

		public bool IsUrl365
		{
			get { return _isUrl365; }
			set
			{
				if (_isUrl365 != value)
					LastChanged = DateTime.Now;
				_isUrl365 = value;
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
			file.IsForbidden = IsForbidden;
			file.IsRestricted = IsRestricted;
			file.NoShare = NoShare;
			file.GeneratePreviewImages = GeneratePreviewImages;
			file.GenerateContentText = GenerateContentText;
			file.ForcePreview = ForcePreview;
			file.IsUrl365 = IsUrl365;
			file.SearchTags = SearchTags;
			file.CustomKeywords = CustomKeywords;
			file.ExpirationDateOptions = ExpirationDateOptions;
			file.PresentationProperties = PresentationProperties;
			file.LineBreakProperties = LineBreakProperties.Clone(file);
			file.BannerProperties = BannerProperties.Clone(file);
			file.SuperFilters.AddRange(SuperFilters.Select(sf => new SuperFilter() { Name = sf.Name }));
			return file;
		}

		public string Serialize()
		{
			return String.Empty;
		}

		public virtual void Deserialize(XmlNode node) { }
		#endregion
	}
}