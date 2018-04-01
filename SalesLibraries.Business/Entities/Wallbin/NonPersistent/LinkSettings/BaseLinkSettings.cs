using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public abstract class BaseLinkSettings : SettingsContainer
	{
		public const string PredefinedNoteNone = "None";
		public const string PredefinedNoteNew = "NEW!";
		public const string PredefinedNoteSold = "SOLD!";
		public const string PredefinedNoteUpdated = "UPDATED!";
		public const string PredefinedNoteSellThis = "SELL THIS!";
		public const string PredefinedNoteAttention = "ATTENTION!";

		protected string _note;
		public virtual string Note
		{
			get => _note;
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
			get => _font ?? ParentLink?.ParentLibrary?.Settings?.FontSettings?.Font;
			set
			{
				if (_font != value)
					OnSettingsChanged();
				if (value != ParentLink?.ParentLibrary?.Settings?.FontSettings?.Font)
					_font = value;
			}
		}

		private Color? _foreColor;
		public Color? ForeColor
		{
			get => _foreColor ?? ParentLink?.ParentLibrary?.Settings?.FontSettings?.Color;
			set
			{
				if (_foreColor != value)
					OnSettingsChanged();
				if (value != ParentLink?.ParentLibrary?.Settings?.FontSettings?.Color)
					_foreColor = value;
			}
		}

		private bool _textWordWrap;
		public bool TextWordWrap
		{
			get => _textWordWrap;
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

			var defaultFont = ParentLink?.ParentLibrary?.Settings?.FontSettings?.Font;
			var defaultColor = ParentLink?.ParentLibrary?.Settings?.FontSettings?.Color;
			if ((_font != null && defaultFont != null && _font.Size != defaultFont.Size && _font.Style != defaultFont.Style && _font.Name != defaultFont.Name) || (ForeColor.HasValue && ForeColor != defaultColor) || TextWordWrap)
				customizedSettingsGroups.Add(LinkSettingsGroupType.TextFormatting);

			return customizedSettingsGroups;
		}
	}
}