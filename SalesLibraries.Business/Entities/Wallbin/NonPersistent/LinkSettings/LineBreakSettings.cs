using System;
using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LineBreakSettings : BaseLinkSettings
	{
		public override void ResetToDefault(IList<LinkSettingsGroupType> groupsForReset)
		{
			foreach (var linkSettingsGroupType in groupsForReset)
			{
				switch (linkSettingsGroupType)
				{
					case LinkSettingsGroupType.HoverNote:
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

		public override IList<LinkSettingsGroupType> GetCustomizedSettigsGroups()
		{
			var customizedSettingsGroups = new List<LinkSettingsGroupType>();

			if (!String.IsNullOrEmpty(Note))
				customizedSettingsGroups.Add(LinkSettingsGroupType.HoverNote);

			var defaultFont = ParentLink?.ParentLibrary?.Settings?.FontSettings?.Font;
			var defaultColor = ParentLink?.ParentLibrary?.Settings?.FontSettings?.Color;
			if ((_font != null && defaultFont != null && _font.Size != defaultFont.Size && _font.Style != defaultFont.Style && _font.Name != defaultFont.Name) || (ForeColor.HasValue && ForeColor != defaultColor) || TextWordWrap)
				customizedSettingsGroups.Add(LinkSettingsGroupType.TextFormatting);

			return customizedSettingsGroups;
		}
	}
}
