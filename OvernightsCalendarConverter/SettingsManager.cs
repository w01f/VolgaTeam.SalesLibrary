using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace OvernightsCalendarConverter
{
    public class SettingsManager
    {
        private static SettingsManager _instance = new SettingsManager();

        #region Path Section
        public string ApplicationRootsPath { get; private set; }
        private string _applicationSettingsFile = string.Empty;
        #endregion

        #region Local Settings
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        #endregion


        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private SettingsManager()
        {
            #region Path Section
            this.ApplicationRootsPath = Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location);
            _applicationSettingsFile = Path.Combine(this.ApplicationRootsPath, "LocalSettings.xml");
            #endregion

            LoadApplicationSettings();
        }

        private void LoadApplicationSettings()
        {
            string xmlFilePath = _applicationSettingsFile;
            if (File.Exists(xmlFilePath))
            {
                XmlDocument document = new XmlDocument();

                document.Load(xmlFilePath);

                XmlNode node = document.SelectSingleNode(@"/LocalSettings/SourcePath");
                if (node != null)
                {
                    this.SourcePath = node.InnerText;
                }

                node = document.SelectSingleNode(@"/LocalSettings/DestinationPath");
                if (node != null)
                {
                    this.DestinationPath = node.InnerText;
                }
            }
        }

        public void SaveApplicationSettings()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<LocalSettings>");
            if (!string.IsNullOrEmpty(this.SourcePath))
                xml.AppendLine(@"<SourcePath>" + this.SourcePath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SourcePath>");
            if (!string.IsNullOrEmpty(this.DestinationPath))
                xml.AppendLine(@"<DestinationPath>" + this.DestinationPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DestinationPath>");
            xml.AppendLine(@"</LocalSettings>");

            using (StreamWriter sw = new StreamWriter(_applicationSettingsFile, false))
            {
                sw.Write(xml.ToString());
                sw.Flush();
            }
        }
    }
}