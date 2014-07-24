using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

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
			var appID = Guid.Empty;
			var appIDFile = String.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (File.Exists(appIDFile))
			{
				var document = new XmlDocument();
				document.Load(appIDFile);

				var node = document.SelectSingleNode(@"/AppID");
				if (node != null)
					if (!String.IsNullOrEmpty(node.InnerText))
						appID = new Guid(node.InnerText);
			}
			if (appID.Equals(Guid.Empty))
				appID = Guid.NewGuid();
			var folderPath = String.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + appID, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_storageFolderPath = Path.Combine(folderPath, "image_favorites");
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
	}
}
