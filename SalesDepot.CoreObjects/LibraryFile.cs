using System;
using System.Drawing;
using System.Xml;

namespace SalesDepot.CoreObjects
{
    public interface ILibraryFile
    {
        string Name { get; set; }
        LibraryFolder Parent { get; set; }
        Guid RootId { get; set; }
        Guid Identifier { get; set; }
        string RelativePath { get; set; }
        FileTypes Type { get; set; }
        string Format { get; set; }
        int Order { get; set; }
        bool IsBold { get; set; }
        bool IsDead { get; set; }
        DateTime AddDate { get; set; }
        bool EnableWidget { get; set; }
        string CriteriaOverlap { get; set; }
        string OriginalPath { get; }
        string DisplayName { get; }
        string NameWithExtension { get; }
        string NameWithoutExtesion { get; }
        string Extension { get; }
        string Note { get; }
        bool DisplayAsBold { get; }
        bool IsExpired { get; }
        Image Widget { get; }

        LibraryFileSearchTags SearchTags { get; set; }
        ExpirationDateOptions ExpirationDateOptions { get; set; }
        PresentationProperties PresentationProperties { get; set; }
        LineBreakProperties LineBreakProperties { get; set; }
        BannerProperties BannerProperties { get; set; }

        string Serialize();
        void Deserialize(XmlNode fileNode);
    }
}
