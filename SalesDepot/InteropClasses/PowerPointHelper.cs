using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace SalesDepot.InteropClasses
{
    public class PowerPointHelper
    {
        private static PowerPointHelper instance = new PowerPointHelper();

        private PowerPoint.Application _powerPointObject = null;
        private int _powerPointProcessId = 0;
        private PowerPoint.Presentation _activePresentation;
        private PowerPoint.Presentation _slideSourcePresentationObject;
        private PowerPoint.SlideShowWindow _currentSlideShowWindow;

        private bool _isOpened;

        public bool Is2003 { get; private set; }

        private PowerPointHelper()
        {
            GetVersion();
        }

        public static PowerPointHelper Instance
        {
            get
            {
                return instance;
            }
        }

        public PowerPoint.Application PowerPointObject
        {
            get
            {
                if (_powerPointObject == null)
                    Connect();
                return _powerPointObject;
            }
        }

        public PowerPoint.Presentation ActivePresentation
        {
            get
            {
                GetActivePresentation();
                return _activePresentation;
            }
        }

        public PowerPoint.Presentation SlideSourcePresentation
        {
            get
            {
                return _slideSourcePresentationObject;
            }
        }

        public PowerPoint.SlideShowWindow SlideShowWindow
        {
            get
            {
                return _currentSlideShowWindow;
            }
        }

        public bool IsLinkedWithApplication
        {
            get
            {
                Process[] proc = Process.GetProcessesByName("POWERPNT");
                if (!(proc.GetLength(0) > 0))
                {
                    _powerPointObject = null;
                    _isOpened = false;
                }
                else
                {
                    try
                    {
                        string caption = _powerPointObject.Caption;
                        _isOpened = true;
                    }
                    catch
                    {
                        _isOpened = false;
                    }
                }
                return _isOpened;
            }
        }

        public bool PowerPointVisible
        {
            set
            {
                try
                {
                    this.PowerPointObject.Visible = value ? Microsoft.Office.Core.MsoTriState.msoTrue : Microsoft.Office.Core.MsoTriState.msoFalse;
                }
                catch
                {
                }
            }
        }

        private void GetVersion()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application appVersion = new Microsoft.Office.Interop.Word.Application();
                appVersion.Visible = false;
                this.Is2003 = appVersion.Version.Equals("11.0");
                (appVersion as Microsoft.Office.Interop.Word._Application).Quit();
                appVersion = null;
            }
            catch
            {
            }
        }

        public bool Connect(bool background = false)
        {
            bool result = false;
            try
            {
                MessageFilter.Register();
                if (background)
                {
                    _powerPointObject = new Microsoft.Office.Interop.PowerPoint.Application();
                }
                else
                {
                    try
                    {
                        _powerPointObject =
                            System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application") as PowerPoint.Application;
                    }
                    catch
                    {
                    }
                }
                uint lpdwProcessId = 0;
                WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_powerPointObject.HWND), out lpdwProcessId);
                _powerPointProcessId = (int)lpdwProcessId;

                _powerPointObject.DisplayAlerts = Microsoft.Office.Interop.PowerPoint.PpAlertLevel.ppAlertsNone;

                ConfigurationClasses.SettingsManager.Instance.EnablePdfConverting = !this.Is2003;
                _isOpened = true;
                result = true;
            }
            catch
            {
                _powerPointObject = null;
                result = false;
            }
            finally
            {
                MessageFilter.Revoke();
            }
            return result;
        }

        public void Disconnect()
        {
            AppManager.Instance.ReleaseComObject(_powerPointObject);
            GC.Collect();
            _powerPointObject = null;
        }

        public void GetActivePresentation()
        {
            try
            {
                _activePresentation = this.PowerPointObject.ActivePresentation;
            }
            catch
            {
                try
                {
                    _activePresentation = this.PowerPointObject.Presentations[1];
                }
                catch
                {
                    _activePresentation = null;
                }
            }
        }

        public bool ConvertToPDF(string originalFileName, string pdfFileName)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(originalFileName))
                {

                    if (this.PowerPointObject != null)
                    {
                        MessageFilter.Register();
                        PowerPoint.Presentation presentationObject = this.PowerPointObject.Presentations.Open(originalFileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
                        presentationObject.SaveAs(pdfFileName, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF, Microsoft.Office.Core.MsoTriState.msoCTrue);
                    }
                }
                else
                    _slideSourcePresentationObject.SaveAs(pdfFileName, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF, Microsoft.Office.Core.MsoTriState.msoCTrue);
                result = true;
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
            return result;
        }

        private void CloseSlideSourcePresentation()
        {
            try
            {
                MessageFilter.Register();
                _slideSourcePresentationObject.Close();
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void OpenSlideSourcePresentation(FileInfo presentationFile)
        {

            MessageFilter.Register();
            try
            {
                CloseSlideSourcePresentation();
                _slideSourcePresentationObject = this.PowerPointObject.Presentations.Open(FileName: presentationFile.FullName, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
            }
            catch
            {
                _slideSourcePresentationObject = null;
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void ViewSlideShow()
        {

            MessageFilter.Register();
            try
            {
                _slideSourcePresentationObject.SlideShowSettings.ShowType = Microsoft.Office.Interop.PowerPoint.PpSlideShowType.ppShowTypeKiosk;
                _slideSourcePresentationObject.SlideShowSettings.ShowWithAnimation = Microsoft.Office.Core.MsoTriState.msoFalse;
                _slideSourcePresentationObject.SlideShowSettings.LoopUntilStopped = Microsoft.Office.Core.MsoTriState.msoTrue;
                _currentSlideShowWindow = _slideSourcePresentationObject.SlideShowSettings.Run();

            }
            catch
            {
                _currentSlideShowWindow = null;
                _slideSourcePresentationObject = null;
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void ResizeSlideShow(IntPtr parentHandle, int parentHeight, int parentWidth)
        {
            if ((_currentSlideShowWindow != null) && !parentHandle.Equals(IntPtr.Zero) && (parentHeight != 0) && (parentWidth != 0))
            {
                IntPtr currentSlideShowHandle = new IntPtr(_currentSlideShowWindow.HWND);
                if (currentSlideShowHandle != IntPtr.Zero)
                {
                    WinAPIHelper.ShowWindowAsync(currentSlideShowHandle, 0);
                    WinAPIHelper.SetParent(currentSlideShowHandle, IntPtr.Zero);
                    _currentSlideShowWindow.Height = parentHeight;
                    _currentSlideShowWindow.Width = parentWidth;
                    WinAPIHelper.ShowWindowAsync(currentSlideShowHandle, WindowShowStyle.ShowNormal);
                    WinAPIHelper.SetParent(currentSlideShowHandle, parentHandle);
                }
            }
        }

        public void ExitSlideShow()
        {

            if (_currentSlideShowWindow != null)
            {
                try
                {
                    MessageFilter.Register();
                    _currentSlideShowWindow.View.Exit();
                }
                catch
                {
                }
                finally
                {
                    MessageFilter.Revoke();
                    _currentSlideShowWindow = null;
                }
            }
        }

        public void ChangeAspect()
        {
            if (this.ActivePresentation.PageSetup.SlideOrientation == Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal)
                this.ActivePresentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationVertical;
            else if (this.ActivePresentation.PageSetup.SlideOrientation == Microsoft.Office.Core.MsoOrientation.msoOrientationVertical)
                this.ActivePresentation.PageSetup.SlideOrientation = Microsoft.Office.Core.MsoOrientation.msoOrientationHorizontal;
        }

        public PowerPoint.Slide GetActiveSlide()
        {
            if (this.PowerPointObject.Windows.Count > 0)
            {
                this.PowerPointObject.Activate();
                if (this.PowerPointObject.ActiveWindow != null)
                {
                    this.PowerPointObject.ActiveWindow.ViewType = Microsoft.Office.Interop.PowerPoint.PpViewType.ppViewNormal;
                    return (PowerPoint.Slide)this.PowerPointObject.ActiveWindow.View.Slide;
                }
            }
            return null;
        }

        public int GetActiveSlideIndex()
        {
            int slideIndex = -1;
            try
            {
                if (this.PowerPointObject.Windows.Count > 0)
                {
                    this.PowerPointObject.Activate();
                    if (this.PowerPointObject.ActiveWindow != null)
                    {
                        slideIndex = ((PowerPoint.Slide)this.PowerPointObject.ActiveWindow.View.Slide).SlideIndex;
                    }
                }
            }
            catch
            {
            }
            if (this.Is2003 && slideIndex == -1)
            {
                RefreshContents();
                this.PowerPointObject.ActiveWindow.Selection.Unselect();
            }
            return slideIndex;
        }

        public void AppendSlide(int slideIndex, string templatePath = "")
        {
            PowerPoint.Slide slide;
            PowerPoint.SlideRange pastedRange;
            PowerPoint.Design design;
            int indexToPaste;
            int currentSlideIndex = 0;
            Microsoft.Office.Core.MsoTriState masterShape;

            try
            {
                MessageFilter.Register();
                indexToPaste = GetActiveSlideIndex() + 1;

                if (!string.IsNullOrEmpty(templatePath))
                    _slideSourcePresentationObject.ApplyTemplate(templatePath);
                for (int i = 1; i <= _slideSourcePresentationObject.Slides.Count; i++)
                {
                    if ((i == slideIndex) || (slideIndex == -1))
                    {
                        if (indexToPaste > 1)
                        {
                            slide = _slideSourcePresentationObject.Slides[i];
                            slide.Copy();
                            pastedRange = this.ActivePresentation.Slides.Paste(indexToPaste);
                            indexToPaste++;
                            design = GetDesignFromSlide(slide, this.ActivePresentation);
                            if (design != null)
                                pastedRange.Design = design;
                            else
                                pastedRange.Design = slide.Design;
                            pastedRange.ColorScheme = slide.ColorScheme;
                            if (slide.FollowMasterBackground == Microsoft.Office.Core.MsoTriState.msoFalse)
                            {
                                pastedRange.FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
                                pastedRange.Background.Fill.Visible = slide.Background.Fill.Visible;
                                pastedRange.Background.Fill.ForeColor = slide.Background.Fill.ForeColor;
                                pastedRange.Background.Fill.BackColor = slide.Background.Fill.BackColor;

                                switch (slide.Background.Fill.Type)
                                {
                                    case Microsoft.Office.Core.MsoFillType.msoFillTextured:
                                        switch (slide.Background.Fill.TextureType)
                                        {
                                            case Microsoft.Office.Core.MsoTextureType.msoTexturePreset:
                                                pastedRange.Background.Fill.PresetTextured(slide.Background.Fill.PresetTexture);
                                                break;
                                        }
                                        break;
                                    case Microsoft.Office.Core.MsoFillType.msoFillSolid:
                                        pastedRange.Background.Fill.Transparency = 0;
                                        pastedRange.Background.Fill.Solid();
                                        break;
                                    case Microsoft.Office.Core.MsoFillType.msoFillPicture:
                                        if (slide.Shapes.Count > 0)
                                            (slide.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                        masterShape = slide.DisplayMasterShapes;
                                        slide.DisplayMasterShapes = Microsoft.Office.Core.MsoTriState.msoFalse;
                                        slide.Export(Path.Combine(AppManager.Instance.TempFolder.FullName, slide.SlideID + ".png"), "PNG", -1, -1);
                                        pastedRange.Background.Fill.UserPicture(Path.Combine(AppManager.Instance.TempFolder.FullName, slide.SlideID + ".png"));
                                        FileInfo file = new FileInfo(Path.Combine(AppManager.Instance.TempFolder.FullName, slide.SlideID + ".png"));
                                        if (file.Exists)
                                            file.Delete();
                                        slide.DisplayMasterShapes = masterShape;
                                        if (slide.Shapes.Count > 0)
                                            (slide.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                        break;
                                    case Microsoft.Office.Core.MsoFillType.msoFillPatterned:
                                        pastedRange.Background.Fill.Patterned(slide.Background.Fill.Pattern);
                                        break;
                                    case Microsoft.Office.Core.MsoFillType.msoFillGradient:
                                        switch (slide.Background.Fill.GradientColorType)
                                        {
                                            case Microsoft.Office.Core.MsoGradientColorType.msoGradientTwoColors:
                                                pastedRange.Background.Fill.TwoColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant);
                                                break;
                                            case Microsoft.Office.Core.MsoGradientColorType.msoGradientPresetColors:
                                                pastedRange.Background.Fill.PresetGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.PresetGradientType);
                                                break;
                                            case Microsoft.Office.Core.MsoGradientColorType.msoGradientOneColor:
                                                pastedRange.Background.Fill.OneColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.GradientDegree);
                                                break;
                                        }
                                        break;
                                }
                            }
                            MakeDesignUnique(slide, pastedRange.Design);
                            this.ActivePresentation.Slides[indexToPaste - 1].Select();
                            currentSlideIndex = indexToPaste - 1;
                        }
                    }
                }
                if (this.Is2003 && currentSlideIndex != 0)
                {
                    RefreshContents();
                    this.ActivePresentation.Slides[currentSlideIndex].Select();
                }
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        private PowerPoint.Design GetDesignFromSlide(PowerPoint.Slide slide, PowerPoint.Presentation presentation)
        {
            foreach (PowerPoint.Design design in presentation.Designs)
                if (design.Name == slide.Design.Name)
                    return design;
            return null;
        }

        private void RefreshContents()
        {
            bool hasContents = false;
            int contentSlideIndex = 0;
            int contentSlideIndexCount = 0;
            int i = 1;
            foreach (PowerPoint.Slide slide in this.ActivePresentation.Slides)
            {
                foreach (PowerPoint.Shape shape in slide.Shapes)
                    if (shape.Tags["CONTENT_HEADER"] != "")
                    {
                        hasContents = true;
                        contentSlideIndex = i;
                        break;
                    }
                    else if (contentSlideIndexCount > 0)
                        break;
                if (hasContents)
                {
                    contentSlideIndexCount++;
                    hasContents = false;
                }
                i++;
            }
            if (contentSlideIndexCount > 0 && File.Exists(Path.Combine(ConfigurationClasses.SettingsManager.Instance.ContentsSlidePath, ConfigurationClasses.SettingsManager.Instance.DefaultWizard, ConfigurationClasses.SettingsManager.ContentsSlideName)))
            {
                PowerPoint.Design design = this.ActivePresentation.Slides[contentSlideIndex].Design;
                for (int j = 0; j < contentSlideIndexCount; j++)
                    this.ActivePresentation.Slides[contentSlideIndex].Delete();
                for (int j = 0; j < contentSlideIndexCount; j++)
                    this.ActivePresentation.Slides.InsertFromFile(Path.Combine(ConfigurationClasses.SettingsManager.Instance.ContentsSlidePath, ConfigurationClasses.SettingsManager.Instance.DefaultWizard, ConfigurationClasses.SettingsManager.ContentsSlideName), contentSlideIndex - 1 + j, 1, 1);

                for (int j = 0; j < contentSlideIndexCount; j++)
                    this.ActivePresentation.Slides[contentSlideIndex + j].Design = design;
            }
        }

        private void MakeDesignUnique(PowerPoint.Slide slide, PowerPoint.Design design)
        {
            while (!(design.SlideMaster.Shapes.Count <= slide.Design.SlideMaster.Shapes.Count))
            {
                if (design.SlideMaster.Shapes.Count > 0)
                    design.SlideMaster.Shapes[design.SlideMaster.Shapes.Count].Delete();
                else
                    break;
            }
        }

        public bool InsertVideoIntoActivePresentation(string videoFile, float left, float top, float width, float height)
        {
            bool result = false;
            try
            {
                MessageFilter.Register();
                FileInfo presentationFile = new FileInfo(this.ActivePresentation.FullName);
                string newFile = Path.Combine(presentationFile.DirectoryName, Path.GetFileName(videoFile));
                File.Copy(videoFile, newFile, true);
                PowerPoint.Slide slide = GetActiveSlide();
                if (slide != null)
                {
                    PowerPoint.Shape shape = slide.Shapes.AddMediaObject(newFile, left, top, width, height);
                    shape.AnimationSettings.PlaySettings.PlayOnEntry = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.AnimationSettings.AdvanceTime = 0;
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                MessageFilter.Revoke();
            }
            return result;
        }

        public void PrintPresentation(int currentSlideIndex)
        {
            try
            {
                MessageFilter.Register();

                int fromPage = 1;
                int toPage = 1;

                System.Windows.Forms.PrintDialog dlg = new System.Windows.Forms.PrintDialog();
                dlg.AllowCurrentPage = true;
                dlg.AllowPrintToFile = false;
                dlg.AllowSelection = false;
                dlg.AllowSomePages = true;
                dlg.ShowNetwork = true;
                dlg.UseEXDialog = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    switch (dlg.PrinterSettings.PrintRange)
                    {
                        case System.Drawing.Printing.PrintRange.AllPages:
                            fromPage = 1;
                            toPage = _slideSourcePresentationObject.Slides.Count;
                            break;
                        case System.Drawing.Printing.PrintRange.CurrentPage:
                            fromPage = currentSlideIndex;
                            toPage = currentSlideIndex;
                            break;
                        case System.Drawing.Printing.PrintRange.SomePages:
                            fromPage = dlg.PrinterSettings.FromPage;
                            if (fromPage < 1)
                                fromPage = 1;
                            toPage = currentSlideIndex;
                            if (toPage > _slideSourcePresentationObject.Slides.Count)
                                toPage = _slideSourcePresentationObject.Slides.Count;
                            break;
                    }
                    _slideSourcePresentationObject.PrintOptions.ActivePrinter = dlg.PrinterSettings.PrinterName;
                    _slideSourcePresentationObject.PrintOptions.NumberOfCopies = dlg.PrinterSettings.Copies;
                    if (dlg.PrinterSettings.Collate)
                        _slideSourcePresentationObject.PrintOut(fromPage, toPage, string.Empty, dlg.PrinterSettings.Copies, Microsoft.Office.Core.MsoTriState.msoTrue);
                    else
                        _slideSourcePresentationObject.PrintOut(fromPage, toPage, string.Empty, dlg.PrinterSettings.Copies, Microsoft.Office.Core.MsoTriState.msoFalse);
                }
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void ExportPresentationAsPDF(int slideIndex, string destinationFileName)
        {
            try
            {
                MessageFilter.Register();
                if (slideIndex == -1)
                {
                    _slideSourcePresentationObject.SaveAs(FileName: destinationFileName, FileFormat: Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF);
                }
                else
                {
                    string presentationName = Path.GetTempFileName();
                    SaveSingleSlide(slideIndex, presentationName);
                    PowerPoint.Presentation singleSlidePresentation = this.PowerPointObject.Presentations.Open(FileName: presentationName, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                    singleSlidePresentation.SaveAs(FileName: destinationFileName, FileFormat: Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF);
                    singleSlidePresentation.Close();
                }
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void SaveSingleSlide(int slideIndex, string destinationFileName)
        {
            PowerPoint.Presentation singleSlidePresentation = this.PowerPointObject.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
            singleSlidePresentation.PageSetup.SlideOrientation = _slideSourcePresentationObject.PageSetup.SlideOrientation;
            PowerPoint.Slide singleSlide = _slideSourcePresentationObject.Slides[slideIndex];
            singleSlide.Copy();
            PowerPoint.SlideRange pastedRange = singleSlidePresentation.Slides.Paste();
            PowerPoint.Design design = GetDesignFromSlide(singleSlide, singleSlidePresentation);
            if (design != null)
                pastedRange.Design = design;
            else
                pastedRange.Design = singleSlide.Design;
            pastedRange.ColorScheme = singleSlide.ColorScheme;
            if (singleSlide.FollowMasterBackground == Microsoft.Office.Core.MsoTriState.msoFalse)
            {
                pastedRange.FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
                pastedRange.Background.Fill.Visible = singleSlide.Background.Fill.Visible;
                pastedRange.Background.Fill.ForeColor = singleSlide.Background.Fill.ForeColor;
                pastedRange.Background.Fill.BackColor = singleSlide.Background.Fill.BackColor;

                switch (singleSlide.Background.Fill.Type)
                {
                    case Microsoft.Office.Core.MsoFillType.msoFillTextured:
                        switch (singleSlide.Background.Fill.TextureType)
                        {
                            case Microsoft.Office.Core.MsoTextureType.msoTexturePreset:
                                pastedRange.Background.Fill.PresetTextured(singleSlide.Background.Fill.PresetTexture);
                                break;
                        }
                        break;
                    case Microsoft.Office.Core.MsoFillType.msoFillSolid:
                        pastedRange.Background.Fill.Transparency = 0;
                        pastedRange.Background.Fill.Solid();
                        break;
                    case Microsoft.Office.Core.MsoFillType.msoFillPicture:
                        if (singleSlide.Shapes.Count > 0)
                            (singleSlide.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        Microsoft.Office.Core.MsoTriState masterShape = singleSlide.DisplayMasterShapes;
                        singleSlide.DisplayMasterShapes = Microsoft.Office.Core.MsoTriState.msoFalse;
                        singleSlide.Export(Path.GetTempPath() + @"\" + singleSlide.SlideID + ".png", "PNG", -1, -1);
                        pastedRange.Background.Fill.UserPicture(Path.GetTempPath() + @"\" + singleSlide.SlideID + ".png");
                        FileInfo file = new FileInfo(Path.GetTempPath() + @"\" + singleSlide.SlideID + ".png");
                        if (file.Exists)
                            file.Delete();
                        singleSlide.DisplayMasterShapes = masterShape;
                        if (singleSlide.Shapes.Count > 0)
                            (singleSlide.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        break;
                    case Microsoft.Office.Core.MsoFillType.msoFillPatterned:
                        pastedRange.Background.Fill.Patterned(singleSlide.Background.Fill.Pattern);
                        break;
                    case Microsoft.Office.Core.MsoFillType.msoFillGradient:
                        switch (singleSlide.Background.Fill.GradientColorType)
                        {
                            case Microsoft.Office.Core.MsoGradientColorType.msoGradientTwoColors:
                                pastedRange.Background.Fill.TwoColorGradient(singleSlide.Background.Fill.GradientStyle, singleSlide.Background.Fill.GradientVariant);
                                break;
                            case Microsoft.Office.Core.MsoGradientColorType.msoGradientPresetColors:
                                pastedRange.Background.Fill.PresetGradient(singleSlide.Background.Fill.GradientStyle, singleSlide.Background.Fill.GradientVariant, singleSlide.Background.Fill.PresetGradientType);
                                break;
                            case Microsoft.Office.Core.MsoGradientColorType.msoGradientOneColor:
                                pastedRange.Background.Fill.OneColorGradient(singleSlide.Background.Fill.GradientStyle, singleSlide.Background.Fill.GradientVariant, singleSlide.Background.Fill.GradientDegree);
                                break;
                        }
                        break;
                }
            }
            MakeDesignUnique(singleSlide, pastedRange.Design);

            singleSlidePresentation.SaveAs(FileName: destinationFileName);
            singleSlidePresentation.Close();
        }
    }

    public class MessageFilter : IOleMessageFilter
    {
        //
        // Class containing the IOleMessageFilter
        // thread error-handling functions.

        // Start the filter.
        public static void Register()
        {
            IOleMessageFilter newFilter = new MessageFilter();
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(newFilter, out oldFilter);
        }

        // Done with the filter, close it.
        public static void Revoke()
        {
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(null, out oldFilter);
        }

        //
        // IOleMessageFilter functions.
        // Handle incoming thread requests.
        int IOleMessageFilter.HandleInComingCall(int dwCallType,
          System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr
          lpInterfaceInfo)
        {
            //Return the flag SERVERCALL_ISHANDLED.
            return 0;
        }

        // Thread call was rejected, so try again.
        int IOleMessageFilter.RetryRejectedCall(System.IntPtr
          hTaskCallee, int dwTickCount, int dwRejectType)
        {
            if (dwRejectType == 2)
            // flag = SERVERCALL_RETRYLATER.
            {
                // Retry the thread call immediately if return >=0 & 
                // <100.
                return 99;
            }
            // Too busy; cancel call.
            return -1;
        }

        int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee,
          int dwTickCount, int dwPendingType)
        {
            //Return the flag PENDINGMSG_WAITDEFPROCESS.
            return 2;
        }

        // Implement the IOleMessageFilter interface.
        [DllImport("Ole32.dll")]
        private static extern int
          CoRegisterMessageFilter(IOleMessageFilter newFilter, out 
          IOleMessageFilter oldFilter);
    }

    [ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    interface IOleMessageFilter
    {
        [PreserveSig]
        int HandleInComingCall(
            int dwCallType,
            IntPtr hTaskCaller,
            int dwTickCount,
            IntPtr lpInterfaceInfo);

        [PreserveSig]
        int RetryRejectedCall(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwRejectType);

        [PreserveSig]
        int MessagePending(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwPendingType);
    }
}
