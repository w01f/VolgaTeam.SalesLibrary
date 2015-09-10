using System;
using System.IO;
using FileManager.ToolClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class VideoPreviewGenerator : IPreviewGenerator
	{
		private readonly VideoPreviewContainer _videoPreviewContainer;

		public VideoPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
			_videoPreviewContainer = (VideoPreviewContainer)Parent;
		}

		#region IPreviewGenerator Members
		public IPreviewContainer Parent { get; private set; }

		public void GeneratePreview(bool generateImages, bool generateText)
		{
			if (!_videoPreviewContainer.HasInfo)
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var infoDestination = Path.Combine(Parent.ContainerPath, "info");
					if (!Directory.Exists(infoDestination))
						Directory.CreateDirectory(infoDestination);
					VideoHelper.Instance.ExtractVideoInfo(Parent.OriginalPath, infoDestination);
				}

			var ffMpegData = _videoPreviewContainer.GetFFMpegData();
			if (ffMpegData == null) return;

			if (!String.Equals(Path.GetExtension(Parent.OriginalPath), ".mp4", StringComparison.OrdinalIgnoreCase) || !ffMpegData.IsH264Encoded)
				if (!_videoPreviewContainer.HasMp4)
					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					{
						var mp4Destination = Path.Combine(Parent.ContainerPath, "mp4");
						if (!Directory.Exists(mp4Destination))
							Directory.CreateDirectory(mp4Destination);
						VideoHelper.Instance.ExportMp4(Parent.OriginalPath, mp4Destination, ffMpegData);
					}

			if (!_videoPreviewContainer.HasThumbnail)
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var sourceFile = !ffMpegData.IsH264Encoded ?
						Path.Combine(Parent.ContainerPath, "mp4", Path.ChangeExtension(Path.GetFileName(Parent.OriginalPath), ".mp4")) :
						Parent.OriginalPath;
					var thumbDestination = Path.Combine(Parent.ContainerPath, "thumb");
					if (!Directory.Exists(thumbDestination))
						Directory.CreateDirectory(thumbDestination);
					VideoHelper.Instance.GenerateThumbnails(sourceFile, thumbDestination, ffMpegData);
				}

			Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}
}