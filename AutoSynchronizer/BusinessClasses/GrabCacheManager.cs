using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace AutoSynchronizer.BusinessClasses
{
    public class GrabCacheManager
    {
        public string GrabCacheFilePath { get; private set; }
        public List<GrabFileInfo> GrabbedFiles { get; private set; }

        public GrabCacheManager(string grabFolderPath)
        {
            this.GrabCacheFilePath = Path.Combine(grabFolderPath, "GrabCache.xml");
            this.GrabbedFiles = new List<GrabFileInfo>();

            LoadCache();
        }

        private void LoadCache()
        {
            this.GrabbedFiles.Clear();
            if (File.Exists(this.GrabCacheFilePath))
            {
                XmlDocument document = new XmlDocument();

                document.Load(this.GrabCacheFilePath);

                XmlNode node = document.SelectSingleNode(@"/GrabCache/GrabbedFiles");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Name.Equals("GrabbedFile"))
                        {
                            GrabFileInfo grabbedFile = new GrabFileInfo();
                            grabbedFile.Deserialize(childNode);
                            this.GrabbedFiles.Add(grabbedFile);
                        }
                    }
                }
            }
        }

        public void SaveCache()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<GrabCache>");
            xml.AppendLine(@"<GrabbedFiles>");
            foreach (GrabFileInfo fileInfo in this.GrabbedFiles)
                xml.AppendLine(@"<GrabbedFile>" + fileInfo.Serialize() + @"</GrabbedFile>");
            xml.AppendLine(@"</GrabbedFiles>");
            xml.AppendLine(@"</GrabCache>");

            using (StreamWriter sw = new StreamWriter(this.GrabCacheFilePath, false))
            {
                sw.Write(xml.ToString());
                sw.Flush();
            }
        }
    }

    public class GrabFileInfo
    {
        public string FilePath { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime FileDate { get; set; }

        public GrabFileInfo()
        {
            this.FilePath = string.Empty;
            this.LastModified = DateTime.MinValue;
            this.FileDate = DateTime.MinValue;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<FilePath>" + this.FilePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</FilePath>");
            result.AppendLine(@"<LastModified>" + this.LastModified.ToString() + @"</LastModified>");
            result.AppendLine(@"<FileDate>" + this.FileDate.ToString() + @"</FileDate>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDate;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FilePath":
                        this.FilePath = childNode.InnerText;
                        break;
                    case "LastModified":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.LastModified = tempDate;
                        break;
                    case "FileDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.FileDate = tempDate;
                        break;
                }
            }
        }
    }
}
