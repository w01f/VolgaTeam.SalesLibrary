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
		private bool _linkAvailabel;
		private bool _linkAvailabilityChecked;

		protected string _linkLocalPath = string.Empty;
		private string _linkRemotePath = string.Empty;
		private Image _widget;

		public LibraryLink(LibraryFolder parent)
		{
			Name = string.Empty;
			Parent = parent;
			RootId = Guid.Empty;
			Identifier = Guid.NewGuid();
			RelativePath = string.Empty;
			Type = FileTypes.Other;
			Order = 0;
			IsDead = false;
			CriteriaOverlap = string.Empty;
			ExtendedProperties = new LinkExtendedProperties(this);
			SearchTags = new LibraryFileSearchTags();
			ExpirationDateOptions = new ExpirationDateOptions();
			SuperFilters = new List<SuperFilter>();
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
		public bool IsDead { get; set; }
		public bool EnableWidget { get; set; }
		public string CriteriaOverlap { get; set; }
		public DateTime AddDate { get; set; }
		public DateTime LastChanged { get; set; }
		
		public LinkExtendedProperties ExtendedProperties { get; set; }
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
				else if (ExpirationDateOptions.EnableExpirationDate && ExpirationDateOptions.LabelLinkWhenExpired && ExpirationDateOptions.IsExpired)
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
			throw new NotImplementedException();
		}

		public string Serialize()
		{
			throw new NotImplementedException();
		}

		public virtual void Deserialize(XmlNode node)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}