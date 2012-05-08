using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace SalesDepot.InteropClasses
{
    public enum OfficeComponent
    {
        Word,
        Excel,
        PowerPoint,
        Outlook,
        None,
    }

    static class OfficeHelper
    {
        /// <summary>
        /// gets the component's path from the registry. if it can't find it - retuns 
        ///an empty string
        /// </summary>
        public static string GetComponentPath(OfficeComponent component)
        {
            const string RegKey = @"Software\Microsoft\Windows\CurrentVersion\App Paths";
            string toReturn = string.Empty;
            string key = string.Empty;

            switch (component)
            {
                case OfficeComponent.Word:
                    key = "winword.exe";
                    break;
                case OfficeComponent.Excel:
                    key = "excel.exe";
                    break;
                case OfficeComponent.PowerPoint:
                    key = "powerpnt.exe";
                    break;
                case OfficeComponent.Outlook:
                    key = "outlook.exe";
                    break;
            }

            //looks inside CURRENT_USER:
            RegistryKey mainKey = Registry.CurrentUser;
            try
            {
                mainKey = mainKey.OpenSubKey(Path.Combine(RegKey,key), false);
                if (mainKey != null)
                {
                    toReturn = mainKey.GetValue(string.Empty).ToString();
                }
            }
            catch
            { }

            //if not found, looks inside LOCAL_MACHINE:
            mainKey = Registry.LocalMachine;
            if (string.IsNullOrEmpty(toReturn))
            {
                try
                {
                    mainKey = mainKey.OpenSubKey(Path.Combine(RegKey,key), false);
                    if (mainKey != null)
                    {
                        toReturn = mainKey.GetValue(string.Empty).ToString();
                    }
                }
                catch
                { }
            }

            //closing the handle:
            if (mainKey != null)
                mainKey.Close();

            return toReturn;
        }

        /// <summary>
        /// Gets the major version of the path. if file not found (or any other        
        /// exception occures - returns 0
        /// </summary>
        private static int GetMajorVersion(string path)
        {
            int toReturn = 0;
            if (File.Exists(path))
            {
                try
                {
                    FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(path);
                    toReturn = fileVersion.FileMajorPart;
                }
                catch
                { }
            }
            return toReturn;
        }

        public static bool IsOfficeComponentAllowedPDFConversion(OfficeComponent component)
        {
            if (GetMajorVersion(GetComponentPath(component)) > 11)
                return true;
            else
                return false;
        }

        public static OfficeComponent GetOfficeComponent(string fileExtension)
        {
            switch (fileExtension.ToLower())
            { 
                case ".doc":
                case ".docx":
                    return OfficeComponent.Word;
                case ".xls":
                case ".xlsx":
                    return OfficeComponent.Excel;
                case ".ppt":
                case ".pptx":
                    return OfficeComponent.PowerPoint;
                default:
                    return OfficeComponent.None;
            }
        }
    }
}
