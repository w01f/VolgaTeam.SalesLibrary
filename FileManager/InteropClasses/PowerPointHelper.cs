using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace FileManager.InteropClasses
{
    class PowerPointHelper
    {
        private static PowerPointHelper instance = new PowerPointHelper();

        private PowerPoint.Application _powerPointObject;

        private PowerPointHelper()
        {
        }

        public static PowerPointHelper Instance
        {
            get
            {
                return instance;
            }
        }

        public bool Connect()
        {
            bool result = false;
            MessageFilter.Register();
            try
            {
                _powerPointObject = new Microsoft.Office.Interop.PowerPoint.Application();
                _powerPointObject.DisplayAlerts = PowerPoint.PpAlertLevel.ppAlertsNone;
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

        public void Disconnect()
        {
            AppManager.Instance.ReleaseComObject(_powerPointObject);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public bool ExportPresentationAsImages(string sourceFilePath, string destinationFolderPath)
        {
            bool result = false;
            try
            {
                MessageFilter.Register();
                if (Connect())
                {
                    PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: sourceFilePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                    presentation.Export(Path: destinationFolderPath, FilterName: "PNG");
                    presentation.Close();
                    AppManager.Instance.ReleaseComObject(presentation);
                    Disconnect();
                    result = true;
                }
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

        public void GetPresentationProperties(BusinessClasses.LibraryFile file)
        {
            try
            {
                MessageFilter.Register();
                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: file.FullPath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                if (file.PresentationProperties == null)
                    file.PresentationProperties = new BusinessClasses.PresentationProperties();
                file.PresentationProperties.Height = presentation.PageSetup.SlideHeight / 72;
                file.PresentationProperties.Width = presentation.PageSetup.SlideWidth / 72;
                file.PresentationProperties.LastUpdate = DateTime.Now;
                presentation.Close();
                AppManager.Instance.ReleaseComObject(presentation);
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
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
