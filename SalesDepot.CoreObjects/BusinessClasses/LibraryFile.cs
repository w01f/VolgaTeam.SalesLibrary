using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public interface ILibraryLink : ISyncObject
	{
		string Name { get; set; }
		LibraryFolder Parent { get; set; }
		Guid RootId { get; set; }
		Guid Identifier { get; set; }
		string OriginalPath { get; set; }
		string RelativePath { get; set; }
		string WebPath { get; }
		FileTypes Type { get; set; }
		string Format { get; }
		int Order { get; set; }
		bool IsDead { get; set; }
		DateTime AddDate { get; set; }
		bool EnableWidget { get; set; }
		string CriteriaOverlap { get; set; }
		string DisplayName { get; }
		string NameWithExtension { get; }
		string NameWithoutExtesion { get; }
		string Extension { get; }
		Image Widget { get; set; }
		LinkExtendedProperties ExtendedProperties{ get; }
		LibraryFileSearchTags SearchTags { get; set; }
		SearchGroup CustomKeywords { get; }
		List<SuperFilter> SuperFilters { get; }
		ExpirationDateOptions ExpirationDateOptions { get; }
		PresentationProperties PresentationProperties { get; set; }
		LineBreakProperties LineBreakProperties { get; set; }
		BannerProperties BannerProperties { get; }

		ILibraryLink Clone(LibraryFolder parent);
		string Serialize();
		void Deserialize(XmlNode fileNode);
	}

	public interface ILibraryFolderLink : ILibraryLink
	{
		List<ILibraryLink> FolderContent { get; }
	}
}
