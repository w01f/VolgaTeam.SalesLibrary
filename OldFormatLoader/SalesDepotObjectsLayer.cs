using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Linq;
using System.Windows.Forms;


namespace FileManager
{
    /// <summary>
    /// Enumeration of the Sales Depot Styles
    /// </summary>
    public enum SalesDepotStyles
    {
        Banner = 0,
        NoBanner,
        DigitalWallBin
    }

    /// <summary>
    /// Implementation of the Base Sales Depot Object
    /// </summary>
    public abstract class SalesDepot
    {
        protected SalesDepotFiles storedSalesDepot = new SalesDepotFiles();
        protected Guid _identifier = Guid.NewGuid();

        protected string _name;
        protected DirectoryInfo _folder;
        protected string brandingText;
        protected Font brandingFont;
        protected Color brandingForeColor;
        protected int windowSeparatorSize;
        protected Font defaultFolderFont;
        protected int defaultFolderHeight;
        protected bool multiLogo;
        protected bool showNewFiles;
        protected int newFilesPeriod;
        protected bool resetNewFilesWhileSync;
        protected bool disableBuggyTags;
        protected bool presentationMastersChanged = false;
        protected bool applyForAllWindows = false;
        protected bool syncLinkedFiles = true;

        protected List<Folder> folders = new List<Folder>();
        protected List<SalesDepotTabPage> tabPages = new List<SalesDepotTabPage>();
        protected List<PresentationMaster> presentationMasters = new List<PresentationMaster>();
        protected List<SalesDepotFile> deadLinks = new List<SalesDepotFile>();

        protected bool isConfigured = false;

        protected DateTime syncDate = DateTime.UtcNow;

        #region Public Properties
        public Guid Identifier
        {
            get
            {
                return _identifier;
            }
        }


        public string Name
        {
            get
            {
                return _name;
            }
        }

        public DirectoryInfo Folder
        {
            get
            {
                return _folder;
            }
        }

        public string BrandingText
        {
            get
            {
                return brandingText;
            }
            set
            {
                brandingText = value;
            }
        }

        public Font BrandingFont
        {
            get
            {
                return brandingFont;
            }
            set
            {
                brandingFont = value;
            }
        }

        public Color BrandingForeColor
        {
            get
            {
                return brandingForeColor;
            }
            set
            {
                brandingForeColor = value;
            }
        }

        public int WindowSeparatorSize
        {
            get
            {
                return windowSeparatorSize;
            }
            set
            {
                windowSeparatorSize = value;
            }
        }

        public int DefaultFolderHeight
        {
            get
            {
                return defaultFolderHeight;
            }
            set
            {
                defaultFolderHeight = value;
            }
        }

        public Font DefaultFolderFont
        {
            get
            {
                return defaultFolderFont;
            }
            set
            {
                defaultFolderFont = value;
            }
        }

        public bool MultiLogo
        {
            get
            {
                return multiLogo;
            }
            set
            {
                multiLogo = value;
            }
        }

        public bool ShowNewFiles
        {
            get
            {
                return showNewFiles;
            }
            set
            {
                showNewFiles = value;
            }
        }

        public int NewFilesPeriod
        {
            get
            {
                return newFilesPeriod;
            }
            set
            {
                newFilesPeriod = value;
            }
        }

        public bool ResetNewFilesWhileSync
        {
            get
            {
                return resetNewFilesWhileSync;
            }
            set
            {
                resetNewFilesWhileSync = value;
            }
        }

        public bool DisableBuggyTags
        {
            get
            {
                return disableBuggyTags;
            }
            set
            {
                disableBuggyTags = value;
            }
        }

        public bool PresentationMastersChanged
        {
            get
            {
                return presentationMastersChanged;
            }
            set
            {
                presentationMastersChanged = value;
            }
        }

        public bool ApplyForAllWindows
        {
            get
            {
                return applyForAllWindows;
            }
            set
            {
                applyForAllWindows = value;
            }
        }

        public bool SyncLinkedFiles
        {
            get
            {
                return syncLinkedFiles;
            }
            set
            {
                syncLinkedFiles = value;
            }
        }

        public List<Folder> Folders
        {
            get
            {
                return folders;
            }
        }

        public List<SalesDepotTabPage> TabPages
        {
            get
            {
                return tabPages;
            }
        }

        public List<PresentationMaster> PresentationMasters
        {
            get
            {
                return presentationMasters;
            }
        }

        public List<SalesDepotFile> DeadLinks
        {
            get
            {
                return deadLinks;
            }
        }

        public bool IsConfigured
        {
            get
            {
                return isConfigured;
            }
            set
            {
                isConfigured = value;
            }
        }

        public DateTime SyncDate
        {
            get
            {
                return syncDate;
            }
            set
            {
                syncDate = value;
            }
        }
        #endregion

        public SalesDepot(string name, DirectoryInfo folder)
        {
            _folder = folder;
            _name = name;
            Init();
        }

        public static SalesDepotStyles GetSalesDepotStyle(DirectoryInfo folder)
        {
            string styleString = "";
            SalesDepotStyles style = SalesDepotStyles.Banner;
            FileInfo styleFile = new FileInfo(Path.Combine(folder.FullName,AppManager.StyleFileName));
            if (styleFile.Exists)
            {
                try
                {
                    DataSet styleTable = new DataSet();
                    styleTable.ReadXml(styleFile.FullName);
                    if (styleTable.Tables.Contains("SalesDepotStyle"))
                        if (styleTable.Tables["SalesDepotStyle"].Rows.Count > 0)
                        {
                            styleString = styleTable.Tables["SalesDepotStyle"].Rows[0]["Style"] == DBNull.Value ? "Banner" : styleTable.Tables["SalesDepotStyle"].Rows[0]["Style"].ToString();
                            switch (styleString)
                            {
                                case "Banner":
                                    style = SalesDepotStyles.Banner;
                                    break;
                                case "No Banner":
                                    style = SalesDepotStyles.NoBanner;
                                    break;
                                case "Digital Wall Bin":
                                    style = SalesDepotStyles.DigitalWallBin;
                                    break;
                                default:
                                    style = SalesDepotStyles.Banner;
                                    break;
                            }
                        }
                }
                catch
                {
                }
            }
            return style;
        }

        private bool LoadSettings()
        {
            bool result = false;
            FileInfo settingsFile = new FileInfo(Path.Combine(_folder.FullName,AppManager.UserSettingsFileName));
            if (settingsFile.Exists)
            {
                try
                {
                    DataSet settingsTables = new DataSet();
                    settingsTables.ReadXml(settingsFile.FullName);
                    foreach (DataTable table in settingsTables.Tables)
                        storedSalesDepot.Tables[table.TableName].Merge(table);
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
            FileInfo storageFile = new FileInfo(Path.Combine(_folder.FullName,AppManager.StorageFileName));
            if (storageFile.Exists)
            {
                try
                {
                    DataSet storageTables = new DataSet();
                    storageTables.ReadXml(storageFile.FullName);
                    foreach (DataTable table in storageTables.Tables)
                        storedSalesDepot.Tables[table.TableName].Merge(table);
                    result = true;
                }
                catch
                {
                }
            }
            return result;
        }

        protected abstract Folder CreateFolder();

        public virtual void Init()
        {
            brandingText = string.Empty;
            brandingFont = null;
            brandingForeColor = Color.Black;
            windowSeparatorSize = 50;
            defaultFolderFont = new Font("Arial",12);
            defaultFolderHeight = 350;
            multiLogo = true;
            showNewFiles = true;
            newFilesPeriod = 3;
            resetNewFilesWhileSync = true;
            disableBuggyTags = false;
            presentationMastersChanged = false;
            isConfigured = false;

            FontConverter converter = new FontConverter();

            if (LoadSettings())
            {
                if (storedSalesDepot.Stations.Rows.Count > 0)
                {
                    _name = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.NameColumn] == DBNull.Value ? string.Empty : storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.NameColumn].ToString();
                    brandingText = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.BrandingTextColumn] == DBNull.Value ? string.Empty : storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.BrandingTextColumn].ToString();
                    try
                    {
                        brandingFont = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.BrandingFontColumn] == DBNull.Value ? new Font("Arial Black", 48, FontStyle.Regular, GraphicsUnit.Pixel) : (Font)converter.ConvertFromString(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.BrandingFontColumn].ToString());
                    }
                    catch
                    {
                    }
                    brandingForeColor = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.BrandingForeColorColumn] == DBNull.Value ? Color.Black : Color.FromArgb(Int32.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.BrandingForeColorColumn].ToString()));
                    windowSeparatorSize = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.SeparatorSizeColumn] == DBNull.Value ? 50 : Int32.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.SeparatorSizeColumn].ToString());
                    try
                    {
                        defaultFolderFont = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.DefaultFolderFontColumn] == DBNull.Value ? new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel) : (Font)converter.ConvertFromString(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.DefaultFolderFontColumn].ToString());
                    }
                    catch
                    {
                    }
                    defaultFolderHeight = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.DefaultFolderHeightColumn] == DBNull.Value ? 350 : Int32.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.DefaultFolderHeightColumn].ToString());
                    showNewFiles = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.ShowNewFilesColumn] == DBNull.Value ? true : Boolean.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.ShowNewFilesColumn].ToString());
                    newFilesPeriod = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.NewFilesPeriodColumn] == DBNull.Value ? 3 : Int32.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.NewFilesPeriodColumn].ToString());
                    resetNewFilesWhileSync = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.ResetNewFilesWhileSyncColumn] == DBNull.Value ? true : Boolean.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.ResetNewFilesWhileSyncColumn].ToString());
                    disableBuggyTags = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.DisableBuggyTagsColumn] == DBNull.Value ? false : Boolean.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.DisableBuggyTagsColumn].ToString());
                    presentationMastersChanged = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.PresentationMastersChangedColumn] == DBNull.Value ? false : Boolean.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.PresentationMastersChangedColumn].ToString());
                    applyForAllWindows = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.ApplyForAllWindowsColumn] == DBNull.Value ? false : Boolean.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.ApplyForAllWindowsColumn].ToString());
                    syncLinkedFiles = storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.SyncLinksOnlyColumn] == DBNull.Value ? true : Boolean.Parse(storedSalesDepot.Stations.Rows[0][storedSalesDepot.Stations.SyncLinksOnlyColumn].ToString());

                    tabPages.Clear();
                    SalesDepotTabPage tabPage;
                    foreach (SalesDepotFiles.TabPagesRow row in storedSalesDepot.TabPages.Rows)
                    {
                        tabPage = new SalesDepotTabPage();
                        tabPage.TabPageName = row[storedSalesDepot.TabPages.TabPageNameColumn] == DBNull.Value ? string.Empty : row[storedSalesDepot.TabPages.TabPageNameColumn].ToString();
                        tabPage.IsChecked = row[storedSalesDepot.TabPages.IsCheckedColumn] == DBNull.Value ? false : Boolean.Parse(row[storedSalesDepot.TabPages.IsCheckedColumn].ToString());
                        tabPages.Add(tabPage);
                    }

                    presentationMasters.Clear();
                    PresentationMaster presentationMaster;
                    foreach (SalesDepotFiles.PresentationMastersRow row in storedSalesDepot.PresentationMasters.Rows)
                    {
                        presentationMaster = new PresentationMaster();
                        presentationMaster.MasterName = row[storedSalesDepot.PresentationMasters.MasterNameColumn] == DBNull.Value ? string.Empty : row[storedSalesDepot.PresentationMasters.MasterNameColumn].ToString();
                        presentationMasters.Add(presentationMaster);
                    }
                    isConfigured = true;
                }
            }

            if (LoadStorage())
            {
                Folder folder;
                SalesDepotFile file;
                foreach (SalesDepotFiles.FoldersRow row in storedSalesDepot.Folders.Rows)
                {
                    folder = CreateFolder();
                    folder.Name = row[storedSalesDepot.Folders.NameColumn] == DBNull.Value ? string.Empty : row[storedSalesDepot.Folders.NameColumn].ToString();
                    folder.ParentName = row[storedSalesDepot.Folders.ParentNameColumn] == DBNull.Value ? string.Empty : row[storedSalesDepot.Folders.ParentNameColumn].ToString();
                    folder.TabPageName = row[storedSalesDepot.Folders.TabPageNameColumn] == DBNull.Value ? string.Empty : row[storedSalesDepot.Folders.TabPageNameColumn].ToString();
                    folder.Order = row[storedSalesDepot.Folders.OrderColumn] == DBNull.Value ? -1 : Int32.Parse(row[storedSalesDepot.Folders.OrderColumn].ToString());
                    folder.Height = row[storedSalesDepot.Folders.HeightColumn] == DBNull.Value ? 350 : Int32.Parse(row[storedSalesDepot.Folders.HeightColumn].ToString());
                    folder.ColumnOrder = row[storedSalesDepot.Folders.ColumnOrderColumn] == DBNull.Value ? -1 : Int32.Parse(row[storedSalesDepot.Folders.ColumnOrderColumn].ToString());
                    try
                    {
                        folder.WindowFont = row[storedSalesDepot.Folders.WindowFontColumn] == DBNull.Value ? new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel) : (Font)converter.ConvertFromString(row[storedSalesDepot.Folders.WindowFontColumn].ToString());
                    }
                    catch
                    {
                    }
                    try
                    {
                        folder.HeaderFont = row[storedSalesDepot.Folders.HeaderFontColumn] == DBNull.Value ? new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel) : (Font)converter.ConvertFromString(row[storedSalesDepot.Folders.HeaderFontColumn].ToString());
                    }
                    catch
                    {
                    }
                    folder.BackgroundWindowColor = row[storedSalesDepot.Folders.BackgroundWindowColorColumn] == DBNull.Value ? Color.White : Color.FromArgb(Int32.Parse(row[storedSalesDepot.Folders.BackgroundWindowColorColumn].ToString()));
                    folder.ForeWindowColor = row[storedSalesDepot.Folders.ForeWindowColorColumn] == DBNull.Value ? Color.Black : Color.FromArgb(Int32.Parse(row[storedSalesDepot.Folders.ForeWindowColorColumn].ToString()));
                    folder.BackgroundHeaderColor = row[storedSalesDepot.Folders.BackgroundHeaderColorColumn] == DBNull.Value ? Color.White : Color.FromArgb(Int32.Parse(row[storedSalesDepot.Folders.BackgroundHeaderColorColumn].ToString()));
                    folder.ForeHeaderColor = row[storedSalesDepot.Folders.ForeHeaderColorColumn] == DBNull.Value ? Color.Black : Color.FromArgb(Int32.Parse(row[storedSalesDepot.Folders.ForeHeaderColorColumn].ToString()));

                    foreach (SalesDepotFiles.FilesRow fileRow in (SalesDepotFiles.FilesRow[])storedSalesDepot.Files.Select("FolderName = '" + Path.Combine(folder.ParentName,folder.Name) + "'"))
                    {
                        file = new SalesDepotFile();
                        file.DisplayName = fileRow[storedSalesDepot.Files.DisplayNameColumn] == DBNull.Value ? string.Empty : fileRow[storedSalesDepot.Files.DisplayNameColumn].ToString();
                        file.FullName = fileRow[storedSalesDepot.Files.FullNameColumn] == DBNull.Value ? string.Empty : fileRow[storedSalesDepot.Files.FullNameColumn].ToString();
                        while (file.FullName.StartsWith(@"\"))
                            file.FullName = file.FullName.Substring(1);
                        file.FolderName = fileRow[storedSalesDepot.Files.FolderNameColumn] == DBNull.Value ? string.Empty : fileRow[storedSalesDepot.Files.FolderNameColumn].ToString();
                        file.LastModificationDate = fileRow[storedSalesDepot.Files.LastModificationDateColumn] == DBNull.Value ? DateTime.Now : DateTime.Parse(fileRow[storedSalesDepot.Files.LastModificationDateColumn].ToString());
                        file.Type = fileRow[storedSalesDepot.Files.TypeColumn] == DBNull.Value ? FileTypes.Other : (FileTypes)Int32.Parse(fileRow[storedSalesDepot.Files.TypeColumn].ToString());
                        file.Format = fileRow[storedSalesDepot.Files.FormatColumn] == DBNull.Value ? string.Empty : fileRow[storedSalesDepot.Files.FormatColumn].ToString();
                        file.Order = fileRow[storedSalesDepot.Files.OrderColumn] == DBNull.Value ? 1 : Int32.Parse(fileRow[storedSalesDepot.Files.OrderColumn].ToString());
                        file.Image = null;
                        file.Note = fileRow[storedSalesDepot.Files.NoteColumn] == DBNull.Value ? string.Empty : fileRow[storedSalesDepot.Files.NoteColumn].ToString();
                        file.AddRowDate = fileRow[storedSalesDepot.Files.AddRowDateColumn] == DBNull.Value ? DateTime.Now : DateTime.Parse(fileRow[storedSalesDepot.Files.AddRowDateColumn].ToString());
                        file.ChangeRowDate = fileRow[storedSalesDepot.Files.ChangeRowDateColumn] == DBNull.Value ? DateTime.Now : DateTime.Parse(fileRow[storedSalesDepot.Files.ChangeRowDateColumn].ToString());
                        file.IsNew = fileRow[storedSalesDepot.Files.IsNewColumn] == DBNull.Value ? true : Boolean.Parse(fileRow[storedSalesDepot.Files.IsNewColumn].ToString());
                        file.IsModified = fileRow[storedSalesDepot.Files.IsModifiedColumn] == DBNull.Value ? false : Boolean.Parse(fileRow[storedSalesDepot.Files.IsModifiedColumn].ToString());
                        file.IsBold = fileRow[storedSalesDepot.Files.IsBoldColumn] == DBNull.Value ? false : Boolean.Parse(fileRow[storedSalesDepot.Files.IsBoldColumn].ToString());
                        folder.Files.Add(file);
                    }

                    folders.Add(folder);
                }
                if (storedSalesDepot.SyncOptions.Rows.Count > 0)
                    syncDate = storedSalesDepot.SyncOptions.Rows[0][storedSalesDepot.SyncOptions.SyncDateColumn] == DBNull.Value ? DateTime.UtcNow : DateTime.Parse(storedSalesDepot.SyncOptions.Rows[0][storedSalesDepot.SyncOptions.SyncDateColumn].ToString());
            }
        }

        public void SaveMetadata()
        {
            SaveStyle();
            SaveSettings();
        }

        public void SaveAll()
        {
            SaveStyle();
            SaveSettings();
            SaveStorage();
        }

        private void SaveStyle()
        {
            storedSalesDepot.SalesDepotStyle.Clear();
            SalesDepotFiles.SalesDepotStyleRow salesDepotStyleRow = storedSalesDepot.SalesDepotStyle.NewSalesDepotStyleRow();
            salesDepotStyleRow.BeginEdit();
            if (this.GetType() == typeof(BannerSalesDepot))
            {
                salesDepotStyleRow[storedSalesDepot.SalesDepotStyle.StyleColumn] = "Banner";
            }
            else if (this.GetType() == typeof(NoBannerSalesDepot))
            {
                salesDepotStyleRow[storedSalesDepot.SalesDepotStyle.StyleColumn] = "No Banner";
            }
            else if (this.GetType() == typeof(DWBSalesDepot))
            {
                salesDepotStyleRow[storedSalesDepot.SalesDepotStyle.StyleColumn] = "Digital Wall Bin";
            }
            salesDepotStyleRow.EndEdit();
            storedSalesDepot.SalesDepotStyle.AddSalesDepotStyleRow(salesDepotStyleRow);

            DataSet settingsData = new DataSet("Style");
            settingsData.Tables.Add(storedSalesDepot.SalesDepotStyle.Copy());
            settingsData.WriteXml(Path.Combine(_folder.FullName,AppManager.StyleFileName));
        }

        private void SaveSettings()
        {
            FontConverter converter = new FontConverter();
            storedSalesDepot.Stations.Clear();
            SalesDepotFiles.StationsRow stationsRow = storedSalesDepot.Stations.NewStationsRow();
            stationsRow.BeginEdit();
            stationsRow[storedSalesDepot.Stations.NameColumn] = _name;
            stationsRow[storedSalesDepot.Stations.BrandingTextColumn] = brandingText;
            stationsRow[storedSalesDepot.Stations.BrandingFontColumn] = converter.ConvertToString(brandingFont);
            stationsRow[storedSalesDepot.Stations.BrandingForeColorColumn] = brandingForeColor.ToArgb();
            stationsRow[storedSalesDepot.Stations.SeparatorSizeColumn] = windowSeparatorSize;
            stationsRow[storedSalesDepot.Stations.DefaultFolderFontColumn] = converter.ConvertToString(defaultFolderFont);
            stationsRow[storedSalesDepot.Stations.DefaultFolderHeightColumn] = defaultFolderHeight;
            stationsRow[storedSalesDepot.Stations.ShowNewFilesColumn] = showNewFiles;
            stationsRow[storedSalesDepot.Stations.NewFilesPeriodColumn] = newFilesPeriod;
            stationsRow[storedSalesDepot.Stations.ResetNewFilesWhileSyncColumn] = resetNewFilesWhileSync;
            stationsRow[storedSalesDepot.Stations.DisableBuggyTagsColumn] = disableBuggyTags;
            stationsRow[storedSalesDepot.Stations.PresentationMastersChangedColumn] = presentationMastersChanged;
            stationsRow[storedSalesDepot.Stations.ApplyForAllWindowsColumn] = applyForAllWindows;
            stationsRow[storedSalesDepot.Stations.SyncLinksOnlyColumn] = syncLinkedFiles;
            stationsRow.EndEdit();
            storedSalesDepot.Stations.AddStationsRow(stationsRow);

            storedSalesDepot.TabPages.Clear();
            SalesDepotFiles.TabPagesRow tabPagesRow;
            foreach (SalesDepotTabPage tabPage in tabPages)
            {
                tabPagesRow = storedSalesDepot.TabPages.NewTabPagesRow();
                tabPagesRow.BeginEdit();
                tabPagesRow[storedSalesDepot.TabPages.TabPageNameColumn] = tabPage.TabPageName;
                tabPagesRow[storedSalesDepot.TabPages.IsCheckedColumn] = tabPage.IsChecked;
                tabPagesRow.EndEdit();
                storedSalesDepot.TabPages.AddTabPagesRow(tabPagesRow);
            }

            storedSalesDepot.PresentationMasters.Clear();
            SalesDepotFiles.PresentationMastersRow presentationMastersRow;
            foreach (PresentationMaster presentationMaster in presentationMasters)
            {
                presentationMastersRow = storedSalesDepot.PresentationMasters.NewPresentationMastersRow();
                presentationMastersRow.BeginEdit();
                presentationMastersRow[storedSalesDepot.PresentationMasters.MasterNameColumn] = presentationMaster.MasterName;
                presentationMastersRow.EndEdit();
                storedSalesDepot.PresentationMasters.AddPresentationMastersRow(presentationMastersRow);
            }

            DataSet settingsData = new DataSet("Settings");
            settingsData.Tables.Add(storedSalesDepot.Stations.Copy());
            settingsData.Tables.Add(storedSalesDepot.TabPages.Copy());
            settingsData.Tables.Add(storedSalesDepot.PresentationMasters.Copy());
            settingsData.WriteXml(Path.Combine(_folder.FullName,AppManager.UserSettingsFileName));
        }

        private void SaveStorage()
        {
            FontConverter converter = new FontConverter();
            storedSalesDepot.Folders.Clear();
            storedSalesDepot.Files.Clear();
            SalesDepotFiles.FoldersRow foldersRow;
            SalesDepotFiles.FilesRow filesRow;
            foreach (Folder folder in folders)
            {
                foldersRow = storedSalesDepot.Folders.NewFoldersRow();
                foldersRow.BeginEdit();
                foldersRow[storedSalesDepot.Folders.NameColumn] = folder.Name;
                foldersRow[storedSalesDepot.Folders.ParentNameColumn] = folder.ParentName;
                foldersRow[storedSalesDepot.Folders.TabPageNameColumn] = folder.TabPageName;
                foldersRow[storedSalesDepot.Folders.OrderColumn] = folder.Order;
                foldersRow[storedSalesDepot.Folders.HeightColumn] = folder.Height;
                foldersRow[storedSalesDepot.Folders.ColumnOrderColumn] = folder.ColumnOrder;
                foldersRow[storedSalesDepot.Folders.WindowFontColumn] = converter.ConvertToString(folder.WindowFont);
                foldersRow[storedSalesDepot.Folders.HeaderFontColumn] = converter.ConvertToString(folder.HeaderFont);
                foldersRow[storedSalesDepot.Folders.BackgroundWindowColorColumn] = folder.BackgroundWindowColor.ToArgb();
                foldersRow[storedSalesDepot.Folders.ForeWindowColorColumn] = folder.ForeWindowColor.ToArgb();
                foldersRow[storedSalesDepot.Folders.BackgroundHeaderColorColumn] = folder.BackgroundHeaderColor.ToArgb();
                foldersRow[storedSalesDepot.Folders.ForeHeaderColorColumn] = folder.ForeHeaderColor.ToArgb();
                foldersRow.EndEdit();
                storedSalesDepot.Folders.AddFoldersRow(foldersRow);
                foreach (SalesDepotFile file in folder.Files)
                {
                    filesRow = storedSalesDepot.Files.NewFilesRow();
                    filesRow.BeginEdit();
                    filesRow[storedSalesDepot.Files.DisplayNameColumn] = file.DisplayName;
                    filesRow[storedSalesDepot.Files.FullNameColumn] = @"\" + file.FullName;
                    filesRow[storedSalesDepot.Files.FolderNameColumn] = file.FolderName;
                    filesRow[storedSalesDepot.Files.LastModificationDateColumn] = file.LastModificationDate;
                    filesRow[storedSalesDepot.Files.TypeColumn] = (int)file.Type;
                    filesRow[storedSalesDepot.Files.FormatColumn] = file.Format;
                    filesRow[storedSalesDepot.Files.OrderColumn] = file.Order;
                    filesRow[storedSalesDepot.Files.NoteColumn] = file.Note;
                    filesRow[storedSalesDepot.Files.AddRowDateColumn] = file.AddRowDate;
                    filesRow[storedSalesDepot.Files.ChangeRowDateColumn] = file.ChangeRowDate;
                    filesRow[storedSalesDepot.Files.IsNewColumn] = file.IsNew;
                    filesRow[storedSalesDepot.Files.IsModifiedColumn] = file.IsModified;
                    filesRow[storedSalesDepot.Files.IsBoldColumn] = file.IsBold;
                    filesRow.EndEdit();
                    storedSalesDepot.Files.AddFilesRow(filesRow);
                }
            }
            storedSalesDepot.SyncOptions.Clear();
            SalesDepotFiles.SyncOptionsRow syncOptionsRow = storedSalesDepot.SyncOptions.NewSyncOptionsRow();
            syncOptionsRow.BeginEdit();
            syncOptionsRow[storedSalesDepot.SyncOptions.SyncDateColumn] = syncDate;
            syncOptionsRow.EndEdit();
            storedSalesDepot.SyncOptions.AddSyncOptionsRow(syncOptionsRow);

            DataSet settingsData = new DataSet("Cache");
            settingsData.Tables.Add(storedSalesDepot.Folders.Copy());
            settingsData.Tables.Add(storedSalesDepot.Files.Copy());
            settingsData.Tables.Add(storedSalesDepot.SyncOptions.Copy());
            settingsData.WriteXml(Path.Combine(_folder.FullName,AppManager.StorageFileName));
        }

        public virtual void Synchronize()
        {
            if (isConfigured)
            {
                SaveAll();
            }
        }
    }

    /// <summary>
    /// Implementation of the Banner Sales Depot Object
    /// </summary>
    class BannerSalesDepot : SalesDepot
    {
        public BannerSalesDepot(string name, DirectoryInfo folder)
            : base(name, folder)
        {
        }

        private int GetOrder(DirectoryInfo folder)
        {
            try
            {
                return Convert.ToInt32(folder.Name.Substring(0, 2));
            }
            catch
            {
                return -1;
            }
        }

        protected override Folder CreateFolder()
        {
            return new BannerFolder(this);
        }

        public override void Init()
        {
            base.Init();
            foreach (BannerFolder folder in folders)
            {
                UpdateFolderProperties(folder);
                folder.UpdateFileProperties();
            }
            folders.Sort(new FoldersComparer());
        }

        public override void Synchronize()
        {
            this.syncDate = DateTime.UtcNow;
            if (isConfigured)
                FillStorage();
            base.Synchronize();
        }

        private void FillStorage()
        {
            BannerFolder folder;
            List<BannerFolder> newFolders = new List<BannerFolder>();
            IEnumerable<Folder> existedFolders;

            IEnumerable<SalesDepotTabPage> checkedTabpages =
                from salesDepotTabPage in tabPages
                where salesDepotTabPage.IsChecked
                select salesDepotTabPage;


            if (checkedTabpages.Count() > 1)
            {
                foreach (DirectoryInfo subFolderLevel1 in _folder.GetDirectories())
                {
                    foreach (DirectoryInfo subFolderLevel2 in subFolderLevel1.GetDirectories())
                    {
                        existedFolders =
                            from salesDepotFolder in folders
                            where salesDepotFolder.Name == subFolderLevel2.Name && salesDepotFolder.ParentName == subFolderLevel1.Name && salesDepotFolder.TabPageName == subFolderLevel1.Name
                            select salesDepotFolder;
                        if (existedFolders.Count() > 0)
                        {
                            folder = (BannerFolder)existedFolders.First();
                            folder.LoadFiles(subFolderLevel2);
                            newFolders.Add(folder);
                        }
                        else
                        {
                            folder = new BannerFolder(this);
                            folder.Order = GetOrder(subFolderLevel2);
                            if (folder.Order != -1)
                            {
                                folder.Name = subFolderLevel2.Name;
                                folder.ParentName = subFolderLevel1.Name;
                                folder.TabPageName = subFolderLevel1.Name;
                                folder.ColumnOrder = -1;
                                folder.LoadFiles(subFolderLevel2);
                                newFolders.Add(folder);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DirectoryInfo subSubFolder in _folder.GetDirectories())
                {
                    existedFolders =
                            from salesDepotFolder in folders
                            where salesDepotFolder.Name == subSubFolder.Name && salesDepotFolder.ParentName == subSubFolder.Parent.Name && salesDepotFolder.TabPageName == string.Empty
                            select salesDepotFolder;
                    if (existedFolders.Count() > 0)
                    {
                        folder = (BannerFolder)existedFolders.First();
                        folder.LoadFiles(subSubFolder);
                        newFolders.Add(folder);
                    }
                    else
                    {
                        folder = new BannerFolder(this);
                        folder.Order = GetOrder(subSubFolder);
                        if (folder.Order != -1)
                        {
                            folder.Name = subSubFolder.Name;
                            folder.ParentName = subSubFolder.Parent.Name;
                            folder.TabPageName = string.Empty;
                            folder.ColumnOrder = -1;
                            folder.LoadFiles(subSubFolder);
                            newFolders.Add(folder);
                        }
                    }
                }
            }
            presentationMastersChanged = false;
            
            folders.Clear();
            foreach (BannerFolder newFolder in newFolders)
                folders.Add(newFolder);
        }

        private void UpdateFolderProperties(BannerFolder folder)
        {
            //DirectoryInfo logoFolder = new DirectoryInfo(Path.Combine(_folder.FullName,Properties.Settings.Default.LogoPath));

            //if (tabPages.Count > 1)
            //    folder.LoadLogo(new DirectoryInfo(Path.Combine(logoFolder.FullName,folder.ParentName)));
            //else
            //    folder.LoadLogo(logoFolder);
        }
    }

    /// <summary>
    /// Implementation of the No banner Sales Depot Object
    /// </summary>
    class NoBannerSalesDepot : BannerSalesDepot
    {
        public NoBannerSalesDepot(string name, DirectoryInfo folder)
            : base(name, folder)
        {
        }
    }

    /// <summary>
    /// Implementation of the Digital Wall Bin Sales Depot Object
    /// </summary>
    public class DWBSalesDepot : SalesDepot
    {
        public DWBSalesDepot(string name, DirectoryInfo folder)
            : base(name, folder)
        {
        }

        protected override Folder CreateFolder()
        {
            return new DWBFolder(this);
        }

        private void CheckIfDead(SalesDepotFile salesDepotFile)
        {
            switch (salesDepotFile.Type)
            {
                case FileTypes.MediaPlayerVideo:
                case FileTypes.Other:
                case FileTypes.OtherPresentation:
                    if (!File.Exists(Path.Combine(this._folder.FullName,salesDepotFile.FullName)))
                    {
                        salesDepotFile.IsDead = true;
                        deadLinks.Add(salesDepotFile);
                    }
                    else
                        salesDepotFile.IsDead = false;
                    break;
                case FileTypes.Folder:
                    if (!Directory.Exists(Path.Combine(this._folder.FullName,salesDepotFile.FullName)))
                    {
                        salesDepotFile.IsDead = true;
                        deadLinks.Add(salesDepotFile);
                    }
                    else
                        salesDepotFile.IsDead = false;
                    break;
            }
        }

        public void FindDeadLinks()
        {
            deadLinks.Clear();
            foreach (DWBFolder folder in folders)
            {
                foreach (SalesDepotFile file in folder.Files)
                    CheckIfDead(file);
            }
        }

        public void DeleteDeadLinks(List<Guid> deletedDeadLinks)
        {
            deadLinks.Clear();
            foreach (DWBFolder folder in folders)
            {
                for (int i = folder.Files.Count - 1; i >= 0; i--)
                {
                    foreach (Guid deadLink in deletedDeadLinks)
                        if (folder.Files[i].Identifier.Equals(deadLink))
                            folder.Files.RemoveAt(i);
                }
            }
            FindDeadLinks();
        }

        public override void Init()
        {
            base.Init();
            foreach (DWBFolder folder in folders)
                folder.UpdateFileProperties();
            FindDeadLinks();
            folders.Sort(new FoldersComparer());
        }

        public override void Synchronize()
        {
            this.syncDate = DateTime.UtcNow;
            foreach (DWBFolder folder in this.folders)
                folder.GetFileProperties();
            base.Synchronize();
        }

        public DWBSalesDepot Copy()
        {
            DWBSalesDepot copy = new DWBSalesDepot(this._name, this._folder);
            copy.BrandingText = this.brandingText;
            copy.BrandingFont = this.brandingFont;
            copy.BrandingForeColor = this.brandingForeColor;
            copy.WindowSeparatorSize = this.windowSeparatorSize;
            copy.DefaultFolderFont = this.defaultFolderFont;
            copy.DefaultFolderHeight = this.defaultFolderHeight;
            copy.MultiLogo = this.multiLogo;
            copy.ShowNewFiles = this.showNewFiles;
            copy.NewFilesPeriod = this.newFilesPeriod;
            copy.ResetNewFilesWhileSync = this.resetNewFilesWhileSync;
            copy.DisableBuggyTags = this.disableBuggyTags;
            copy.PresentationMastersChanged = this.presentationMastersChanged;
            copy.ApplyForAllWindows = this.applyForAllWindows;
            copy.SyncLinkedFiles = this.syncLinkedFiles;
            copy.IsConfigured = this.isConfigured;
            copy.SyncDate = this.syncDate;
            foreach (Folder folder in this.folders)
                copy.Folders.Add(folder.Copy(copy));
            return copy;
        }
    }

    /// <summary>
    /// Implementation of the Sales Depot Folder
    /// </summary>
    public class Folder
    {
        protected Guid _identifier = Guid.NewGuid();
        protected SalesDepot parent;
        protected string folderName;
        protected string parentName;
        protected string tabPageName;
        protected double order = -1;
        protected int height = 350;
        protected int columnOrder = -1;
        protected Color backgroundWindowColor = Color.White;
        protected Color foreWindowColor = Color.Black;
        protected Color backgroundHeaderColor = Color.White;
        protected Color foreHeaderColor = Color.Black;
        protected Font windowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        protected Font headerFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);

        protected List<SalesDepotFile> files = new List<SalesDepotFile>();

        public Folder(SalesDepot parentSalesDepot)
        {
            parent = parentSalesDepot;
        }

        #region Public Properties
        public Guid Identifier
        {
            get
            {
                return _identifier;
            }
        }
        public string Name
        {
            set
            {
                folderName = value;
            }
            get
            {
                return folderName;
            }
        }

        public string ParentName
        {
            set
            {
                parentName = value;
            }
            get
            {
                return parentName;
            }
        }

        public string TabPageName
        {
            set
            {
                tabPageName = value;
            }
            get
            {
                return tabPageName;
            }
        }

        public double Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        public int Height
        {
            set
            {
                height = value;
            }
            get
            {
                return height;
            }
        }

        public int ColumnOrder
        {
            set
            {
                columnOrder = value;
            }
            get
            {
                return columnOrder;
            }
        }

        public Color BackgroundWindowColor
        {
            set
            {
                backgroundWindowColor = value;
            }
            get
            {
                return backgroundWindowColor;
            }
        }

        public Color ForeWindowColor
        {
            set
            {
                foreWindowColor = value;
            }
            get
            {
                return foreWindowColor;
            }
        }

        public Color BackgroundHeaderColor
        {
            set
            {
                backgroundHeaderColor = value;
            }
            get
            {
                return backgroundHeaderColor;
            }
        }

        public Color ForeHeaderColor
        {
            set
            {
                foreHeaderColor = value;
            }
            get
            {
                return foreHeaderColor;
            }
        }

        public Font WindowFont
        {
            get
            {
                return windowFont;
            }
            set
            {
                windowFont = value;
            }
        }

        public Font HeaderFont
        {
            get
            {
                return headerFont;
            }
            set
            {
                headerFont = value;
            }
        }

        public List<SalesDepotFile> Files
        {
            get
            {
                return files;
            }
        }

        public SalesDepot ParentSalesDepot
        {
            get
            {
                return parent;
            }
        }
        #endregion

        protected string GetFileExtension(string filename)
        {
            if (filename.Contains('.'))
                return filename.Substring(filename.LastIndexOf('.'), filename.Length - filename.LastIndexOf('.'));
            else
                return string.Empty;
        }

        public Folder Copy(SalesDepot parentSalesDepot)
        {
            Folder copy = new Folder(parentSalesDepot);
            copy.Name = this.folderName;
            copy.ParentName = this.parentName;
            copy.TabPageName = this.tabPageName;
            copy.Order = this.order;
            copy.Height = this.height;
            copy.ColumnOrder = this.columnOrder;
            copy.BackgroundHeaderColor = this.backgroundHeaderColor;
            copy.BackgroundWindowColor = this.backgroundWindowColor;
            copy.ForeHeaderColor = this.foreHeaderColor;
            copy.ForeWindowColor = this.foreWindowColor;
            copy.WindowFont = this.windowFont;
            copy.HeaderFont = this.headerFont;
            foreach (SalesDepotFile file in this.files)
                copy.Files.Add(file);
            return copy;
        }
    }

    /// <summary>
    /// Implementation of the Banner Sales Depot Folder
    /// </summary>
    class BannerFolder : Folder
    {
        private string fileNameForSearch = "";
        private Bitmap logo;

        public BannerFolder(SalesDepot parentSalesDepot)
            : base(parentSalesDepot)
        {
        }

        public Bitmap Logo
        {
            get
            {
                return logo;
            }
        }

        public void LoadFiles(DirectoryInfo folder)
        {
            SalesDepotFile salesDepotFile;
            SalesDepotFile existedSalesDepotFile;
            List<SalesDepotFile> newFiles = new List<SalesDepotFile>();

            foreach (FileInfo file in folder.GetFiles())
            {
                if (file.Exists)
                {
                    if (file.Name.ToLower() == "thumbs.db")
                        continue;
                    IEnumerable<SalesDepotFile> existedFiles =
                            from existedFile in files
                            where existedFile.DisplayName == file.Name
                            select existedFile;
                    
                    salesDepotFile = new SalesDepotFile();
                    salesDepotFile.FullName = file.FullName.Replace(parent.Folder.FullName,"");
                    salesDepotFile.FolderName = Path.Combine(this.ParentName,this.Name);
                    salesDepotFile.LastModificationDate = file.LastWriteTimeUtc;
                    
                    if (existedFiles.Count() > 0)
                    {
                        existedSalesDepotFile = existedFiles.First();
                        salesDepotFile.DisplayName = existedSalesDepotFile.DisplayName;
                        salesDepotFile.AddRowDate = existedSalesDepotFile.AddRowDate;
                        salesDepotFile.ChangeRowDate = existedSalesDepotFile.ChangeRowDate;
                        salesDepotFile.IsNew = existedSalesDepotFile.IsNew;
                        salesDepotFile.IsModified = existedSalesDepotFile.IsModified;
                        if (FileHasBeenChanged(salesDepotFile,existedSalesDepotFile) || parent.PresentationMastersChanged)
                            GetFileProperties(salesDepotFile);
                    }
                    else
                    {
                        salesDepotFile.DisplayName = file.Name;
                        salesDepotFile.AddRowDate = parent.SyncDate;
                        salesDepotFile.ChangeRowDate = parent.SyncDate;
                        salesDepotFile.IsNew = true;
                        salesDepotFile.IsModified = false;
                        GetFileProperties(salesDepotFile);
                    }
                    newFiles.Add(salesDepotFile);
                }
            }
            files.Clear();
            foreach (SalesDepotFile file in newFiles)
                files.Add(file);
        }

        private void GetFileProperties(SalesDepotFile file)
        {
            file.Type = FileTypes.Other;
            string presentationFormat = "";
            string extension = GetFileExtension(file.FullName);
            if (extension.ToUpper() == ".PPT" ||
                extension.ToUpper() == ".PPTX")
            {
                file.Type = FileTypes.OtherPresentation;
                bool isFriendly;
                bool isBuggy;
                PowerPointHelper.Instance.GetPresentationProperties(parent.Folder.FullName + file.FullName, parent, out isFriendly, out isBuggy, out presentationFormat);
                if (isFriendly)
                    file.Type = FileTypes.FriendlyPresentation;
                if (isBuggy)
                    file.Type = FileTypes.BuggyPresentation;
            }
            if ((extension.ToUpper() == ".MPEG")
                || (extension.ToUpper() == ".MPG")
                || (extension.ToUpper() == ".WMV")
                || (extension.ToUpper() == ".ASF")
                || (extension.ToUpper() == ".AVI")
                )
            {
                file.Type = FileTypes.MediaPlayerVideo;
            }
            if ((extension.ToUpper() == ".MOV")
                || (extension.ToUpper() == ".MP4")
                )
            {
                file.Type = FileTypes.QuickTimeVideo;
            }
            file.Format = presentationFormat;
        }

        private bool FileHasBeenChanged(SalesDepotFile newFile, SalesDepotFile oldFile)
        {
            newFile.AddRowDate = oldFile.AddRowDate;
            newFile.ChangeRowDate = oldFile.ChangeRowDate;

            bool resetNewTag = (newFile.AddRowDate < parent.SyncDate) & parent.ResetNewFilesWhileSync;
            bool resetModifiedTag = (newFile.ChangeRowDate < parent.SyncDate) & parent.ResetNewFilesWhileSync;

            newFile.IsNew = oldFile.IsNew & (!resetNewTag) & (newFile.ChangeRowDate == newFile.AddRowDate);

            if (oldFile.LastModificationDate.Date == newFile.LastModificationDate.Date && 
                oldFile.LastModificationDate.Hour == newFile.LastModificationDate.Hour && 
                oldFile.LastModificationDate.Minute == newFile.LastModificationDate.Minute && 
                oldFile.LastModificationDate.Second == newFile.LastModificationDate.Second)
            {
                newFile.Type = oldFile.Type;
                newFile.Format = oldFile.Format;
                newFile.IsModified = oldFile.IsModified & (!resetModifiedTag);
                return false;
            }
            else
            {
                newFile.ChangeRowDate = parent.SyncDate;
                newFile.IsModified = true;
                return true;
            }
        }

        private bool FileExisted(SalesDepotFile file)
        {
            if (file.DisplayName.Equals(fileNameForSearch))
                return true;
            else
                return false;
        }

        private FileInfo GetFolderLogoFile(DirectoryInfo logoFolder)
        {
            string searchPattern = "SDBanner" + order.ToString() + ".*";
            if (!logoFolder.Exists)
                return null;
            FileInfo[] files = logoFolder.GetFiles(searchPattern);
            foreach (FileInfo logoFile in files)
            {
                return logoFile;
            }
            return null;
        }

        public void LoadLogo(DirectoryInfo logoFolder)
        {
            FileInfo logoFile = GetFolderLogoFile(logoFolder);
            if (logoFile != null)
                logo = new Bitmap(logoFile.FullName);
            else
                logo = null;
        }

        private int GetFileOrder(SalesDepotFile file)
        {
            int result = 0;
            int period;

            if (file.Type == FileTypes.BuggyPresentation && !parent.DisableBuggyTags)
                result = -1;
            else
            {
                DateTime addRowDate = file.AddRowDate;
                DateTime changeRowDate = file.ChangeRowDate;
                if (parent.ShowNewFiles)
                {
                    if (((addRowDate < parent.SyncDate) && parent.ResetNewFilesWhileSync) || (addRowDate < changeRowDate) || !file.IsNew)
                        result = 0;
                    else
                    {
                        period = DateTime.UtcNow.Subtract(addRowDate.Date).Days;
                        if (period <= parent.NewFilesPeriod)
                            result = 2;
                        else
                            result = 0;
                    }
                    if (result != 2)
                    {
                        if (((changeRowDate < parent.SyncDate) && parent.ResetNewFilesWhileSync) || !file.IsModified)
                            result = 0;
                        else
                        {
                            period = DateTime.UtcNow.Subtract(changeRowDate.Date).Days;
                            if (period <= parent.NewFilesPeriod)
                                result = 1;
                            else
                                result = 0;
                        }
                    }
                }
                else
                    result = 0;
            }
            return result;
        }

        public void UpdateFileProperties()
        {
            //foreach (SalesDepotFile file in files)
            //{
            //    file.FullName = parent.Folder.FullName + file.FullName;
            //    int order = GetFileOrder(file);
            //    switch (order)
            //    {
            //        case -1:
            //            file.Image = ReadBitmap2ByteArray(Properties.Resources.Bug);
            //            break;
            //        case 1:
            //            file.Image = ReadBitmap2ByteArray(Properties.Resources.Edit);
            //            break;
            //        case 2:
            //            file.Image = ReadBitmap2ByteArray(Properties.Resources.Green_Plus);
            //            break;
            //        default:
            //            file.Image = ReadBitmap2ByteArray(Properties.Resources.EmptyImage);
            //            break;
            //    }
            //    file.Order = parent.DisableBuggyTags && file.Type == FileTypes.BuggyPresentation ? -2 : order;
            //}
        }

        private Byte[] ReadBitmap2ByteArray(Bitmap img)
        {
            MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }

    /// <summary>
    /// Implementation of the Banner Sales Depot Folder
    /// </summary>
    public class DWBFolder : Folder
    {
        public DWBFolder(SalesDepot parentSalesDepot)
            : base(parentSalesDepot)
        {
        }

        public void UpdateFileProperties()
        {
            //foreach (SalesDepotFile file in files)
            //    if (file.Type != FileTypes.Network && file.Type != FileTypes.Url)
            //        file.FullName = (parent.Folder.FullName + file.FullName);
        }

        public void GetFileProperties()
        {
            string extension;
            foreach (SalesDepotFile file in this.files)
            {
                if (file.Type != FileTypes.Folder && file.Type != FileTypes.LineBreak && file.Type != FileTypes.Url && file.Type != FileTypes.Network)
                {
                    extension = GetFileExtension(file.FullName).ToUpper();
                    switch (extension)
                    {
                        case ".PPT":
                        case ".PPTX":
                            file.Type = FileTypes.OtherPresentation;
                            break;
                        case ".MPEG":
                        case ".MPG":
                        case ".WMV":
                        case ".ASF":
                        case ".AVI":
                            file.Type = FileTypes.MediaPlayerVideo;
                            break;
                        case ".MOV":
                        case ".MP4":
                            file.Type = FileTypes.MediaPlayerVideo;
                            break;
                        default:
                            file.Type = FileTypes.Other;
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Implementation of the Sales Depot Folder Comparer
    /// </summary>
    class FoldersComparer : IComparer<Folder>
    {
        public int Compare(Folder x, Folder y)
        {
            if (x.ColumnOrder < y.ColumnOrder)
            {
                return -1;
            }
            if (x.ColumnOrder > y.ColumnOrder)
                return 1;
            if (x.ColumnOrder == y.ColumnOrder)
            {
                if (x.Order < y.Order)
                {
                    return -1;
                }
                if (x.Order > y.Order)
                    return 1;
                if (x.Order == y.Order)
                {
                    return 0;
                }
            }
            return 0;
        }
    }

    /// <summary>
    /// Implementation of the Sales Depot File
    /// </summary>
    public class SalesDepotFile
    {
        protected Guid _identifier = Guid.NewGuid();
        protected string _displayName;
        protected string _fullName;
        protected string _folderName;
        protected DateTime _lastModificationDate;
        protected FileTypes _type;
        protected string _format;
        protected int _order;
        protected Byte[] _image;
        protected string _note;
        protected DateTime _addRowDate;
        protected DateTime _changeRowDate;
        protected bool _isNew;
        protected bool _isModified;
        protected bool _isBold;
        protected bool _isDead = false;

        public Guid Identifier
        {
            get
            {
                return _identifier;
            }
        }

        public string DisplayName 
        {
            get 
            {
                if (_isDead && AppManager.Instance.Settings.processDeadLinks == 1)
                {
                    if (AppManager.Instance.Settings.markDeadLinksAsBold == 1)
                    {
                        if (!_displayName.Contains("INACTIVE!"))
                            return "INACTIVE! " + _displayName;
                        else
                            return _displayName;
                    }
                    else if (AppManager.Instance.Settings.markDeadLinksAsLineBreak == 1)
                        return "";
                    else
                        return _displayName;
                }
                else
                    return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }
        
        public string FullName 
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }
        
        public string FolderName 
        {
            get 
            {
                return _folderName;
            }
            set
            {
                _folderName = value;
            }
        }
        
        public DateTime LastModificationDate 
        {
            get 
            {
                return _lastModificationDate;
            }
            set 
            {
                _lastModificationDate = value;
            }
        }
        
        public FileTypes Type 
        {
            get 
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        
        public string Format 
        {
            get 
            {
                return _format;
            }
            set
            {
                _format = value;
            }
        }
        
        public int Order 
        {
            get 
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }
        
        public Byte[] Image 
        {
            get 
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        
        public string Note 
        {
            get 
            {
                if (_isDead && AppManager.Instance.Settings.processDeadLinks == 1 && (AppManager.Instance.Settings.markDeadLinksAsBold == 1 || AppManager.Instance.Settings.markDeadLinksAsLineBreak == 1))
                    return "";
                else
                    return _note;
                
            }
            set
            {
                _note = value;
            }
        }
        
        public DateTime AddRowDate 
        {
            get 
            {
                return _addRowDate;
            }
            set
            {
                _addRowDate = value;
            }
        }
        
        public DateTime ChangeRowDate 
        {
            get 
            {
                return _changeRowDate;
            }
            set
            {
                _changeRowDate = value;
            }
        }
        
        public bool IsNew 
        {
            get 
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
            }
        }
        
        public bool IsModified 
        {
            get
            {
                return _isModified;
            }
            set
            {
                _isModified = value;
            }
        }
        
        public bool IsBold 
        {
            get 
            {
                if (_isDead && AppManager.Instance.Settings.processDeadLinks == 1 && AppManager.Instance.Settings.markDeadLinksAsBold == 1)
                    return true;
                else
                    return _isBold;
            }
            set
            {
                _isBold = value;
            }
        }
        
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            set
            {
                _isDead = value;
            }
        }
    }

    /// <summary>
    /// Implementation of the Sales Depot File Comparers
    /// </summary>
    class SalesDepotFilesComparerDesc : IComparer<SalesDepotFile>
    {
        public int Compare(SalesDepotFile x, SalesDepotFile y)
        {
            if (x.Order > y.Order)
                return -1;
            if (x.Order < y.Order)
                return 1;
            if (x.Order == y.Order)
                return x.DisplayName.CompareTo(y.DisplayName);
            return 0;
        }
    }

    class SalesDepotFilesComparerAsc : IComparer<SalesDepotFile>
    {
        public int Compare(SalesDepotFile x, SalesDepotFile y)
        {
            if (x.Order < y.Order)
                return -1;
            if (x.Order > y.Order)
                return 1;
            if (x.Order == y.Order)
                return x.DisplayName.CompareTo(y.DisplayName);
            return 0;
        }
    }

    /// <summary>
    /// Enumeration of the file types
    /// </summary>
    public enum FileTypes
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

    /// <summary>
    /// Implementation of the Sales Depot Tab Page
    /// </summary>
    public class SalesDepotTabPage
    {
        public string TabPageName { get; set; }
        public bool IsChecked { get; set; }
    }

    /// <summary>
    /// Implementation of the Presentation master
    /// </summary>
    public class PresentationMaster
    {
        public string MasterName { get; set; }
    }

    /// <summary>
    /// Implementation of the FileInfo comparer
    /// </summary>
    class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    /// <summary>
    /// Implementation of the DirectoryInfo comparer
    /// </summary>
    class DirectoryInfoComparer : IComparer<DirectoryInfo>
    {
        public int Compare(DirectoryInfo x, DirectoryInfo y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    /// <summary>
    /// Implementation of the DWBFolderBoxControl comparer
    /// </summary>
    class BoxesComparer : IComparer<DWBFolderBoxControl>
    {
        public int Compare(DWBFolderBoxControl x, DWBFolderBoxControl y)
        {
            if (x.Column < y.Column)
            {
                return -1;
            }
            if (x.Column > y.Column)
                return 1;
            if (x.Column == y.Column)
            {
                if (x.Order < y.Order)
                {
                    return -1;
                }
                if (x.Order > y.Order)
                    return 1;
                if (x.Order == y.Order)
                {
                    return 0;
                }
            }
            return 0;
        }
    }

    /// <summary>
    /// Implementation of the DWB Window Copy-Paster
    /// </summary>
    static class DWBWindowsCopyPaster
    {
        private static Folder copiedFolder = null;
        private static bool pasteReady = false;
        private static DataGridView copiedGrid = null;
        private static int sourceRowIndex = -1;

        public static bool PasteReady
        {
            get
            {
                return pasteReady;
            }
            set
            {
                pasteReady = value;
            }
        }

        public static Folder CopiedFolder
        {
            get
            {
                return copiedFolder;
            }
            set
            {
                copiedFolder = value;
            }

        }

        public static void Copy(DataGridView sourceGrid, int rowIndex)
        {
            copiedGrid = sourceGrid;
            pasteReady = true;
            sourceRowIndex = rowIndex;
        }

        public static void Paste(int destColumnNumber, DataGridView destGrid)
        {
            copiedGrid.Rows.RemoveAt(sourceRowIndex);

            if (destGrid.SelectedRows.Count > 0)
                destGrid.Rows.Insert(destGrid.SelectedRows[0].Index, copiedFolder.Name, copiedFolder.Name);
            else
                destGrid.Rows.Add(copiedFolder.Name, copiedFolder.Name);

            copiedFolder.ParentName = "Column" + destColumnNumber.ToString();
            copiedFolder.ColumnOrder = destColumnNumber;
            foreach (SalesDepotFile file in copiedFolder.Files)
                file.FolderName = Path.Combine(copiedFolder.ParentName,copiedFolder.Name);
            copiedGrid = null;
            copiedFolder = null;
            pasteReady = false;
            sourceRowIndex = -1;
        }
    }
}