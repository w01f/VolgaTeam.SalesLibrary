using System;
using System.IO;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class LinkImageSource
	{
		public string FileName { get; set; }
		public string FilePath { get; private set; }

		public event EventHandler<EventArgs> AddToFavs;
		public event EventHandler<EventArgs> RemoveFromFavs;

		protected LinkImageSource(string filePath)
		{
			FilePath = filePath;
			FileName = Path.GetFileNameWithoutExtension(filePath);
		}

		public virtual void CopyToFavs()
		{
			AddToFavs?.Invoke(this, EventArgs.Empty);
		}

		public virtual void DeleteFromFavs()
		{
			RemoveFromFavs?.Invoke(this, EventArgs.Empty);
		}
	}
}
