using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
			PreviewContainer = null;
			SetProperties();
		}

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
		public PresentationPreviewContainer PreviewContainer { get; set; }

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
					default:
						format = "other";
						break;
				}
				return format;
			}
		}

		public virtual ILibraryLink Clone(LibraryFolder parent)
		{
			throw new NotImplementedException();
		}

		public virtual string Serialize()
		{
			throw new NotImplementedException();
		}

		public virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool = false;
				int tempInt = 0;
				DateTime tempDate;
				Guid tempGuid;
				switch (childNode.Name)
				{
					case "Identifier":
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "DisplayName":
						Name = childNode.InnerText;
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
				if (LineBreakProperties != null)
				{
					BannerProperties.Enable |= LineBreakProperties.EnableBanner;
					if (LineBreakProperties.Banner != null)
						BannerProperties.Image = LineBreakProperties.Banner;
				}
			}
			catch { }
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
				case ".MP4":
					Type = FileTypes.MediaPlayerVideo;
					break;
				case ".ASF":
				case ".MOV":
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
		public List<ILibraryLink> FolderContent { get; private set; }

		public bool IsPreviewContainerAlive(IPreviewContainer previewContainer)
		{
			var alive = false;
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
					if (Widget != null) continue;
					EnableWidget = true;
					Widget = Properties.Resources.FolderContentFolder;
				}
			ApplyWidgetsForFolderContent();
		}

		private void ApplyWidgetsForFolderContent()
		{
			foreach (var link in FolderContent)
			{
				link.EnableWidget = true;
				switch (link.Type)
				{
					case FileTypes.QuickTimeVideo:
						link.Widget = Properties.Resources.FolderContentMp4;
						break;
					case FileTypes.BuggyPresentation:
					case FileTypes.FriendlyPresentation:
					case FileTypes.Presentation:
						link.Widget = Properties.Resources.FolderContentPptx;
						break;
					case FileTypes.Excel:
						link.Widget = Properties.Resources.FolderContentXlsx;
						break;
					case FileTypes.Folder:
						link.Widget = Properties.Resources.FolderContentFolder;
						break;
					case FileTypes.MediaPlayerVideo:
						link.Widget = Properties.Resources.FolderContentWmv;
						break;
					case FileTypes.PDF:
						link.Widget = Properties.Resources.FolderContentPdf;
						break;
					case FileTypes.Word:
						link.Widget = Properties.Resources.FolderContentDocx;
						break;
				}
			}
		}
	}
}