using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolClasses
{
    public class SyncManager
    {
        public event EventHandler<SyncEventArgs> FileCreated;
        public event EventHandler<SyncEventArgs> FileUpdated;
        public event EventHandler<SyncEventArgs> FileDeleted;
        public event EventHandler<SyncEventArgs> FileDeclined;

        public event EventHandler<SyncEventArgs> FolderCreated;
        public event EventHandler<SyncEventArgs> FolderDeleted;

        public void SynchronizeFolders(DirectoryInfo A, DirectoryInfo B, HashSet<string> filesWhiteList, bool includeSubFolders = true)
        {
            FileInfo[] a_files = A.GetFiles().Where(x => filesWhiteList.Contains(x.FullName) || filesWhiteList.Count == 0).ToArray();
            Dictionary<string, DirectoryInfo> a_dirs = new Dictionary<string, DirectoryInfo>();
            DirectoryInfo[] tempDirs = A.GetDirectories();
            foreach (DirectoryInfo di in tempDirs.Where(x => filesWhiteList.Where(y => Path.GetDirectoryName(y).Contains(x.FullName)).Count() > 0 || filesWhiteList.Count == 0))
                a_dirs.Add(di.Name, di);
            Application.DoEvents();

            FileInfo[] b_files = B.GetFiles();
            Dictionary<string, DirectoryInfo> b_dirs = new Dictionary<string, DirectoryInfo>();
            tempDirs = B.GetDirectories();
            foreach (DirectoryInfo di in tempDirs)
                b_dirs.Add(di.Name, di);
            Application.DoEvents();

            foreach (FileInfo afi in a_files)
            {
                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    string destinationPath = Path.Combine(B.FullName, afi.Name);
                    if (destinationPath.Length < InteropClasses.WinAPIHelper.MAX_PATH)
                    {
                        FileInfo bfi = b_files.Where(x => x.Name.Equals(afi.Name)).FirstOrDefault();
                        if (bfi != null)
                        {
                            if (afi.LastWriteTime > bfi.LastWriteTime)
                            {
                                afi.CopyTo(destinationPath, true);
                                if (this.FileUpdated != null)
                                    this.FileUpdated(this, new SyncEventArgs(afi.FullName, bfi.FullName));
                            }
                        }
                        else
                        {
                            bfi = afi.CopyTo(destinationPath, true);
                            if (this.FileCreated != null)
                                this.FileCreated(this, new SyncEventArgs(afi.FullName, bfi.FullName));
                        }
                    }
                    else
                        if (this.FileDeclined != null)
                            this.FileDeclined(this, new SyncEventArgs(afi.FullName, destinationPath));
                    Application.DoEvents();
                }
                else
                    break;
            }

            foreach (FileInfo bfi in b_files)
            {
                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    if ((!filesWhiteList.Contains(Path.Combine(A.FullName, bfi.Name)) && filesWhiteList.Count > 0) || (!a_files.Select(x => x.Name).Contains(bfi.Name) && filesWhiteList.Count == 0))
                    {
                        bfi.Attributes = FileAttributes.Normal;
                        bfi.Delete();
                        if (this.FileDeleted != null)
                            this.FileDeleted(this, new SyncEventArgs("empty", bfi.FullName));
                        Application.DoEvents();
                    }
                }
                else
                    break;
            }

            if (includeSubFolders)
            {
                foreach (DirectoryInfo adi in a_dirs.Values)
                {
                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                    {
                        if (b_dirs.ContainsKey(adi.Name))
                        {
                            DirectoryInfo bdi = b_dirs[adi.Name];
                            SynchronizeFolders(adi, bdi, filesWhiteList);
                        }
                        else
                        {
                            DirectoryInfo bdi = B.CreateSubdirectory(adi.Name);
                            SynchronizeFolders(adi, bdi, filesWhiteList);
                            if (this.FolderCreated != null)
                                this.FolderCreated(this, new SyncEventArgs(adi.FullName, bdi.FullName));
                        }
                        Application.DoEvents();
                    }
                    else
                        break;
                }

                if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                {
                    foreach (DirectoryInfo bdi in b_dirs.Values)
                    {
                        if (!a_dirs.ContainsKey(bdi.Name))
                        {
                            DeleteFolder(bdi);
                            if (this.FolderDeleted != null)
                                this.FolderDeleted(this, new SyncEventArgs("empty", bdi.FullName));
                            Application.DoEvents();
                        }
                    }
                }
            }
        }

        public static void MakeFolderAvailable(DirectoryInfo folder)
        {
            try
            {
                foreach (DirectoryInfo subFolder in folder.GetDirectories())
                    MakeFolderAvailable(subFolder);
                foreach (FileInfo file in folder.GetFiles())
                    if (File.Exists(file.FullName))
                        File.SetAttributes(file.FullName, FileAttributes.Normal);
            }
            catch
            {
            }
        }

        public static void DeleteFolder(DirectoryInfo folder, string filter = "")
        {
            try
            {
                MakeFolderAvailable(folder);
                foreach (DirectoryInfo subFolder in folder.GetDirectories())
                    DeleteFolder(subFolder, filter);
                foreach (FileInfo file in folder.GetFiles())
                {
                    try
                    {
                        if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                            File.Delete(file.FullName);
                    }
                    catch
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(100);
                            if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                                File.Delete(file.FullName);
                        }
                        catch
                        {
                        }
                    }
                }
                try
                {
                    if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                        Directory.Delete(folder.FullName, false);
                }
                catch
                {
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
                            Directory.Delete(folder.FullName, false);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
    }

    public class SyncEventArgs : EventArgs
    {
        public string Source { get; private set; }
        public string Destination { get; private set; }

        public SyncEventArgs(string source, string destination)
        {
            this.Source = source;
            this.Destination = destination;
        }
    }
}
