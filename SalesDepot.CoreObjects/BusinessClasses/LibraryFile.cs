using System;
using System.Drawing;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public interface ILibraryFile : ISyncObject
	{
		string Name { get; set; }
		LibraryFolder Parent { get; set; }
		Guid RootId { get; set; }
		Guid Identifier { get; set; }
		string OriginalPath { get; }
		string RelativePath { get; set; }
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
		string Note { get; }
		bool DisplayAsBold { get; }
		bool IsExpired { get; }
		Image Widget { get; }

		LibraryFileSearchTags SearchTags { get; set; }
		SearchGroup CustomKeywords { get; }
		ExpirationDateOptions ExpirationDateOptions { get; }
		PresentationProperties PresentationProperties { get; set; }
		LineBreakProperties LineBreakProperties { get; }
		AttachmentProperties AttachmentProperties { get; set; }
		BannerProperties BannerProperties { get; }
		FileCard FileCard { get; set; }

		ILibraryFile Clone(LibraryFolder parent);
		string Serialize();
		void Deserialize(XmlNode fileNode);
	}
}
