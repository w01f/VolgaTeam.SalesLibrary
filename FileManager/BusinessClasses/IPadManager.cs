using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace FileManager.BusinessClasses
{
    public class IPadManager
    {
        public Library Parent { get; private set; }
        public string SyncDestinationPath { get; set; }
        public string Website { get; set; }

        public IPadManager(Library parent)
        {
            this.Parent = parent;
            this.SyncDestinationPath = string.Empty;
            this.Website = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<SyncDestinationPath>" + this.SyncDestinationPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SyncDestinationPath>");
            result.AppendLine(@"<Website>" + this.Website.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Website>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "SyncDestinationPath":
                        this.SyncDestinationPath = childNode.InnerText;
                        break;
                    case "Website":
                        this.Website = childNode.InnerText;
                        break;
                }
            }
        }

        public VideoInfo[] VideoFiles
        {
            get
            {
                List<LibraryFile> videoLinks = new List<LibraryFile>();
                foreach (LibraryPage page in this.Parent.Pages)
                    foreach (LibraryFolder folder in page.Folders)
                        videoLinks.AddRange(folder.Files.Where(x => x.Type == FileTypes.MediaPlayerVideo || x.Type == FileTypes.QuickTimeVideo));
                videoLinks.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(Path.GetFileName(x.FullPath), Path.GetFileName(y.FullPath)));

                List<VideoInfo> videoFiles = new List<VideoInfo>();
                int i = 1;
                foreach (LibraryFile videoLink in videoLinks)
                {
                    VideoInfo videoFile = new VideoInfo(videoLink);
                    videoFile.Index = i.ToString();
                    videoFile.SourceFileName = Path.GetFileName(videoLink.FullPath);
                    videoFile.SourceFilePath = videoLink.FullPath;
                    if (videoLink.UniversalPreviewContainer != null)
                    {
                        if (Directory.Exists(videoLink.UniversalPreviewContainer.ContainerPath))
                            videoFile.IPadFolderPath = videoLink.UniversalPreviewContainer.ContainerPath;
                        else
                            videoFile.IPadFolderPath = null;

                        string mp4Path = Path.Combine(videoLink.UniversalPreviewContainer.ContainerPath, "mp4", Path.GetFileName(Path.ChangeExtension(videoLink.FullPath, ".mp4")));
                        if (File.Exists(mp4Path))
                        {
                            videoFile.Mp4FileName = Path.GetFileName(mp4Path);
                            videoFile.Mp4FilePath = mp4Path;
                            videoFile.IPadCompatible = "YES!";
                        }
                        else
                        {
                            videoFile.Mp4FileName = null;
                            videoFile.Mp4FilePath = null;
                            videoFile.IPadCompatible = "NO!";
                        }
                        string ogvPath = Path.Combine(videoLink.UniversalPreviewContainer.ContainerPath, "ogv", Path.GetFileName(Path.ChangeExtension(videoLink.FullPath, ".ogv")));
                        if (File.Exists(ogvPath))
                        {
                            videoFile.OgvFileName = Path.GetFileName(ogvPath);
                            videoFile.OgvFilePath = ogvPath;
                        }
                        else
                        {
                            videoFile.OgvFileName = null;
                            videoFile.OgvFilePath = null;
                        }
                    }
                    else
                        videoFile.IPadCompatible = "NO!";
                    videoFiles.Add(videoFile);
                    i++;
                }
                return videoFiles.ToArray();
            }
        }
    }

    public class VideoInfo
    {
        public LibraryFile Parent { get; private set; }
        public string Index { get; set; }
        public string SourceFileName { get; set; }
        public string SourceFilePath { get; set; }
        public string IPadFolderPath { get; set; }
        public string Mp4FileName { get; set; }
        public string Mp4FilePath { get; set; }
        public string IPadCompatible { get; set; }
        public string OgvFileName { get; set; }
        public string OgvFilePath { get; set; }

        public VideoInfo(LibraryFile parent)
        {
            this.Parent = parent;
        }
    }
}
