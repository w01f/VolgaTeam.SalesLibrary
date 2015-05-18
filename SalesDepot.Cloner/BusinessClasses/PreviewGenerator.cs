using System;
using System.IO;
using SalesDepot.Cloner.ToolClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class VideoPreviewGenerator : IPreviewGenerator
	{
		public VideoPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
		}

		#region IPreviewGenerator Members
		public IPreviewContainer Parent { get; private set; }

		public void GeneratePreview(bool generateImages, bool generateText)
		{
			if (generateText)
			{
				var update = false;
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					string mp4Destination = Path.Combine(Parent.ContainerPath, "mp4");
					bool updateMp4 = !(Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination, "*.mp4").Length > 0);
					if (!Directory.Exists(mp4Destination))
						Directory.CreateDirectory(mp4Destination);
					if (updateMp4)
						VideoHelper.Instance.ExportMp4(Parent.OriginalPath, mp4Destination);
					update |= updateMp4;
				}

				if (update)
					Parent.LastChanged = DateTime.Now;
			}
			if (generateImages)
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var sourceFile = !Parent.Extension.ToUpper().Equals(".MP4") ?
					Path.Combine(Parent.ContainerPath, "mp4", Path.ChangeExtension(Path.GetFileName(Parent.OriginalPath), ".mp4")) :
					Parent.OriginalPath;
					var thumbDestination = Path.Combine(Parent.ContainerPath, "thumb");
					var update = !(Directory.Exists(thumbDestination) && Directory.GetFiles(thumbDestination, "*.png").Length > 0);
					if (!Directory.Exists(thumbDestination))
						Directory.CreateDirectory(thumbDestination);
					if (update)
						VideoHelper.Instance.GenerateThumbnails(sourceFile, thumbDestination);
					if (update)
						Parent.LastChanged = DateTime.Now;
				}
			}
		}
		#endregion
	}
}