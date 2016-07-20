using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public abstract class BaseLinkSettings : SettingsContainer
	{
		public const string PredefinedNoteNone = "None";
		public const string PredefinedNoteNew = "NEW!";
		public const string PredefinedNoteUpdated = "UPDATED!";
		public const string PredefinedNoteSellThis = "SELL THIS!";
		public const string PredefinedNoteAttention = "ATTENTION!";

		protected string _note;
		public virtual string Note
		{
			get
			{
				return _note;
			}
			set
			{
				if (_note != value)
					OnSettingsChanged();
				_note = value;
			}
		}

		private Font _font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
		public Font Font
		{
			get { return _font; }
			set
			{
				if (_font != value)
					OnSettingsChanged();
				_font = value;
			}
		}

		private Color? _foreColor;
		public Color? ForeColor
		{
			get { return _foreColor; }
			set
			{
				if (_foreColor != value)
					OnSettingsChanged();
				_foreColor = value;
			}
		}

		[JsonIgnore]
		protected BaseLibraryLink ParentLink => (BaseLibraryLink)Parent;
	}
}