using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AutoSynchronizer.ToolClasses
{
    class SyncManager
    {
        private static SyncManager _instance = new SyncManager();

        private SyncManager()
        {
        }

        public static SyncManager Instance
        {
            get
            {
                return _instance;
            }
        }

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
                FileInfo bfi = b_files.Where(x => x.Name.Equals(afi.Name)).FirstOrDefault();
                if (bfi != null)
                {
                    if (afi.LastWriteTime > bfi.LastWriteTime)
                    {
                        afi.CopyTo(Path.Combine(B.FullName, afi.Name), true);
                    }
                }
                else
                {
                    afi.CopyTo(Path.Combine(B.FullName, afi.Name), true);
                }
                Application.DoEvents();
            }

            foreach (FileInfo bfi in b_files)
            {
                if ((!filesWhiteList.Contains(Path.Combine(A.FullName, bfi.Name)) && filesWhiteList.Count > 0) || (!a_files.Select(x => x.Name).Contains(bfi.Name) && filesWhiteList.Count == 0))
                {
                    bfi.Attributes = FileAttributes.Normal;
                    bfi.Delete();
                    Application.DoEvents();
                }
            }
            if (includeSubFolders)
            {
                foreach (DirectoryInfo adi in a_dirs.Values)
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
                    }
                    Application.DoEvents();
                }

                foreach (DirectoryInfo bdi in b_dirs.Values)
                {
                    if (!a_dirs.ContainsKey(bdi.Name))
                    {
                        DeleteFolder(bdi);
                        Application.DoEvents();
                    }
                }
            }
        }

        public void MakeFolderAvailable(DirectoryInfo folder)
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

        public void DeleteFolder(DirectoryInfo folder, string filter = "")
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
}
