using System.Drawing;
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

		public bool IsBold { get; set; }
		private FontStyle _regularFontStyle;
		public FontStyle RegularFontStyle
		{
			get { return _regularFontStyle; }
			set
			{
				if (_regularFontStyle != value)
					OnSettingsChanged();
				_regularFontStyle = value;
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
		public virtual bool DisplayAsBold
		{
			get
			{
				if (ParentObjectLink.ExpirationSettings.Enable && ParentObjectLink.ExpirationSettings.IsExpired && ParentObjectLink.ExpirationSettings.MarkWhenExpired)
					return true;
				return false;
			}
		}

		[JsonIgnore]
		protected LibraryObjectLink ParentObjectLink => (LibraryObjectLink)Parent;

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			RegularFontStyle = FontStyle.Regular;
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			//TODO Remove After several version
			if (IsBold)
			{
				_regularFontStyle = FontStyle.Bold;
				IsBold = false;
			}
		}
	}
}
