using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkGroupSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LibraryFolderLinkSettings : LibraryFileLinkSettings, ILinkGroupSettingsContainer
	{
		public List<LinkFileSettingsTemplate> SettingsTemplates { get; set; }

		private LibraryFolderLink ParentFolder => (LibraryFolderLink)Parent;

		public ILinkGroupSettingsContainer ParentLinkSettingsContainer => ParentFolder.ParentFolder.Settings;

		public List<LinkGroupSettingsTemplate> LinkSettingsTemplates { get; }

		public LibraryFolderLinkSettings()
		{
			LinkSettingsTemplates = new List<LinkGroupSettingsTemplate>();
		}

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			SettingsTemplates = new List<LinkFileSettingsTemplate>();
		}

		protected override void AfterCreate()
		{
			base.AfterConstruction();
			if (SettingsTemplates == null)
				SettingsTemplates = new List<LinkFileSettingsTemplate>();
		}

		public void ProcessUniverslaLinkSettings(
			LinkSettingsType settingsType,
			LibraryFileLink templateLink,
			bool useUniversalSettings)
		{
			if (!useUniversalSettings)
			{
				SettingsTemplates.RemoveAll(st => st.SettingsType == settingsType && st.LinkType == templateLink.Type);
				return;
			}

			if (!SettingsTemplates.Any(st => st.SettingsType == settingsType && st.LinkType == templateLink.Type))
				SettingsTemplates.Add(LinkFileSettingsTemplate.Create(settingsType, templateLink.Type));

			foreach (var targetLink in ParentFolder.AllLinks.Where(l => l.Type == templateLink.Type && l != templateLink).ToList())
				ApplyUniversalLinkSettings(settingsType, targetLink, templateLink);
		}

		public void ApplyUniverslaLinkSettings(IList<LibraryFileLink> targetLinks, LibraryFileLink templateLink = null)
		{
			foreach (var template in SettingsTemplates)
			{
				if (templateLink == null)
					templateLink = ParentFolder.AllLinks
						.FirstOrDefault(link => link.Type == template.LinkType &&
						targetLinks.All(tl => tl != link));

				if (templateLink == null) continue;

				foreach (var targetLink in targetLinks)
					ApplyUniversalLinkSettings(template.SettingsType, targetLink, templateLink);
			}
		}

		private void ApplyUniversalLinkSettings(
			LinkSettingsType settingsType,
			LibraryFileLink targetLink,
			LibraryFileLink templateLink)
		{
			switch (settingsType)
			{
				case LinkSettingsType.Tags:
					targetLink.Tags = null;
					targetLink.TagsEncoded = templateLink.Tags.Serialize();
					break;
				case LinkSettingsType.Security:
					targetLink.Security = null;
					targetLink.SettingsEncoded = templateLink.Security.Serialize();
					break;
				default:
					targetLink.Settings = null;
					targetLink.SettingsEncoded = templateLink.Settings.Serialize();
					break;
			}
			targetLink.MarkAsModified();
		}
	}
}
