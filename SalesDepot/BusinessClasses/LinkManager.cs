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
                    return;
                }
            }
            switch (link.Type)
            {
                case FileTypes.BuggyPresentation:
                case FileTypes.FriendlyPresentation:
                case FileTypes.OtherPresentation:
                    ToolClasses.SDRecorder.Instance.WriteSDEvent(sourceFile.Name);
                    switch (ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                        OpenCopyOfFile(sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                        SaveFile("Save copy of the file as", sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                        PrintFile(sourceFile);
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
                            OpenCopyOfFile(sourceFile);
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
                    ToolClasses.SDRecorder.Instance.WriteSDEvent(sourceFile.Name);
                    switch (ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenCopyOfFile(sourceFile);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                        OpenCopyOfFile(sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                        SaveFile("Save copy of the file as", sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                        PrintFile(sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Email)
                                        using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
                                        {
                                            form.SelectedFile = link;
                                            form.ShowDialog();
                                        }
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.Word:
                    ToolClasses.SDRecorder.Instance.WriteSDEvent(sourceFile.Name);
                    switch (ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenCopyOfFile(sourceFile);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                        OpenCopyOfFile(sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                        SaveFile("Save copy of the file as", sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                        PrintFile(sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Email)
                                        using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
                                        {
                                            form.SelectedFile = link;
                                            form.ShowDialog();
                                        }
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.Excel:
                    ToolClasses.SDRecorder.Instance.WriteSDEvent(sourceFile.Name);
                    switch (ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenCopyOfFile(sourceFile);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormViewOptions formViewOptions = new ToolForms.WallBin.FormViewOptions())
                            {
                                formViewOptions.Text = string.Format(formViewOptions.Text, sourceFile.Name);
                                if (formViewOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Open)
                                        OpenCopyOfFile(sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Save)
                                        SaveFile("Save copy of the file as", sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Print)
                                        PrintFile(sourceFile);
                                    if (formViewOptions.SelectedOption == ToolForms.WallBin.ViewOptions.Email)
                                        using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
                                        {
                                            form.SelectedFile = link;
                                            form.ShowDialog();
                                        }
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.MediaPlayerVideo:
                    ToolClasses.SDRecorder.Instance.WriteSDEvent(sourceFile.Name);
                    switch (ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions)
                    {
                        case ConfigurationClasses.LinkLaunchOptions.Viewer:
                            PreviewFile(link);
                            return;
                        case ConfigurationClasses.LinkLaunchOptions.Launch:
                            OpenVideo(sourceFile);
                            break;
                        case ConfigurationClasses.LinkLaunchOptions.Menu:
                            using (ToolForms.WallBin.FormVideoViewOptions formVideoOptions = new ToolForms.WallBin.FormVideoViewOptions())
                            {
                                formVideoOptions.Text = string.Format(formVideoOptions.Text, sourceFile.Name);
                                if (formVideoOptions.ShowDialog() == DialogResult.OK)
                                {
                                    if (formVideoOptions.IsAdd)
                                        AddVideoIntoPresentation(sourceFile);
                                    else
                                        OpenVideo(sourceFile);
                                }
                            }
                            break;
                    }
                    break;
                case FileTypes.QuickTimeVideo:
                    ToolClasses.SDRecorder.Instance.WriteSDEvent(sourceFile.Name);
                    OpenVideo(sourceFile);
                    break;
                case FileTypes.Other:
                    ToolClasses.SDRecorder.Instance.WriteSDEvent(sourceFile.Name);
                    OpenCopyOfFile(sourceFile);
                    break;
                case FileTypes.Folder:
                    OpenFolder(link.LocalPath);
                    break;
                case FileTypes.Url:
                    StartProcess(link.LocalPath);
                    break;
                case FileTypes.Network:
                    StartProcess(link.LocalPath);
                    break;
                case FileTypes.OvernightsLink:
                    StartProcess(link.LocalPath);
                    break;
                case FileTypes.LineBreak:
                    if (!string.IsNullOrEmpty(link.LineBreakProperties.Note))
                        AppManager.Instance.ShowInfo(link.LineBreakProperties.Note);
                    break;
                default:
                    break;
            }
        }

        public void PreviewFile(LibraryFile selectedFile)
        {
            if (selectedFile.LinkAvailable)
            {
                _formLinkPreview.SelectedFile = selectedFile;
                _formLinkPreview.ShowDialog();
            }
            ConfigurationClasses.RegistryHelper.RemoteLibraryHandle = FormMain.Instance.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeRemoteLibrary = true;
        }

        public void OpenFile(FileInfo file)
        {
            try
            {
                Process.Start(file.FullName);
            }
            catch
            {
                AppManager.Instance.ShowWarning(string.Format("Could not open {0} ", file.Name));
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

        public void OpenFolder(string folderName)
        {
            if (Directory.Exists(folderName))
                Process.Start(folderName);
            else
                AppManager.Instance.ShowWarning("Folder is Not Active");
        }

        public void StartProcess(string processName)
        {
            try
            {
                Process.Start(processName);
            }
            catch
            {
                AppManager.Instance.ShowWarning("This Link is not active");
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

        public void PrintFile(FileInfo file)
        {
            Process printProcess = new Process();
            switch (file.Extension.Substring(1).ToUpper())
            {
                case "PPT":
                case "PPTX":
                    break;
                case "DOC":
                case "DOCX":
                    try
                    {
                        printProcess.StartInfo.FileName = "winword.exe";
                        printProcess.StartInfo.Arguments = '"' + file.FullName + '"' + " /mFilePrint";
                        printProcess.Start();
                    }
                    catch
                    {
                        AppManager.Instance.ShowWarning("AcroRd32.exe has not been found");
                    }
                    break;
                case "XLS":
                case "XLSX":
                    if (InteropClasses.ExcelHelper.Instance.Open())
                        InteropClasses.ExcelHelper.Instance.Print(file);
                    break;
                case "PDF":
                    try
                    {
                        printProcess.StartInfo.FileName = "AcroRd32.exe";
                        printProcess.StartInfo.Arguments = " /p " + '"' + file.FullName + '"';
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

        public void ViewPresentation(LibraryFile selectedFile)
        {
            string presentationFile = selectedFile.LocalPath;
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
                        OpenCopyOfFile(file);
                    return;
                }
            }
            if (file.Exists)
            {
                _formPowerPointQuickView.SelectedFile = selectedFile;
                int temp = selectedFile.PreviewContainer.SelectedIndex;
                _formPowerPointQuickView.ShowDialog();
                selectedFile.PreviewContainer.SelectedIndex = temp;
            }
            ConfigurationClasses.RegistryHelper.RemoteLibraryHandle = FormMain.Instance.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeRemoteLibrary = true;
        }

        public void ViewPresentationOld(LibraryFile selectedFile)
        {
            FormMain.Instance.TopMost = true;
            if (!InteropClasses.PowerPointHelper.Instance.IsLinkedWithApplication)
                AppManager.Instance.RunPowerPointLoader();
            AppManager.Instance.ActivatePowerPoint();
            AppManager.Instance.ActivateMiniBar();
            FormMain.Instance.TopMost = false;
            FileInfo file = new FileInfo(selectedFile.LocalPath);
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
                        OpenCopyOfFile(file);
                    return;
                }
            }
            if (file.Exists)
            {
                _formPowerPointQuickViewOld.SelectedFile = selectedFile;
                _formPowerPointQuickViewOld.ShowDialog();
            }
            ConfigurationClasses.RegistryHelper.RemoteLibraryHandle = FormMain.Instance.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeRemoteLibrary = true;
        }

        public void AddVideoIntoPresentation(FileInfo file)
        {
            if (File.Exists(InteropClasses.PowerPointHelper.Instance.ActivePresentation.FullName))
            {
                AppManager.Instance.ActivatePowerPoint();
                AppManager.Instance.ActivateMainForm();
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Inserting the video...";
                    form.TopMost = true;
                    bool result = false;
                    Thread thread = new Thread(delegate()
                    {
                        result = InteropClasses.PowerPointHelper.Instance.InsertVideoIntoActivePresentation(file, 100, 100, 400, 400);
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

        public void OpenVideo(FileInfo file)
        {
            file = file.CopyTo(Path.Combine(AppManager.Instance.TempFolder.FullName, file.Name), true);
            ProcessStartInfo videoPlay = new ProcessStartInfo(file.FullName);
            try
            {
                Process process = new Process();
                process.StartInfo = videoPlay;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
            }
            catch
            {
                AppManager.Instance.ShowWarning("Couldn’t find player associated with video file");
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
