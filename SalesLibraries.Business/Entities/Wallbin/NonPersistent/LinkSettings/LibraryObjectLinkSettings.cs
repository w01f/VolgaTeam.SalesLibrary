using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LibraryObjectLinkSettings : BaseLinkSettings
	{
		private string _hoverNote;
		public string HoverNote
		{
			get { return _hoverNote; }
			set
			{
				if (_hoverNote != value)
					OnSettingsChanged();
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
					OnSettingsChanged();
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
					OnSettingsChanged();
				_isSpecialFormat = value;
			}
		}


		[JsonIgnore]
		public bool IsRegularFormat
		{
			get { return !(_isBold || _isSpecialFormat); }
		}

		[JsonIgnore]
		public virtual bool DisplayAsBold
		{
			get
			{
				if (ParentObjectLink.ExpirationSettings.Enable && ParentObjectLink.ExpirationSettings.IsExpired && ParentObjectLink.ExpirationSettings.MarkWhenExpired)
					return true;
				return _isBold;
			}
		}

		[JsonIgnore]
		protected LibraryObjectLink ParentObjectLink
		{
			get { return (LibraryObjectLink)Parent; }
		}
	}
}
