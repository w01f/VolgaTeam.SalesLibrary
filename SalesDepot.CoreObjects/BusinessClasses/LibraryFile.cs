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
		bool IsBold { get; set; }
		bool IsDead { get; set; }
		DateTime AddDate { get; set; }
		bool EnableWidget { get; set; }
		string CriteriaOverlap { get; set; }
		string DisplayName { get; }
		string NameWithExtension { get; }
		string NameWithoutExtesion { get; }
		string Extension { get; }
		string Note { get; set; }
		bool DisplayAsBold { get; }
		bool IsExpired { get; }
		bool IsRestricted { get; set; }
		bool NoShare { get; set; }
		bool DoNotGeneratePreview { get; set; }
		bool ForcePreview { get; set; }
		string AssignedUsers { get; set; }
		Image Widget { get; set; }

		LibraryFileSearchTags SearchTags { get; set; }
		SearchGroup CustomKeywords { get; }
		ExpirationDateOptions ExpirationDateOptions { get; }
		PresentationProperties PresentationProperties { get; set; }
		LineBreakProperties LineBreakProperties { get; set; }
		BannerProperties BannerProperties { get; }
		FileCard FileCard { get; set; }
		AttachmentProperties AttachmentProperties { get; set; }

		ILibraryLink Clone(LibraryFolder parent);
		string Serialize();
		void Deserialize(XmlNode fileNode);
	}

	public interface ILibraryFolderLink : ILibraryLink
	{
		List<ILibraryLink> FolderContent { get; }
	}
}
