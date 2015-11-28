using System;
using System.Drawing;
using System.IO;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class LinkImageSource
	{
		public int Index { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; private set; }
		public Image Image { get; set; }

		public event EventHandler<EventArgs> AddToFavs;

		protected LinkImageSource(string filePath)
		{
			FilePath = filePath;
			int index;
			if (int.TryParse(Path.GetFileName(filePath).Substring(0, Path.GetFileName(filePath).IndexOf('.')), out index))
				Index = index;
			FileName = Path.GetFileNameWithoutExtension(filePath);
			Image = new Bitmap(filePath);
		}

		public virtual void CopyToFavs()
		{
			if (AddToFavs != null)
				AddToFavs(this, EventArgs.Empty);
		}
	}
}
