using System.IO;
using System.Windows.Forms;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class Constants
    {
        public const string UserSettingsFileName = @"SalesDepotUserSettings.xml";
        public const string StorageFileName = @"SalesDepotCache.xml";
        public const string JsonFileName = @"SalesDepotCache.json";
        public const string StorageLightFileName = @"SalesDepotCacheLight.xml";
        public const string StyleFileName = @"SalesDepotStyle.xml";
        public const string WholeDriveFilesStorage = @"Primary Root";
        public const string RegularPreviewContainersRootFolderName = @"!QV";
        public const string FtpPreviewContainersRootFolderName = @"!WV";
        public const string OldPreviewFolderPrefix = @"!PNG_";
        public const string LibraryLogoFolder = @"!SD-Graphics";
        public const string OvernightsCalendarRootFolderName = @"!OC";
        public const string ProgramManagerRootFolderName = @"!PM";
        public const string ExtraFoldersRootFolderName = @"!Extra Roots";
        public const string SweepPeriodsFileName = @"SweepPeriods.xml";
    }

    public enum FileTypes
    {
        FriendlyPresentation = 0,
        Presentation,
        BuggyPresentation,
        MediaPlayerVideo,
        QuickTimeVideo,
        Folder,
        LineBreak,
        Other,
        Url,
        Network,
        PDF,
        Excel,
        Word,
        OvernightsLink
    }

    public enum Alignment
    {
        Left = 0,
        Center,
        Right
    }
}
