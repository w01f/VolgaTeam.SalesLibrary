using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.InteropClasses;

namespace SalesDepot.BusinessClasses
{
	public class PresentationPreviewContainer
	{
		private readonly LibraryLink _parent;
		private string _folderName = Guid.NewGuid().ToString();
		private string _remotePreviewStorageFolder = string.Empty;

		public PresentationPreviewContainer(LibraryLink parent)
		{
			_parent = parent;
			if (_parent.Parent != null)
			{
				_remotePreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName, _folderName);
				if (SettingsManager.Instance.UseRemoteConnection)
					LocalPreviewStorageFolder = Path.Combine(SettingsManager.Instance.LocalLibraryCacheFolder, _folderName);
				else
					LocalPreviewStorageFolder = _remotePreviewStorageFolder;
			}
			Slides = new List<PresentationPreviewSlide>();
		}

		private string LocalPreviewStorageFolder { get; set; }
		public List<PresentationPreviewSlide> Slides { get; set; }
		public int SelectedIndex { get; set; }
		public Image SelectedSlide
		{
			get
			{
				if (SelectedIndex >= 0 && SelectedIndex < Slides.Count)
					return Slides[SelectedIndex].PreviewImage;
				else
					return null;
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<FolderName>" + _folderName + @"</FolderName>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "FolderName":
						_folderName = childNode.InnerText;
						if (_parent.Parent != null)
						{
							_remotePreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName, _folderName);
							if (SettingsManager.Instance.UseRemoteConnection)
								LocalPreviewStorageFolder = Path.Combine(SettingsManager.Instance.LocalLibraryCacheFolder, _folderName);
							else
								LocalPreviewStorageFolder = _remotePreviewStorageFolder;
						}
						break;
				}
			}
		}

		public void ReleasePreviewImages()
		{
			foreach (PresentationPreviewSlide slide in Slides)
				slide.PreviewImage.Dispose();
			Slides.Clear();
		}

		public void GetPreviewImages()
		{
			if (Directory.Exists(_remotePreviewStorageFolder))
			{
				Slides.Clear();
				var localPreviewImages = new List<string>();
				if (SettingsManager.Instance.UseRemoteConnection)
				{
					if (!Directory.Exists(LocalPreviewStorageFolder))
					{
						Directory.CreateDirectory(LocalPreviewStorageFolder);
						foreach (string imagePath in Directory.GetFiles(_remotePreviewStorageFolder, "*.png"))
						{
							try
							{
								File.Copy(imagePath, Path.Combine(LocalPreviewStorageFolder, Path.GetFileName(imagePath)), true);
							}
							catch {}
						}
					}
				}
				localPreviewImages.AddRange(Directory.GetFiles(LocalPreviewStorageFolder, "*.png"));
				localPreviewImages.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x, y));
				for (int i = 0; i < localPreviewImages.Count; i++)
				{
					var slide = new PresentationPreviewSlide();
					slide.Index = i;
					slide.PreviewImage = new Bitmap(localPreviewImages[i], true);
					Slides.Add(slide);
				}
			}
		}

		public bool CheckPreviewImages()
		{
			bool result = false;
			if (Directory.Exists(_remotePreviewStorageFolder))
				result = Directory.GetFiles(_remotePreviewStorageFolder, "*.png").Length > 0;
			return result;
		}
	}

	public class PresentationPreviewSlide
	{
		public int Index { get; set; }
		public Bitmap PreviewImage { get; set; }
	}
}