using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class LinkExtendedProperties
	{
		private readonly LibraryLink _parent;

		private string _note = String.Empty;
		public string Note
		{
			get
			{
				if (_parent.IsDead && _parent.Parent.Parent.Parent.EnableInactiveLinks && (_parent.Parent.Parent.Parent.InactiveLinksBoldWarning || _parent.Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak))
					return string.Empty;
				return _note;
			}
			set
			{
				if (_note != value)
					_parent.LastChanged = DateTime.Now;
				_note = value;
			}
		}

		private string _hoverNote = String.Empty;
		public string HoverNote
		{
			get { return _hoverNote; }
			set
			{
				if (_hoverNote != value)
					_parent.LastChanged = DateTime.Now;
				_hoverNote = value;
			}
		}

		private bool _isBold;
		public bool IsBold
		{
			get { return _isBold; }
			set
			{
				if (_isBold != value)
					_parent.LastChanged = DateTime.Now;
				_isBold = value;
			}
		}

		private bool _isSpecialFormat;
		public bool IsSpecialFormat
		{
			get { return _isSpecialFormat; }
			set
			{
				if (_isSpecialFormat != value)
					_parent.LastChanged = DateTime.Now;
				_isSpecialFormat = value;
			}
		}

		private Font _font;
		public Font Font
		{
			get { return _font; }
			set
			{
				if (_font != value)
					_parent.LastChanged = DateTime.Now;
				_font = value;
			}
		}

		private Color _foreColor = Color.Black;
		public Color ForeColor
		{
			get { return _foreColor; }
			set
			{
				if (_foreColor != value)
					_parent.LastChanged = DateTime.Now;
				_foreColor = value;
			}
		}

		private bool _forcePreview;
		public bool ForcePreview
		{
			get { return _forcePreview; }
			set
			{
				if (_forcePreview != value)
					_parent.LastChanged = DateTime.Now;
				_forcePreview = value;
			}
		}

		private bool _isUrl365;
		public bool IsUrl365
		{
			get { return _isUrl365; }
			set
			{
				if (_isUrl365 != value)
					_parent.LastChanged = DateTime.Now;
				_isUrl365 = value;
			}
		}

		private bool _isForbidden;
		public bool IsForbidden
		{
			get { return _isForbidden; }
			set
			{
				if (_isForbidden != value)
					_parent.LastChanged = DateTime.Now;
				_isForbidden = value;
			}
		}

		private bool _isRestricted;
		public bool IsRestricted
		{
			get { return _isRestricted; }
			set
			{
				if (_isRestricted != value)
					_parent.LastChanged = DateTime.Now;
				_isRestricted = value;
			}
		}

		private bool _noShare;
		public bool NoShare
		{
			get { return _noShare; }
			set
			{
				if (_noShare != value)
					_parent.LastChanged = DateTime.Now;
				_noShare = value;
			}
		}

		private string _assignedUsers;
		public string AssignedUsers
		{
			get { return _assignedUsers; }
			set
			{
				if (_assignedUsers != value)
					_parent.LastChanged = DateTime.Now;
				_assignedUsers = value;
			}
		}

		private string _deniedUsers;
		public string DeniedUsers
		{
			get { return _deniedUsers; }
			set
			{
				if (_deniedUsers != value)
					_parent.LastChanged = DateTime.Now;
				_deniedUsers = value;
			}
		}

		private bool _generatePreviewImages;
		public bool GeneratePreviewImages
		{
			get { return _generatePreviewImages; }
			set
			{
				if (_generatePreviewImages != value)
					_parent.LastChanged = DateTime.Now;
				_generatePreviewImages = value;
			}
		}

		private bool _generateContentText;
		public bool GenerateContentText
		{
			get { return _generateContentText; }
			set
			{
				if (_generateContentText != value)
					_parent.LastChanged = DateTime.Now;
				_generateContentText = value;
			}
		}

		public bool IsRegularFormat
		{
			get { return !(_isBold || _isSpecialFormat); }
		}

		public bool DisplayAsBold
		{
			get
			{
				if (_parent.IsDead && _parent.Parent.Parent.Parent.EnableInactiveLinks && _parent.Parent.Parent.Parent.InactiveLinksBoldWarning)
					return true;
				if (_parent.ExpirationDateOptions.EnableExpirationDate && _parent.ExpirationDateOptions.IsExpired && _parent.ExpirationDateOptions.LabelLinkWhenExpired)
					return true;
				return _isBold;
			}
		}

		public LinkExtendedProperties(LibraryLink parent)
		{
			_parent = parent;
			_generatePreviewImages = true;
			_generateContentText = true;
		}

		public void Deserialize(XmlNode node)
		{
			var converter = new FontConverter();
			foreach (var childNode in node.ChildNodes.OfType<XmlNode>())
			{
				switch (childNode.Name)
				{
					case "Note":
						_note = childNode.InnerText;
						break;
					case "HoverNote":
						_hoverNote = childNode.InnerText;
						break;
					case "IsBold":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_isBold = temp;
						}
						break;
					case "IsSpecialFormat":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_isSpecialFormat = temp;
						}
						break;
					case "Font":
						try
						{
							Font = (Font)converter.ConvertFromString(childNode.InnerText);
						}
						catch { }
						break;
					case "ForeColor":
						{
							int temp;
							if (int.TryParse(childNode.InnerText, out temp))
								_foreColor = Color.FromArgb(temp);
						}
						break;
					case "ForcePreview":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_forcePreview = temp;
						}
						break;
					case "IsUrl365":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_isUrl365 = temp;
						}
						break;
					case "IsForbidden":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_isForbidden = temp;
						}
						break;
					case "IsRestricted":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_isRestricted = temp;
						}
						break;
					case "NoShare":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_noShare = temp;
						}
						break;
					case "AssignedUsers":
						_assignedUsers = childNode.InnerText;
						break;
					case "DeniedUsers":
						_deniedUsers = childNode.InnerText;
						break;
					case "GeneratePreviewImages":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_generatePreviewImages = temp;
						}
						break;
					case "GenerateContentText":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								_generateContentText = temp;
						}
						break;
				}
			}
		}
	}
}
