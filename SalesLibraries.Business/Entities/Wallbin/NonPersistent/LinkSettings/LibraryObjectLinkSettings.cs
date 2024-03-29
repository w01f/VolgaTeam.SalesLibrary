﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LibraryObjectLinkSettings : BaseLinkSettings
	{
		private string _hoverNote;
		public string HoverNote
		{
			get => _hoverNote;
			set
			{
				if (_hoverNote != value)
					OnSettingsChanged();
				_hoverNote = value;
				if (String.IsNullOrEmpty(_hoverNote))
					ShowOnlyCustomHoverNote = false;
			}
		}

		private bool _showOnlyCustomHoverNote;
		public bool ShowOnlyCustomHoverNote
		{
			get => _showOnlyCustomHoverNote;
			set
			{
				if (_showOnlyCustomHoverNote != value)
					OnSettingsChanged();
				_showOnlyCustomHoverNote = value;
			}
		}

		private FontStyle _regularFontStyle;
		public FontStyle RegularFontStyle
		{
			get => _regularFontStyle;
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
			get => _isSpecialFormat;
			set
			{
				if (_isSpecialFormat != value)
					OnSettingsChanged();
				_isSpecialFormat = value;
			}
		}

		private DateTime? _fakeFileDate;
		public DateTime? FakeFileDate
		{
			get => _fakeFileDate;
			set
			{
				if (_fakeFileDate != value)
					OnSettingsChanged();
				_fakeFileDate = value;
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

		public override void ResetToDefault(IList<LinkSettingsGroupType> groupsForReset)
		{
			base.ResetToDefault(groupsForReset);
			foreach (var linkSettingsGroupType in groupsForReset)
			{
				switch (linkSettingsGroupType)
				{
					case LinkSettingsGroupType.HoverNote:
						HoverNote = null;
						ShowOnlyCustomHoverNote = false;
						break;
					case LinkSettingsGroupType.TextFormatting:
						RegularFontStyle = FontStyle.Regular;
						IsSpecialFormat = false;
						break;
				}
			}
		}
		
		public override IList<LinkSettingsGroupType> GetCustomizedSettigsGroups()
		{
			var customizedSettingsGroups = base.GetCustomizedSettigsGroups();

			if (!String.IsNullOrEmpty(HoverNote))
				customizedSettingsGroups.Add(LinkSettingsGroupType.HoverNote);
			if (RegularFontStyle != FontStyle.Regular || IsSpecialFormat)
				customizedSettingsGroups.Add(LinkSettingsGroupType.TextFormatting);

			return customizedSettingsGroups.Distinct().ToList();
		}
	}
}
