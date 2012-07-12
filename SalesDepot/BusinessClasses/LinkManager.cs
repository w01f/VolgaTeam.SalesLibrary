using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace SalesDepot.BusinessClasses
{
    public class LinkManager
    {
        private static LinkManager _instance = null;

        private ToolForms.WallBin.FormPowerPointQuickView _formPowerPointQuickView = null;
        private ToolForms.WallBin.FormPowerPointQuickViewOld _formPowerPointQuickViewOld = null;
        private ToolForms.WallBin.FormLinkPreview _formLinkPreview = null;

        public static LinkManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LinkManager();
                return _instance;
            }
        }

        private LinkManager()
        {
            _formLinkPreview = new ToolForms.WallBin.FormLinkPreview();
            _formPowerPointQuickView = new ToolForms.WallBin.FormPowerPointQuickView();
            _formPowerPointQuickViewOld = new ToolForms.WallBin.FormPowerPointQuickViewOld();
        }

        public void OpenLink(LibraryFile link)
        {
            FileInfo sourceFile = null;
            if (link.Type != FileTypes.LineBreak && link.Type != FileTypes.Folder && link.Type != FileTypes.Url && link.Type != FileTypes.Network)
            {
                sourceFile = RequestFile(link);
                if (sourceFile == null)
                {
                    AppManager.Instance.ShowWarning("File or Link is Not Active");
                    AppManager.Instance.ActivityManager.AddLinkAccessActivity("Link not active", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
                    return;
                }
            }

            AppManager.Instance.ActivityManager.AddLinkAccessActivity("Link Access", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

            switch (link.Type)
            {
                case FileTypes.BuggyPresentation:
                case FileTypes.FriendlyPresentation:
                case FileTypes.Presentation:
                    switch (ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                    {
                                        OpenCopyOfFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                    {
                                        SaveFile("Save copy of the file as", link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                    {
                                        PrintFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Email)
                                    {
                                        InteropClasses.PowerPointHelper.Instance.OpenSlideSourcePresentation(new FileInfo(link.LocalPath));
                                        using (ToolForms.WallBin.FormEmailPresentation form = new ToolForms.WallBin.FormEmailPresentation())
                                        {
                                            form.SelectedFile = link;
                                            form.ActiveSlide = 1;
                                            form.rbActiveSlide.Visible = false;
                                            form.ShowDialog();
                                        }
                                    }
                                }
                            }
                            break;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenCopyOfFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            if (link.PreviewContainer != null && link.PreviewContainer.CheckPreviewImages() && !ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView)
                            {
                                ViewPresentation(link);
                                link.PreviewContainer.ReleasePreviewImages();
                            }
                            else
                                ViewPresentationOld(link);
                            break;
                    }
                    break;
                case FileTypes.PDF:
                    switch (ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenCopyOfFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                    {
                                        OpenCopyOfFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                    {
                                        SaveFile("Save copy of the file as", link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                    {
                                        PrintFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Email)
                                        using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
                                        {
                                            form.link = link;
                                            form.ShowDialog();
                                        }
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.Word:
                    switch (ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenCopyOfFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                    {
                                        OpenCopyOfFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                    {
                                        SaveFile("Save copy of the file as", link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                    {
                                        PrintFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Email)
                                        using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
                                        {
                                            form.link = link;
                                            form.ShowDialog();
                                        }
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.Excel:
                    switch (ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenCopyOfFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                    {
                                        OpenCopyOfFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                    {
                                        SaveFile("Save copy of the file as", link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                    {
                                        PrintFile(link);
                                    }
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Email)
                                        using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
                                        {
                                            form.link = link;
                                            form.ShowDialog();
                                        }
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.MediaPlayerVideo:
                    switch (ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenVideo(link);
                            break;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormVideoViewOptions formVideoOptions = new ToolForms.WallBin.FormVideoViewOptions())
                            {
                                formVideoOptions.Text = string.Format(formVideoOptions.Text, sourceFile.Name);
                                if (formVideoOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formVideoOptions.IsAdd)
                                    {
                                        AddVideoIntoPresentation(link);
                                    }
                                    else
                                    {
                                        OpenVideo(link);
                                    }
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.QuickTimeVideo:
                    OpenVideo(link);
                    break;
                case FileTypes.Other:
                    OpenCopyOfFile(link);
                    break;
                case FileTypes.Folder:
                    OpenFolder(link);
                    break;
                case FileTypes.Url:
                    StartProcess(link);
                    break;
                case FileTypes.Network:
                    StartProcess(link);
                    break;
                case FileTypes.OvernightsLink:
                    StartProcess(link);
                    break;
                case FileTypes.LineBreak:
                    if (!string.IsNullOrEmpty(link.LineBreakProperties.Note))
                        AppManager.Instance.ShowInfo(link.LineBreakProperties.Note);
                    break;
                default:
                    break;
            }
        }

        private void PreviewFile(LibraryFile link)
        {
            AppManager.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

            if (link.LinkAvailable)
            {
                _formLinkPreview.SelectedFile = link;
                _formLinkPreview.ShowDialog();
            }
            ConfigurationClasses.RegistryHelper.SalesDepotHandle = FormMain.Instance.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = true;
        }

        public void OpenCopyOfFile(LibraryFile link)
        {
            try
            {
                string newFile = Path.Combine(AppManager.Instance.TempFolder.FullName, @"Copy of " + Path.GetFileName(link.LocalPath));
                File.Copy(link.LocalPath, newFile, true);
                Process.Start(newFile);

                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
            }
            catch
            {
                AppManager.Instance.ShowWarning(string.Format("Could not create copy of {0} in a temp folder.", Path.GetFileName(link.LocalPath)));
            }
        }

        public void OpenCopyOfFile(FileInfo file)
        {
            try
            {
                file = file.CopyTo(Path.Combine(AppManager.Instance.TempFolder.FullName, @"Copy of " + file.Name), true);
                Process.Start(file.FullName);
            }
            catch
            {
                AppManager.Instance.ShowWarning(string.Format("Could not create copy of {0} in a temp folder.", file.Name));
            }
        }

        public void OpenFolder(LibraryFile link)
        {
            if (Directory.Exists(link.LocalPath))
            {
                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Folder", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
                Process.Start(link.LocalPath);
            }
            else
                AppManager.Instance.ShowWarning("Folder is Not Active");
        }

        public void StartProcess(LibraryFile link)
        {
            try
            {
                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
                Process.Start(link.LocalPath);
            }
            catch
            {
                AppManager.Instance.ShowWarning("This Link is not active");
            }
        }

        public void SaveFile(string dialogTitle, LibraryFile link, bool isCopy = true)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = dialogTitle;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            dialog.FileName = (isCopy ? "Copy of " : string.Empty) + Path.GetFileName(link.LocalPath);
            dialog.OverwritePrompt = true;
            dialog.Filter = (Path.GetExtension(link.LocalPath).Substring(1)).ToUpper() + " Files|*" + Path.GetExtension(link.LocalPath);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Save Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

                string newFile = Path.Combine(AppManager.Instance.TempFolder.FullName, @"Copy of " + Path.GetFileName(link.LocalPath));
                File.Copy(link.LocalPath, newFile, true);
                if (File.Exists(newFile))
                {
                    if (AppManager.Instance.ShowInfoQuestion(string.Format("The {0} file has been saved as\n{1}\nDo you want to open it?", new object[] { isCopy ? "copy of the" : string.Empty, link.RemotePath })) == DialogResult.Yes)
                        OpenCopyOfFile(new FileInfo(newFile));
                }
                else
                    AppManager.Instance.ShowWarning("File has not been saved successfully.");
            }
        }

        public void SaveFile(string dialogTitle, FileInfo file, bool isCopy = true)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = dialogTitle;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            dialog.FileName = (isCopy ? "Copy of " : string.Empty) + file.Name;
            dialog.OverwritePrompt = true;
            dialog.Filter = (file.Extension.Substring(1)).ToUpper() + " Files|*" + file.Extension;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file = file.CopyTo(dialog.FileName, true);
                if (file.Exists)
                {
                    if (AppManager.Instance.ShowInfoQuestion(string.Format("The {0} file has been saved as\n{1}\nDo you want to open it?", new object[] { isCopy ? "copy of the" : string.Empty, file.FullName })) == DialogResult.Yes)
                        OpenCopyOfFile(file);
                }
                else
                    AppManager.Instance.ShowWarning("File has not been saved successfully.");
            }
        }

        public void PrintFile(LibraryFile link)
        {
            Process printProcess = new Process();
            string tempPath = string.Empty;
            string newFile = Path.Combine(AppManager.Instance.TempFolder.FullName, @"Copy of " + Path.GetFileName(link.LocalPath));
            File.Copy(link.LocalPath, newFile, true);
            switch (Path.GetExtension(link.LocalPath).Substring(1).ToUpper())
            {
                case "PPT":
                case "PPTX":
                    break;
                case "DOC":
                case "DOCX":
                    try
                    {
                        printProcess.StartInfo.FileName = "winword.exe";
                        printProcess.StartInfo.Arguments = '"' + tempPath + '"' + " /mFilePrint";
                        printProcess.Start();
                    }
                    catch
                    {
                        AppManager.Instance.ShowWarning("AcroRd32.exe has not been found");
                    }
                    break;
                case "XLS":
                case "XLSX":
                    if (InteropClasses.ExcelHelper.Instance.Connect())
                    {
                        InteropClasses.ExcelHelper.Instance.Print(new FileInfo(tempPath));
                        InteropClasses.ExcelHelper.Instance.Disconnect();
                    }
                    break;
                case "PDF":
                    try
                    {
                        printProcess.StartInfo.FileName = "AcroRd32.exe";
                        printProcess.StartInfo.Arguments = " /p " + '"' + tempPath + '"';
                        printProcess.Start();
                    }
                    catch
                    {
                        AppManager.Instance.ShowWarning("AcroRd32.exe has not been found");
                    }
                    break;
                default:
                    AppManager.Instance.ShowWarning("Cannot print files of this type");
                    break;
            }

            AppManager.Instance.ActivityManager.AddLinkAccessActivity("Print Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
        }

        public void EmailFile(LibraryFile link)
        {
            if (InteropClasses.OutlookHelper.Instance.Open())
            {
                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Email Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
                InteropClasses.OutlookHelper.Instance.CreateMessage(string.Empty, link.LocalPath);
                InteropClasses.OutlookHelper.Instance.Close();
            }
            else
                AppManager.Instance.ShowWarning("Cannot open Outlook");
        }

        public void EmailFile(string filePath)
        {
            if (InteropClasses.OutlookHelper.Instance.Open())
            {
                InteropClasses.OutlookHelper.Instance.CreateMessage(string.Empty, filePath);
                InteropClasses.OutlookHelper.Instance.Close();
            }
            else
                AppManager.Instance.ShowWarning("Cannot open Outlook");
        }

        public void EmailFile(string[] filePaths)
        {
            if (InteropClasses.OutlookHelper.Instance.Open())
            {
                InteropClasses.OutlookHelper.Instance.CreateMessage(string.Empty, filePaths);
                InteropClasses.OutlookHelper.Instance.Close();
            }
            else
                AppManager.Instance.ShowWarning("Cannot open Outlook");
        }

        public FileInfo RequestFile(LibraryFile link)
        {
            FileInfo sourceFile = null;
            if (link.LinkAvailable)
            {
                if (ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection)
                {
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = string.Format("Downloading {0}...", link.NameWithExtension);
                        form.TopMost = true;
                        FormMain.Instance.ribbonControl.Enabled = false;
                        form.Show();
                        Application.DoEvents();
                        try
                        {
                            sourceFile = new FileInfo(link.LocalPath);
                        }
                        catch
                        {
                            sourceFile = null;
                        }
                        FormMain.Instance.UseWaitCursor = false;
                        FormMain.Instance.ribbonControl.Enabled = true;
                        form.Close();
                        Application.DoEvents();
                    }
                }
                else
                {
                    try
                    {
                        sourceFile = new FileInfo(link.LocalPath);
                    }
                    catch
                    {
                        sourceFile = null;
                    }
                }
            }
            return sourceFile;
        }

        public void ViewPresentation(LibraryFile link)
        {
            string presentationFile = link.LocalPath;
            FormMain.Instance.TopMost = true;
            if (!InteropClasses.PowerPointHelper.Instance.IsLinkedWithApplication)
                AppManager.Instance.RunPowerPointLoader();
            AppManager.Instance.ActivatePowerPoint();
            AppManager.Instance.ActivateMiniBar();
            FormMain.Instance.TopMost = false;
            FileInfo file = new FileInfo(presentationFile);
            if (file.Extension.ToLower().Equals(".pptx") && InteropClasses.PowerPointHelper.Instance.Is2003)
            {
                if (!ConfigurationClasses.RegistryHelper.Office2007CompatibilityPackInstalled)
                {
                    AppManager.Instance.ShowWarning("This File was created in a Newer Version of Microsoft Office." + Environment.NewLine + Environment.NewLine + "In the FUTURE, if you want to open this file, ASK your I.T. Manager to INSTALL the Office 2007 Compatibility Pack.");
                    return;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions == ConfigurationClasses.LinkLaunchOptions.Viewer)
                {
                    if (AppManager.Instance.ShowWarningQuestion("This file is built in a newer version of PowerPoint." + Environment.NewLine + "Do you still want to open the file?") == DialogResult.Yes)
                        OpenCopyOfFile(link);
                    return;
                }
            }
            if (file.Exists)
            {
                _formPowerPointQuickView.SelectedFile = link;
                int temp = link.PreviewContainer.SelectedIndex;

                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

                _formPowerPointQuickView.ShowDialog();
                link.PreviewContainer.SelectedIndex = temp;
            }
            ConfigurationClasses.RegistryHelper.SalesDepotHandle = FormMain.Instance.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = true;
        }

        public void ViewPresentationOld(LibraryFile link)
        {
            FormMain.Instance.TopMost = true;
            if (!InteropClasses.PowerPointHelper.Instance.IsLinkedWithApplication)
                AppManager.Instance.RunPowerPointLoader();
            AppManager.Instance.ActivatePowerPoint();
            AppManager.Instance.ActivateMiniBar();
            FormMain.Instance.TopMost = false;
            FileInfo file = new FileInfo(link.LocalPath);
            if (file.Extension.ToLower().Equals(".pptx") && InteropClasses.PowerPointHelper.Instance.Is2003)
            {
                if (!ConfigurationClasses.RegistryHelper.Office2007CompatibilityPackInstalled)
                {
                    AppManager.Instance.ShowWarning("This File was created in a Newer Version of Microsoft Office." + Environment.NewLine + Environment.NewLine + "In the FUTURE, if you want to open this file, ASK your I.T. Manager to INSTALL the Office 2007 Compatibility Pack.");
                    return;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions == ConfigurationClasses.LinkLaunchOptions.Viewer)
                {
                    if (AppManager.Instance.ShowWarningQuestion("This file is built in a newer version of PowerPoint." + Environment.NewLine + "Do you still want to open the file?") == DialogResult.Yes)
                        OpenCopyOfFile(link);
                    return;
                }
            }
            if (file.Exists)
            {
                _formPowerPointQuickViewOld.SelectedFile = link;

                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Preview Link", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

                _formPowerPointQuickViewOld.ShowDialog();
            }
            ConfigurationClasses.RegistryHelper.SalesDepotHandle = FormMain.Instance.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = true;
        }

        public void AddVideoIntoPresentation(LibraryFile link)
        {
            if (File.Exists(InteropClasses.PowerPointHelper.Instance.ActivePresentation.FullName))
            {
                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Insert video", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);

                AppManager.Instance.ActivatePowerPoint();
                AppManager.Instance.ActivateMainForm();
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Inserting the video...";
                    form.TopMost = true;
                    bool result = false;
                    Thread thread = new Thread(delegate()
                    {
                        result = InteropClasses.PowerPointHelper.Instance.InsertVideoIntoActivePresentation(link.LocalPath, 100, 100, 400, 400);
                    });
                    thread.Start();
                    form.Show();
                    while (thread.IsAlive)
                        Application.DoEvents();
                    form.Close();
                    if (result)
                        using (ToolForms.WallBin.FormVideoOutput formOutput = new ToolForms.WallBin.FormVideoOutput())
                        {
                            DialogResult formResult = formOutput.ShowDialog();
                            switch (formResult)
                            {
                                case System.Windows.Forms.DialogResult.Cancel:
                                    AppManager.Instance.ActivateMainForm();
                                    break;
                                case System.Windows.Forms.DialogResult.Abort:
                                    Application.Exit();
                                    break;
                            }
                        }
                    else
                        AppManager.Instance.ShowWarning("The video is not inserted. Couldn't copy video into the presentation folder");

                }
            }
            else
            {
                AppManager.Instance.ShowWarning("The presentation is not saved. Please, save it and try again");
            }
        }

        public void OpenVideo(LibraryFile link)
        {
            string newFile = Path.Combine(AppManager.Instance.TempFolder.FullName, Path.GetFileName(link.LocalPath));
            File.Copy(link.LocalPath, newFile, true);
            ProcessStartInfo videoPlay = new ProcessStartInfo(newFile);
            try
            {
                Process process = new Process();
                process.StartInfo = videoPlay;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();

                AppManager.Instance.ActivityManager.AddLinkAccessActivity("Open Video", link.Name, link.Type.ToString(), link.RemotePath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
            }
            catch
            {
                AppManager.Instance.ShowWarning("Couldn�t find player associated with video file");
            }
        }

        public void CompressFiles(string[] filesPaths, string compressedFilePath)
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(compressedFilePath)))
            {

                s.SetLevel(9); // 0 - store only to 9 - means best compression

                byte[] buffer = new byte[4096];

                foreach (string file in filesPaths)
                {
                    // Using GetFileName makes the result compatible with XP
                    // as the resulting path is not absolute.
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                    // Setup the entry data as required.

                    // Crc and size are handled by the library for seakable streams
                    // so no need to do them here.

                    // Could also use the last write time or similar for the file.
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(file))
                    {

                        // Using a fixed size buffer here makes no noticeable difference for output
                        // but keeps a lid on memory usage.
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }

                // Finish/Close arent needed strictly as the using statement does this automatically

                // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                // the created file would be invalid.
                s.Finish();

                // Close is important to wrap things up and unlock the file.
                s.Close();
            }
        }
    }
}
