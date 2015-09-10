using System.Drawing;
using System.IO;

namespace FileManager.BusinessClasses
{
	public abstract class LinkImageSource
	{
		protected string _filePath;

		public int Index { get; set; }
		public string FileName { get; set; }
		public Image Image { get; set; }

		public LinkImageSource(string filePath)
		{
			_filePath = filePath;
			int index;
			if (int.TryParse(Path.GetFileName(filePath).Substring(0, Path.GetFileName(filePath).IndexOf('.')), out index))
				Index = index;
			FileName = Path.GetFileNameWithoutExtension(filePath);
			Image = new Bitmap(filePath);
		}

		public abstract void CopyToFavs();
	}
}
