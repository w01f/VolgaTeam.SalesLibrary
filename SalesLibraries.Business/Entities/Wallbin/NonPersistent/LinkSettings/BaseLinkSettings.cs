using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public abstract class BaseLinkSettings : SettingsContainer
	{
		[JsonIgnore]
		protected Font _defaultFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);

		public const string PredefinedNoteNone = "None";
		public const string PredefinedNoteNew = "NEW!";
		public const string PredefinedNoteSold = "SOLD!";
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

		protected Font _font;
		public Font Font
		{
			get { return _font ?? _defaultFont; }
			set
			{
				if (_font != value)
					OnSettingsChanged();
				if (value != _defaultFont)
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

		private bool _textWordWrap;
		public bool TextWordWrap
		{
			get { return _textWordWrap; }
			set
			{
				if (_textWordWrap != value)
					OnSettingsChanged();
				_textWordWrap = value;
			}
		}

		[JsonIgnore]
		protected BaseLibraryLink ParentLink => (BaseLibraryLink)Parent;

		public virtual void ResetToDefault(IList<LinkSettingsGroupType> groupsForReset)
		{
			foreach (var linkSettingsGroupType in groupsForReset)
			{
				switch (linkSettingsGroupType)
				{
					case LinkSettingsGroupType.TextNote:
						Note = null;
						break;
					case LinkSettingsGroupType.TextFormatting:
						Font = null;
						ForeColor = null;
						TextWordWrap = false;
						break;
				}
			}
		}

		public virtual void ResetToEmpty() { }

		public virtual IList<LinkSettingsGroupType> GetCustomizedSettigsGroups()
		{
			var customizedSettingsGroups = new List<LinkSettingsGroupType>();

			if (!String.IsNullOrEmpty(Note))
				customizedSettingsGroups.Add(LinkSettingsGroupType.TextNote);
			if ((_font != null && _font.Size != _defaultFont.Size && _font.Style != _defaultFont.Style && _font.Name != _defaultFont.Name) || ForeColor.HasValue || TextWordWrap)
				customizedSettingsGroups.Add(LinkSettingsGroupType.TextFormatting);

			return customizedSettingsGroups;
		}

		public static TSettings CreateEmpty<TSettings>(IChangable parent) where TSettings : BaseLinkSettings
		{
			var settings = SettingsContainer.CreateInstance<TSettings>(parent);
			settings.ResetToEmpty();
			return settings;
		}
	}
}