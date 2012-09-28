using System;
using System.IO;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class VideoPreviewGenerator : SalesDepot.CoreObjects.BusinessClasses.IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public IPreviewContainer Parent { get; private set; }

        public VideoPreviewGenerator(IPreviewContainer parent)
        {
            this.Parent = parent;
        }

        public void GeneratePreview()
        {
            bool update = false;
            if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
            {
                string mp4Destination = Path.Combine(this.Parent.ContainerPath, "mp4");
                bool updateMp4 = !(Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination, "*.mp4").Length > 0);
                if (!Directory.Exists(mp4Destination))
                    Directory.CreateDirectory(mp4Destination);
                if (updateMp4)
                    FileManager.ToolClasses.VideoHelper.Instance.ExportMp4(this.Parent.OriginalPath, mp4Destination);
                if (!((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive))
                    ToolClasses.SyncManager.DeleteFolder(new DirectoryInfo(mp4Destination));
                update |= updateMp4;
            }

            if ((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive)
            {
                string ogvDestination = Path.Combine(this.Parent.ContainerPath, "ogv");
                bool updateOgv = !(Directory.Exists(ogvDestination) && Directory.GetFiles(ogvDestination, "*.ogv").Length > 0);
                if (!Directory.Exists(ogvDestination))
                    Directory.CreateDirectory(ogvDestination);
                if (updateOgv)
                    FileManager.ToolClasses.VideoHelper.Instance.ExportOgv(this.Parent.OriginalPath, ogvDestination);
                if (!((ToolClasses.Globals.ThreadActive && !ToolClasses.Globals.ThreadAborted) || !ToolClasses.Globals.ThreadActive))
                    ToolClasses.SyncManager.DeleteFolder(new DirectoryInfo(ogvDestination));
                update |= updateOgv;

            }
            if (update)
                this.Parent.LastChanged = DateTime.Now;
        }
        #endregion
    }
}
