﻿using System;
using System.IO;
using FileManager.ToolClasses;
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
			var update = false;
			if (!Parent.Extension.ToUpper().Equals(".MP4"))
			{
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
			}

			if (!Parent.Extension.ToUpper().Equals(".WMV"))
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var wmvDestination = Path.Combine(Parent.ContainerPath, "wmv");
					var updateWmv = !(Directory.Exists(wmvDestination) && Directory.GetFiles(wmvDestination, "*.wmv").Length > 0);
					if (!Directory.Exists(wmvDestination))
						Directory.CreateDirectory(wmvDestination);
					if (updateWmv)
						VideoHelper.Instance.ExportWmv(Parent.OriginalPath, wmvDestination);
					update |= updateWmv;
				}
			}

			if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
			{
				string ogvDestination = Path.Combine(Parent.ContainerPath, "ogv");
				bool updateOgv = !(Directory.Exists(ogvDestination) && Directory.GetFiles(ogvDestination, "*.ogv").Length > 0);
				if (!Directory.Exists(ogvDestination))
					Directory.CreateDirectory(ogvDestination);
				if (updateOgv)
					VideoHelper.Instance.ExportOgv(Parent.OriginalPath, ogvDestination);
				update |= updateOgv;
			}
			if (update)
				Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}
}