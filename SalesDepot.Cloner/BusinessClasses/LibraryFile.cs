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
		private bool _isBold;
		private bool _isDead;
		private DateTime _lastChanged = DateTime.MinValue;
		protected string _linkLocalPath = string.Empty;
		private string _name = string.Empty;
		private string _note = string.Empty;
		private int _order;
		private Image _widget;
		private bool _isForbidden;
		private bool _isRestricted;
		private bool _noShare;
		private string _assignedUsers;
		private bool _doNotGeneratePreview;
		private bool _forcePreview;

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
			SearchTags = new LibraryFileSearchTags();
			ExpirationDateOptions = new ExpirationDateOptions();
			AttachmentProperties = new AttachmentProperties(this);
			FileCard = new FileCard(this);

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
				else
					return Path.GetFileName(OriginalPath);
			}
		}
		public string Content
		{
			get
			{
				IPreviewContainer previewContainer = Parent.Parent.Parent.GetPreviewContainer(OriginalPath);
				if (previewContainer != null)
					return previewContainer.GetTextContent();
				return string.Empty;
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

		public LibraryFileSearchTags SearchTags { get; set; }
		public SearchGroup CustomKeywords { get; protected set; }
		public List<SuperFilter> SuperFilters { get; protected set; }
		public ExpirationDateOptions ExpirationDateOptions { get; set; }
		public PresentationProperties PresentationProperties { get; set; }
		public LineBreakProperties LineBreakProperties { get; set; }
		public BannerProperties BannerProperties { get; set; }
		public virtual AttachmentProperties AttachmentProperties { get; set; }
		public FileCard FileCard { get; set; }

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

		public string Note
		{
			get
			{
				if (_isDead && Parent.Parent.Parent.EnableInactiveLinks && (Parent.Parent.Parent.InactiveLinksBoldWarning || Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak))
					return string.Empty;
				return _note;
			}
			set
			{
				if (_note != value)
					LastChanged = DateTime.Now;
				_note = value;
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

		public bool IsBold
		{
			get { return _isBold; }
			set
			{
				if (_isBold != value)
					LastChanged = DateTime.Now;
				_isBold = value;
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
				if (ExpirationDateOptions.EnableExpirationDate && ExpirationDateOptions.LabelLinkWhenExpired && IsExpired)
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

		public bool DisplayAsBold
		{
			get
			{
				if (_isDead && Parent.Parent.Parent.EnableInactiveLinks && Parent.Parent.Parent.InactiveLinksBoldWarning)
					return true;
				if (ExpirationDateOptions.EnableExpirationDate && IsExpired && ExpirationDateOptions.LabelLinkWhenExpired)
					return true;
				return _isBold;
			}
		}

		public bool IsExpired
		{
			get
			{
				if (ExpirationDateOptions.EnableExpirationDate && ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
					return ((long)ExpirationDateOptions.ExpirationDate.Subtract(DateTime.Now).TotalMilliseconds) < 0;
				return false;
			}
		}

		public bool HasTags
		{
			get { return HasCategories || HasKeywords || HasFileCard || HasFileAttachments || HasWebAttachments; }
		}

		public bool HasCategories
		{
			get { return !string.IsNullOrEmpty(SearchTags.AllTags); }
		}

		public bool HasKeywords
		{
			get { return CustomKeywords.Tags.Count > 0; }
		}

		public bool HasFileCard
		{
			get { return FileCard.Enable; }
		}

		public bool HasFileAttachments
		{
			get { return AttachmentProperties.Enable && AttachmentProperties.FilesAttachments.Count > 0; }
		}

		public bool HasWebAttachments
		{
			get { return AttachmentProperties.Enable && AttachmentProperties.WebAttachments.Count > 0; }
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

		public bool DoNotGeneratePreview
		{
			get { return _doNotGeneratePreview; }
			set
			{
				if (_doNotGeneratePreview != value)
					LastChanged = DateTime.Now;
				_doNotGeneratePreview = value;
			}
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
			file.AssignedUsers = AssignedUsers;
			file.DoNotGeneratePreview = DoNotGeneratePreview;
			file.ForcePreview = ForcePreview;
			file.SearchTags = SearchTags;
			file.CustomKeywords = CustomKeywords;
			file.ExpirationDateOptions = ExpirationDateOptions;
			file.PresentationProperties = PresentationProperties;
			if (LineBreakProperties != null)
				file.LineBreakProperties = LineBreakProperties.Clone(file);
			file.AttachmentProperties = AttachmentProperties.Clone(file);
			file.BannerProperties = BannerProperties.Clone(file);
			file.FileCard = FileCard.Clone(file);
			file.SuperFilters.AddRange(SuperFilters.Select(sf => new SuperFilter() { Name = sf.Name }));
			return file;
		}

		public virtual string Serialize()
		{
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier.ToString() + @"</Identifier>");
			result.AppendLine(@"<DisplayName>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</DisplayName>");
			result.AppendLine(@"<Note>" + _note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
			result.AppendLine(@"<IsBold>" + _isBold + @"</IsBold>");
			result.AppendLine(@"<IsDead>" + _isDead + @"</IsDead>");
			result.AppendLine(@"<RootId>" + RootId + @"</RootId>");
			result.AppendLine(@"<LocalPath>" + _linkLocalPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</LocalPath>");
			result.AppendLine(@"<RelativePath>" + RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RelativePath>");
			result.AppendLine(@"<Type>" + (int)Type + @"</Type>");
			result.AppendLine(@"<Order>" + _order + @"</Order>");
			result.AppendLine(@"<EnableWidget>" + _enableWidget + @"</EnableWidget>");
			result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
			result.AppendLine(@"<AttachmentProperties>" + AttachmentProperties.Serialize() + @"</AttachmentProperties>");
			result.AppendLine(@"<AddDate>" + AddDate + @"</AddDate>");
			result.AppendLine(@"<IsForbidden>" + IsForbidden + @"</IsForbidden>");
			result.AppendLine(@"<IsRestricted>" + IsRestricted + @"</IsRestricted>");
			result.AppendLine(@"<NoShare>" + NoShare + @"</NoShare>");
			result.AppendLine(@"<AssignedUsers>" + (AssignedUsers ?? string.Empty).Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</AssignedUsers>");
			result.AppendLine(@"<DoNotGeneratePreview>" + _doNotGeneratePreview + @"</DoNotGeneratePreview>");
			result.AppendLine(@"<ForcePreview>" + _forcePreview + @"</ForcePreview>");
			result.AppendLine(@"<LastChanged>" + (_lastChanged != DateTime.MinValue ? _lastChanged.ToString() : DateTime.Now.ToString()) + @"</LastChanged>");
			result.Append(SearchTags.Serialize());
			result.Append(CustomKeywords.Serialize());
			result.AppendLine(@"<SuperFilters>");
			foreach (var superFilter in SuperFilters)
				result.AppendLine(@"<Filter>" + superFilter.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Filter>");
			result.AppendLine(@"</SuperFilters>");
			result.AppendLine(@"<ExpirationDateOptions>" + ExpirationDateOptions.Serialize() + @"</ExpirationDateOptions>");
			result.AppendLine(@"<FileCard>" + FileCard.Serialize() + @"</FileCard>");

			#region Compatibility with desktop version of Sales Depot
			if (PreviewContainer != null)
				result.AppendLine(@"<PreviewContainer>" + PreviewContainer.Serialize() + @"</PreviewContainer>");
			#endregion

			if (PresentationProperties != null)
				result.AppendLine(@"<PresentationProperties>" + PresentationProperties.Serialize() + @"</PresentationProperties>");
			if (LineBreakProperties != null)
				result.AppendLine(@"<LineBreakProperties>" + LineBreakProperties.Serialize() + @"</LineBreakProperties>");
			if (BannerProperties != null && BannerProperties.Configured)
			{
				result.AppendLine(@"<BannerProperties>" + BannerProperties.Serialize() + @"</BannerProperties>");

				#region Compatibility with old versions
				result.AppendLine(@"<EnableBanner>" + BannerProperties.Enable.ToString() + @"</EnableBanner>");
				result.AppendLine(@"<Banner>" + Convert.ToBase64String((byte[])converter.ConvertTo(BannerProperties.Image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Banner>");
				#endregion
			}
			else
			{
				#region Compatibility with old versions
				result.AppendLine(@"<EnableBanner>" + _oldEnableBanner.ToString() + @"</EnableBanner>");
				result.AppendLine(@"<Banner>" + Convert.ToBase64String((byte[])converter.ConvertTo(_oldBanner, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Banner>");
				#endregion
			}
			return result.ToString();
		}

		public virtual void Deserialize(XmlNode node)
		{
			bool tempBool;
			int tempInt;
			DateTime tempDate = DateTime.Now;
			Guid tempGuid;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Identifier":
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "DisplayName":
						_name = childNode.InnerText;
						break;
					case "Note":
						_note = childNode.InnerText;
						break;
					case "IsBold":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_isBold = tempBool;
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
					case "AttachmentProperties":
						AttachmentProperties.Deserialize(childNode);
						break;
					case "AddDate":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							AddDate = tempDate;
						break;
					case "IsForbidden":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_isForbidden = tempBool;
						break;
					case "IsRestricted":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_isRestricted = tempBool;
						break;
					case "NoShare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_noShare = tempBool;
						break;
					case "AssignedUsers":
						_assignedUsers = childNode.InnerText;
						break;
					case "LastChanged":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							_lastChanged = tempDate;
						break;
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
					case "FileCard":
						FileCard.Deserialize(childNode);
						break;
					case "DoNotGeneratePreview":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_doNotGeneratePreview = tempBool;
						break;
					case "ForcePreview":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_forcePreview = tempBool;
						break;
					#region Compatibility with old version of Sales Depot
					case "PreviewContainer":
						PreviewContainer = new PresentationPreviewContainer(this);
						PreviewContainer.Deserialize(childNode);
						break;
					case "UniversalPreviewContainer":
						var universalPreviewContainer = new UniversalPreviewContainer(Parent.Parent.Parent);
						universalPreviewContainer.Deserialize(childNode);
						universalPreviewContainer.OriginalPath = OriginalPath;
						if (!Parent.Parent.Parent.PreviewContainers.Any(x => x.OriginalPath.ToLower().Equals(OriginalPath.ToLower())))
							Parent.Parent.Parent.PreviewContainers.Add(universalPreviewContainer);
						break;
					#endregion

					case "PresentationProperties":
						PresentationProperties = new PresentationProperties();
						PresentationProperties.Deserialize(childNode);
						break;
					case "LineBreakProperties":
						LineBreakProperties = new LineBreakProperties(this);
						LineBreakProperties.Font = new Font(Parent.WindowFont, Parent.WindowFont.Style);
						LineBreakProperties.BoldFont = new Font(Parent.WindowFont, FontStyle.Bold);
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
						_oldBanner = string.IsNullOrEmpty(childNode.InnerText) ? null : new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					#endregion
				}
			}

			if (BannerProperties == null)
				InitBannerProperties();

			SetProperties();

			if (Type == FileTypes.BuggyPresentation || Type == FileTypes.FriendlyPresentation || Type == FileTypes.Presentation || Type == FileTypes.Other || Type == FileTypes.MediaPlayerVideo || Type == FileTypes.QuickTimeVideo)
				Parent.Parent.Parent.GetPreviewContainer(OriginalPath);
		}
		#endregion

		#region Compatibility with desktop version of Sales Depot
		public PresentationPreviewContainer PreviewContainer { get; set; }
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
			file.AssignedUsers = AssignedUsers;
			file.DoNotGeneratePreview = DoNotGeneratePreview;
			file.SearchTags = SearchTags;
			file.CustomKeywords = CustomKeywords;
			file.ExpirationDateOptions = ExpirationDateOptions;
			file.PresentationProperties = PresentationProperties;
			file.LineBreakProperties = LineBreakProperties.Clone(file);
			file.AttachmentProperties = AttachmentProperties.Clone(file);
			file.BannerProperties = BannerProperties.Clone(file);
			file.FileCard = FileCard.Clone(file);
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