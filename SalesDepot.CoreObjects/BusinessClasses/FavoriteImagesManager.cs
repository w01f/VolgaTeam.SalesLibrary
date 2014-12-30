using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class FavoriteImagesManager
	{
		private static readonly FavoriteImagesManager _instance = new FavoriteImagesManager();
		private readonly string _storageFolderPath;

		public List<ImageSource> Images { get; private set; }
		public event EventHandler<EventArgs> CollectionChanged;

		public static FavoriteImagesManager Instance
		{
			get { return _instance; }
		}

		private FavoriteImagesManager()
		{
			_storageFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\image_favorites", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(_storageFolderPath))
				Directory.CreateDirectory(_storageFolderPath);
			Images = new List<ImageSource>();
			LoadImages();
		}

		protected void OnCollectionChanged()
		{
			var handler = CollectionChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		private void LoadImages()
		{
			Images.Clear();
			foreach (var file in Directory.GetFiles(_storageFolderPath, "*.png"))
			{
				using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
				{
					var imageSource = ImageSource.FromImage(Image.FromStream(fs));
					imageSource.Name = Path.GetFileNameWithoutExtension(file);
					imageSource.FileName = file;
					Images.Add(imageSource);
					fs.Close();
				}
			}
		}

		public void DeleteImage(ImageSource image)
		{
			if (image == null) return;
			if (!File.Exists(image.FileName)) return;
			try
			{
				image.Dispose();
				File.Delete(image.FileName);
				LoadImages();
				OnCollectionChanged();
			}
			catch { }

		}

		public void SaveImage(Image image, string fileName)
		{
			if (image == null) return;
			image.Save(Path.Combine(_storageFolderPath, String.Format("{0}.png", fileName)));
			LoadImages();
			OnCollectionChanged();
		}

		public void SaveImages(Dictionary<string, Image> images)
		{
			foreach (var image in images)
				image.Value.Save(Path.Combine(_storageFolderPath, String.Format("{0}.png", image.Key)));
			LoadImages();
			OnCollectionChanged();
		}
	}
}
