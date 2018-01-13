using System;
using System.IO;

namespace SalesLibraries.Common.Objects.Graphics
{
	public abstract class BaseImageSource
	{
		public Guid Identifier { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; }

		public event EventHandler<EventArgs> AddToFavs;
		public event EventHandler<EventArgs> RemoveFromFavs;
		public event EventHandler<EventArgs> RemoveFromImported;
		public event EventHandler<EventArgs> RemoveFromResized;

		protected BaseImageSource(string filePath)
		{
			Identifier = Guid.NewGuid();
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

		public virtual void DeleteFromImported()
		{
			RemoveFromImported?.Invoke(this, EventArgs.Empty);
		}

		public virtual void DeleteFromResized()
		{
			RemoveFromResized?.Invoke(this, EventArgs.Empty);
		}
	}
}
