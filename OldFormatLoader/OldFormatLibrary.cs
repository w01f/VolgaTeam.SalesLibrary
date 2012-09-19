using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SalesDepot.CoreObjects
{
    public enum OldFormatLibraryStyles
    {
        Banner = 0,
        NoBanner,
        DigitalWallBin
    }

    public enum OldFormatFileTypes
    {
        FriendlyPresentation = 0,
        OtherPresentation,
        BuggyPresentation,
        MediaPlayerVideo,
        QuickTimeVideo,
        Folder,
        LineBreak,
        Other,
        Url,
        Network
    }

    public class OldFormatLibrary
    {
        private const string UserSettingsFileName = @"SalesDepotUserSettings.xml";
        private const string StorageFileName = @"SalesDepotCache.xml";
        private const string StyleFileName = @"SalesDepotStyle.xml";

        private SalesDepotFiles _storedSalesDepot = new SalesDepotFiles();
        private string _name = string.Empty;
        private DirectoryInfo _folder;

        private string _brandingText;
        private DateTime _syncDate = DateTime.UtcNow;
        private bool _applyForAllWindows = false;
        private bool _syncLinkedFiles = true;

        private List<OldFormatLibraryFolder> _folders = new List<OldFormatLibraryFolder>();
        private List<OldFormatLibraryPage> _pages = new List<OldFormatLibraryPage>();

        public double MinFolderOrder { get; set; }

        public OldFormatLibrary(string name, DirectoryInfo folder)
        {
            _folder = folder;
            _name = name;
            Load();
        }

        private bool LoadSettings()
        {
            bool result = false;
            FileInfo settingsFile = new FileInfo(Path.Combine(_folder.FullName, UserSettingsFileName));
            if (settingsFile.Exists)
            {
                try
                {
                    DataSet settingsTables = new DataSet();
                    settingsTables.ReadXml(settingsFile.FullName);
                    foreach (DataTable table in settingsTables.Tables)
                        _storedSalesDepot.Tables[table.TableName].Merge(table);
                    result = true;
                }
                catch
                {
                }
            }
            return result;
        }

        private bool LoadStorage()
        {
            bool result = false;
            FileInfo storageFile = new FileInfo(Path.Combine(_folder.FullName, StorageFileName));
            if (storageFile.Exists)
            {
                try
                {
                    DataSet storageTables = new DataSet();
                    storageTables.ReadXml(storageFile.FullName);
                    foreach (DataTable table in storageTables.Tables)
                        _storedSalesDepot.Tables[table.TableName].Merge(table);
                    result = true;
                }
                catch
                {
                }
            }
            return result;
        }

        private void Load()
        {
            _brandingText = string.Empty;

            FontConverter converter = new FontConverter();

            if (LoadSettings())
            {
                if (_storedSalesDepot.Stations.Rows.Count > 0)
                {
                    _name = _storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.NameColumn] == DBNull.Value ? string.Empty : _storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.NameColumn].ToString();
                    _brandingText = _storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.BrandingTextColumn] == DBNull.Value ? string.Empty : _storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.BrandingTextColumn].ToString();
                    _applyForAllWindows = _storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.ApplyForAllWindowsColumn] == DBNull.Value ? false : Boolean.Parse(_storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.ApplyForAllWindowsColumn].ToString());
                    _syncLinkedFiles = _storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.SyncLinksOnlyColumn] == DBNull.Value ? true : Boolean.Parse(_storedSalesDepot.Stations.Rows[0][_storedSalesDepot.Stations.SyncLinksOnlyColumn].ToString());
                    _pages.Clear();
                    OldFormatLibraryPage tabPage;
                    foreach (SalesDepotFiles.TabPagesRow row in _storedSalesDepot.TabPages.Rows)
                    {
                        tabPage = new OldFormatLibraryPage();
                        tabPage.TabPageName = row[_storedSalesDepot.TabPages.TabPageNameColumn] == DBNull.Value ? string.Empty : row[_storedSalesDepot.TabPages.TabPageNameColumn].ToString();
                        tabPage.IsChecked = row[_storedSalesDepot.TabPages.IsCheckedColumn] == DBNull.Value ? false : Boolean.Parse(row[_storedSalesDepot.TabPages.IsCheckedColumn].ToString());
                        _pages.Add(tabPage);
                    }
                }
            }

            if (LoadStorage())
            {
                OldFormatLibraryFolder folder;
                OldFormatLibraryFile file;
                foreach (SalesDepotFiles.FoldersRow row in _storedSalesDepot.Folders.Rows)
                {
                    folder = new OldFormatLibraryFolder(this);
                    folder.Name = row[_storedSalesDepot.Folders.NameColumn] == DBNull.Value ? string.Empty : row[_storedSalesDepot.Folders.NameColumn].ToString();
                    folder.ParentName = row[_storedSalesDepot.Folders.ParentNameColumn] == DBNull.Value ? string.Empty : row[_storedSalesDepot.Folders.ParentNameColumn].ToString();
                    folder.TabPageName = row[_storedSalesDepot.Folders.TabPageNameColumn] == DBNull.Value ? string.Empty : row[_storedSalesDepot.Folders.TabPageNameColumn].ToString();
                    folder.Order = row[_storedSalesDepot.Folders.OrderColumn] == DBNull.Value ? -1 : Int32.Parse(row[_storedSalesDepot.Folders.OrderColumn].ToString());
                    folder.ColumnOrder = row[_storedSalesDepot.Folders.ColumnOrderColumn] == DBNull.Value ? -1 : Int32.Parse(row[_storedSalesDepot.Folders.ColumnOrderColumn].ToString());
                    try
                    {
                        folder.WindowFont = row[_storedSalesDepot.Folders.WindowFontColumn] == DBNull.Value ? new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel) : (Font)converter.ConvertFromString(row[_storedSalesDepot.Folders.WindowFontColumn].ToString());
                    }
                    catch
                    {
                    }
                    try
                    {
                        folder.HeaderFont = row[_storedSalesDepot.Folders.HeaderFontColumn] == DBNull.Value ? new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel) : (Font)converter.ConvertFromString(row[_storedSalesDepot.Folders.HeaderFontColumn].ToString());
                    }
                    catch
                    {
                    }
                    folder.BackgroundWindowColor = row[_storedSalesDepot.Folders.BackgroundWindowColorColumn] == DBNull.Value ? Color.White : Color.FromArgb(Int32.Parse(row[_storedSalesDepot.Folders.BackgroundWindowColorColumn].ToString()));
                    folder.ForeWindowColor = row[_storedSalesDepot.Folders.ForeWindowColorColumn] == DBNull.Value ? Color.Black : Color.FromArgb(Int32.Parse(row[_storedSalesDepot.Folders.ForeWindowColorColumn].ToString()));
                    folder.BackgroundHeaderColor = row[_storedSalesDepot.Folders.BackgroundHeaderColorColumn] == DBNull.Value ? Color.White : Color.FromArgb(Int32.Parse(row[_storedSalesDepot.Folders.BackgroundHeaderColorColumn].ToString()));
                    folder.ForeHeaderColor = row[_storedSalesDepot.Folders.ForeHeaderColorColumn] == DBNull.Value ? Color.Black : Color.FromArgb(Int32.Parse(row[_storedSalesDepot.Folders.ForeHeaderColorColumn].ToString()));

                    foreach (SalesDepotFiles.FilesRow fileRow in (SalesDepotFiles.FilesRow[])_storedSalesDepot.Files.Select("FolderName = '" + Path.Combine(folder.ParentName, folder.Name) + "'"))
                    {
                        file = new OldFormatLibraryFile();
                        file.DisplayName = fileRow[_storedSalesDepot.Files.DisplayNameColumn] == DBNull.Value ? string.Empty : fileRow[_storedSalesDepot.Files.DisplayNameColumn].ToString();
                        file.FullName = fileRow[_storedSalesDepot.Files.FullNameColumn] == DBNull.Value ? string.Empty : fileRow[_storedSalesDepot.Files.FullNameColumn].ToString();
                        file.FolderName = fileRow[_storedSalesDepot.Files.FolderNameColumn] == DBNull.Value ? string.Empty : fileRow[_storedSalesDepot.Files.FolderNameColumn].ToString();
                        file.Type = fileRow[_storedSalesDepot.Files.TypeColumn] == DBNull.Value ? OldFormatFileTypes.Other : (OldFormatFileTypes)Int32.Parse(fileRow[_storedSalesDepot.Files.TypeColumn].ToString());
                        file.Format = fileRow[_storedSalesDepot.Files.FormatColumn] == DBNull.Value ? string.Empty : fileRow[_storedSalesDepot.Files.FormatColumn].ToString();
                        file.Order = fileRow[_storedSalesDepot.Files.OrderColumn] == DBNull.Value ? 1 : Int32.Parse(fileRow[_storedSalesDepot.Files.OrderColumn].ToString());
                        file.Note = fileRow[_storedSalesDepot.Files.NoteColumn] == DBNull.Value ? string.Empty : fileRow[_storedSalesDepot.Files.NoteColumn].ToString();
                        file.IsBold = fileRow[_storedSalesDepot.Files.IsBoldColumn] == DBNull.Value ? false : Boolean.Parse(fileRow[_storedSalesDepot.Files.IsBoldColumn].ToString());
                        folder.Files.Add(file);
                    }
                    _folders.Add(folder);
                }
                this.MinFolderOrder = _folders.Where(x => x.Order >= 0).Min(x => x.Order);
                if (_storedSalesDepot.SyncOptions.Rows.Count > 0)
                    _syncDate = _storedSalesDepot.SyncOptions.Rows[0][_storedSalesDepot.SyncOptions.SyncDateColumn] == DBNull.Value ? DateTime.UtcNow : DateTime.Parse(_storedSalesDepot.SyncOptions.Rows[0][_storedSalesDepot.SyncOptions.SyncDateColumn].ToString());
            }
            _folders.Sort((x, y) => (x.ColumnOrder.CompareTo(y.ColumnOrder) == 0 ? x.Order.CompareTo(y.Order) : x.ColumnOrder.CompareTo(y.ColumnOrder)));
        }

        private void ArchiveOldFiles()
        {
            string archivePath = Path.Combine(_folder.FullName, "!OldFormatCache");
            if (!Directory.Exists(archivePath))
                Directory.CreateDirectory(archivePath);

            FileInfo styleFile = new FileInfo(Path.Combine(_folder.FullName, StyleFileName));
            if (styleFile.Exists)
            {
                try
                {

                    styleFile.CopyTo(Path.Combine(archivePath, StyleFileName), true);
                    styleFile.Attributes = FileAttributes.Normal;
                    styleFile.Delete();
                }
                catch
                { }
            }

            FileInfo settingsFile = new FileInfo(Path.Combine(_folder.FullName, UserSettingsFileName));
            if (settingsFile.Exists)
            {
                try
                {
                    settingsFile.CopyTo(Path.Combine(archivePath, UserSettingsFileName), true);
                    settingsFile.Attributes = FileAttributes.Normal;
                    settingsFile.Delete();
                }
                catch
                { }
            }

            FileInfo storageFile = new FileInfo(Path.Combine(_folder.FullName, StorageFileName));
            if (storageFile.Exists)
            {
                try
                {
                    storageFile.CopyTo(Path.Combine(archivePath, StorageFileName), true);
                    settingsFile.Attributes = FileAttributes.Normal;
                    storageFile.Delete();
                }
                catch
                { }
            }
        }

        public string SerializeInNewFormat()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<Library>");
            xml.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
            xml.AppendLine(@"<BrandingText>" + _brandingText.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BrandingText>");
            xml.AppendLine(@"<SyncDate>" + _syncDate + @"</SyncDate>");
            xml.AppendLine(@"<SyncLinkedFiles>" + _syncLinkedFiles + @"</SyncLinkedFiles>");
            xml.AppendLine(@"<ApplyForAllWindows>" + _applyForAllWindows + @"</ApplyForAllWindows>");
            xml.AppendLine(@"<MinimizeOnSync>" + true + @"</MinimizeOnSync>");
            xml.AppendLine(@"<CloseAfterSync>" + true + @"</CloseAfterSync>");
            xml.AppendLine(@"<ShowProgressDuringSync>" + true + @"</ShowProgressDuringSync>");
            xml.AppendLine(@"<EnableInactiveLinks>" + true + @"</EnableInactiveLinks>");
            xml.AppendLine(@"<InactiveLinksBoldWarning>" + true + @"</InactiveLinksBoldWarning>");
            xml.AppendLine(@"<ReplaceInactiveLinksWithLineBreak>" + false + @"</ReplaceInactiveLinksWithLineBreak>");
            xml.AppendLine(@"<InactiveLinksMessageAtStartup>" + true + @"</InactiveLinksMessageAtStartup>");
            xml.AppendLine("<Pages>");
            int i = 0;
            foreach (OldFormatLibraryPage page in _pages)
            {
                xml.AppendLine(@"<Page>");
                xml.AppendLine(@"<Name>" + page.TabPageName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
                xml.AppendLine(@"<Enable>" + page.IsChecked + @"</Enable>");
                xml.AppendLine(@"<Order>" + i + @"</Order>");
                xml.AppendLine("<Folders>");
                foreach (OldFormatLibraryFolder folder in _folders.Where(x => x.TabPageName.Equals(page.TabPageName)))
                    xml.AppendLine(@"<Folder>" + folder.Serialize() + @"</Folder>");
                xml.AppendLine("</Folders>");
                xml.AppendLine(@"</Page>");
                i++;
            }
            xml.AppendLine("</Pages>");
            xml.AppendLine(@"</Library>");
            return xml.ToString();
        }

        public void SaveInNewFormat()
        {
            ArchiveOldFiles();

            using (StreamWriter sw = new StreamWriter(Path.Combine(_folder.FullName, StorageFileName), false))
            {
                sw.Write(SerializeInNewFormat());
                sw.Flush();
            }
        }
    }

    public class OldFormatLibraryFolder
    {
        public OldFormatLibrary Parent { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string TabPageName { get; set; }
        public double Order { get; set; }
        public int ColumnOrder { get; set; }
        public Color BackgroundWindowColor { get; set; }
        public Color ForeWindowColor { get; set; }
        public Color BackgroundHeaderColor { get; set; }
        public Color ForeHeaderColor { get; set; }
        public Font WindowFont { get; set; }
        public Font HeaderFont { get; set; }

        public List<OldFormatLibraryFile> Files { get; set; }

        public OldFormatLibraryFolder(OldFormatLibrary parentSalesDepot)
        {
            this.Parent = parentSalesDepot;
            this.Name = string.Empty;
            this.ParentName = string.Empty;
            this.TabPageName = string.Empty;
            this.BackgroundWindowColor = Color.White;
            this.ForeWindowColor = Color.Black;
            this.BackgroundHeaderColor = Color.White;
            this.ForeHeaderColor = Color.Black;
            this.WindowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            this.HeaderFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.Files = new List<OldFormatLibraryFile>();
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<RowOrder>" + (this.Order - this.Parent.MinFolderOrder).ToString() + @"</RowOrder>");
            result.AppendLine(@"<ColumnOrder>" + (this.ColumnOrder - 1).ToString() + @"</ColumnOrder>");
            result.AppendLine(@"<BackgroundWindowColor>" + this.BackgroundWindowColor.ToArgb() + @"</BackgroundWindowColor>");
            result.AppendLine(@"<ForeWindowColor>" + this.ForeWindowColor.ToArgb() + @"</ForeWindowColor>");
            result.AppendLine(@"<BackgroundHeaderColor>" + this.BackgroundHeaderColor.ToArgb() + @"</BackgroundHeaderColor>");
            result.AppendLine(@"<ForeHeaderColor>" + this.ForeHeaderColor.ToArgb() + @"</ForeHeaderColor>");
            result.AppendLine(@"<WindowFont>" + converter.ConvertToString(this.WindowFont) + @"</WindowFont>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(this.HeaderFont) + @"</HeaderFont>");
            result.AppendLine("<Files>");
            foreach (OldFormatLibraryFile file in this.Files)
                result.AppendLine(@"<File>" + file.Serialize() + @"</File>");
            result.AppendLine("</Files>");
            return result.ToString();
        }
    }

    public class OldFormatLibraryFile
    {
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string FolderName { get; set; }
        public OldFormatFileTypes Type { get; set; }
        public string Format { get; set; }
        public int Order { get; set; }
        public string Note { get; set; }
        public bool IsBold { get; set; }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<DisplayName>" + this.DisplayName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DisplayName>");
            result.AppendLine(@"<Note>" + this.Note.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Note>");
            result.AppendLine(@"<IsBold>" + this.IsBold + @"</IsBold>");
            result.AppendLine(@"<RelativePath>" + this.FullName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</RelativePath>");
            result.AppendLine(@"<Type>" + (int)this.Type + @"</Type>");
            result.AppendLine(@"<Format>" + this.Format.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Format>");
            result.AppendLine(@"<Order>" + this.Order + @"</Order>");
            return result.ToString();
        }
    }

    public class OldFormatLibraryPage
    {
        public string TabPageName { get; set; }
        public bool IsChecked { get; set; }
    }
}
