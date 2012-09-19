using System;
using System.Collections.Generic;
using System.IO;

namespace SalesDepot.CoreObjects
{
    public interface ILibrary
    {
        string Name { get; }
        RootFolder RootFolder { get; }

        Guid Identifier { get; set; }
        DirectoryInfo Folder { get; set; }
        bool UseDirectAccess { get; set; }
        DateTime DirectAccessFileBottomDate { get; set; }
        string BrandingText { get; set; }
        DateTime SyncDate { get; set; }

        bool ApplyAppearanceForAllWindows { get; set; }
        bool ApplyWidgetForAllWindows { get; set; }
        bool ApplyBannerForAllWindows { get; set; }
        bool MinimizeOnSync { get; set; }
        bool CloseAfterSync { get; set; }
        bool ShowProgressDuringSync { get; set; }
        bool EnableInactiveLinks { get; set; }
        bool InactiveLinksBoldWarning { get; set; }
        bool ReplaceInactiveLinksWithLineBreak { get; set; }
        bool InactiveLinksMessageAtStartup { get; set; }
        bool SendEmail { get; set; }

        bool IsConfigured { get; set; }

        List<RootFolder> ExtraFolders { get; }
        List<LibraryPage> Pages { get; }
        List<string> EmailList { get; }
        List<AutoWidget> AutoWidgets { get; }

        RootFolder GetRootFolder(Guid folderId);
        ILibraryFile GetLinkInstance(LibraryFolder parentFolder);
    }
}
