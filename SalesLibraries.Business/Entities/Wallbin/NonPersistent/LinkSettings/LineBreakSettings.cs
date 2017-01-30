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
			if ((_font != null && _font.Size != DefaultFont.Size && _font.Style != DefaultFont.Style && _font.Name != DefaultFont.Name) || ForeColor.HasValue || TextWordWrap)
				customizedSettingsGroups.Add(LinkSettingsGroupType.TextFormatting);

			return customizedSettingsGroups;
		}
	}
}
