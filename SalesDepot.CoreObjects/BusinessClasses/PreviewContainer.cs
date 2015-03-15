﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public interface IPreviewStorage
	{
		string StoragePath { get; }
		List<IPreviewContainer> PreviewContainers { get; }
		IPreviewContainer GetPreviewContainer(string originalPath);
		IPreviewGenerator GetPreviewGenerator(IPreviewContainer previewContainer);
		void UpdatePreviewableObject(string originalPath, DateTime lastChanged);
		void DeletePreviewableObject(string originalPath);
		void UpdatePreviewContainers();
	}

	public interface IPreviewContainer
	{
		IPreviewStorage Parent { get; }
		string Identifier { get; }
		string OriginalPath { get; set; }
		FileTypes Type { get; }
		string Extension { get; }
		DateTime LastChanged { set; }
		string ContainerPath { get; }
		bool GenerateImages { get; set; }
		bool GenerateText { get; set; }
		bool Ready { get; }
		IPreviewContainer Clone(IPreviewStorage parent);
		string Serialize();
		void Deserialize(XmlNode node);
		string[] GetPreviewLinks(string format);
		void UpdateContent();
		void ClearContent();
		void DeleteRelatedLinks();
		Size GetThumbSize();
	}

	public class UniversalPreviewContainer : IPreviewContainer
	{
		protected UniversalPreviewContainer(IPreviewStorage parent)
		{
			Parent = parent;
			Identifier = Guid.NewGuid().ToString();
			GenerateImages = true;
			GenerateText = true;
		}

		public override string ToString()
		{
			return OriginalPath;
		}

		public string RelativePath
		{
			get
			{
				string result = OriginalPath.Replace(Parent.StoragePath, string.Empty);
				if (!String.IsNullOrEmpty(result) && result.Substring(0, 1) == @"\")
					result = result.Substring(1, result.Length - 1);
				return result;
			}
		}

		#region IPreviewContainer Members
		public IPreviewStorage Parent { get; private set; }
		public string Identifier { get; private set; }
		public virtual string ContainerPath
		{
			get { return Path.Combine(Parent.StoragePath, Constants.FtpPreviewContainersRootFolderName, "files", Identifier); }
		}

		private bool _generatePreviewImages;
		public bool GenerateImages
		{
			get
			{
				return _generatePreviewImages &&
				  (Type == FileTypes.Presentation ||
				  Type == FileTypes.Word ||
				  Type == FileTypes.PDF ||
				  ((Type == FileTypes.Other && new[] { ".ppt", ".pptx", ".doc", ".docx", ".pdf" }.Contains(Extension.ToLower()))));
			}
			set { _generatePreviewImages = value; }
		}
		public bool GenerateText { get; set; }
		public virtual FileTypes Type
		{
			get
			{
				switch (Extension.ToUpper())
				{
					case ".PPT":
					case ".PPTX":
						return FileTypes.Presentation;
					case ".DOC":
					case ".DOCX":
					case ".XLS":
					case ".XLSX":
					case ".PDF":
						return FileTypes.Other;
					default:
						return FileTypes.Url;
				}
			}
		}

		private string _originalPath = String.Empty;
		public string OriginalPath
		{
			get { return _originalPath; }
			set { _originalPath = value; }
		}

		public string Extension
		{
			get { return Path.GetExtension(_originalPath); }
		}

		public DateTime LastChanged
		{
			set { Parent.UpdatePreviewableObject(OriginalPath, value); }
		}

		public bool Ready
		{
			get
			{
				switch (Type)
				{
					case FileTypes.MediaPlayerVideo:
					case FileTypes.QuickTimeVideo:
						var mp4Destination = Path.Combine(ContainerPath, "mp4");
						var wmvDestination = Path.Combine(ContainerPath, "wmv");
						var ogvDestination = Path.Combine(ContainerPath, "ogv");
						if (Extension.ToUpper().Equals(".MP4"))
							return (Directory.Exists(wmvDestination) && Directory.GetFiles(wmvDestination, "*.wmv").Length > 0)
								&& (Directory.Exists(ogvDestination) && Directory.GetFiles(ogvDestination, "*.ogv").Length > 0);
						if (Extension.ToUpper().Equals(".WMV"))
							return (Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination, "*.mp4").Length > 0)
								&& (Directory.Exists(ogvDestination) && Directory.GetFiles(ogvDestination, "*.ogv").Length > 0);
						return (Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination, "*.mp4").Length > 0)
							&& (Directory.Exists(wmvDestination) && Directory.GetFiles(wmvDestination, "*.wmv").Length > 0)
							&& (Directory.Exists(ogvDestination) && Directory.GetFiles(ogvDestination, "*.ogv").Length > 0);
					default:
						return true;
				}
			}
		}

		public virtual IPreviewContainer Clone(IPreviewStorage parent)
		{
			IPreviewContainer previewContainer = new UniversalPreviewContainer(parent);
			previewContainer.OriginalPath = Path.Combine(parent.StoragePath, RelativePath);
			return previewContainer;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier + @"</Identifier>");
			result.AppendLine(@"<OriginalPath>" + RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</OriginalPath>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "FolderName":
					case "Identifier":
						Identifier = childNode.InnerText;
						break;
					case "OriginalPath":
						OriginalPath = Path.Combine(Parent.StoragePath, childNode.InnerText.ToLower().Replace(Parent.StoragePath.ToLower(), string.Empty));
						break;
				}
			}
		}

		public string[] GetPreviewLinks(string format)
		{
			var result = new List<string>();
			if (!string.IsNullOrEmpty(ContainerPath))
			{
				var previewFolder = string.Empty;
				if (format.Equals("old office"))
				{
					switch (Extension.ToUpper())
					{
						case ".PPT":
						case ".PPTX":
							previewFolder = Path.Combine(ContainerPath, "ppt");
							break;
						case ".DOC":
						case ".DOCX":
							previewFolder = Path.Combine(ContainerPath, "doc");
							break;
					}
				}
				else if (format.Equals("new office"))
				{
					switch (Extension.ToUpper())
					{
						case ".PPT":
						case ".PPTX":
							previewFolder = Path.Combine(ContainerPath, "pptx");
							break;
						case ".DOC":
						case ".DOCX":
							previewFolder = Path.Combine(ContainerPath, "docx");
							break;
					}
				}
				else
					previewFolder = Path.Combine(ContainerPath, format);
				if (Directory.Exists(previewFolder))
					result.AddRange(Directory.GetFiles(previewFolder).Where(x => !x.ToLower().Contains("thumbs.db")).Select(x => x.Replace(Parent.StoragePath, string.Empty)));
			}
			return result.ToArray();
		}

		public Size GetThumbSize()
		{
			var size = new Size(0, 0);
			if (!string.IsNullOrEmpty(ContainerPath))
			{
				string thumbsFolder = Path.Combine(ContainerPath, "thumbs");
				if (Directory.Exists(thumbsFolder))
					foreach (string thumbFile in Directory.GetFiles(thumbsFolder, "*.png"))
					{
						try
						{
							size = Image.FromFile(thumbFile).Size;
						}
						catch { }
					}
			}
			return size;
		}

		public virtual void UpdateContent()
		{
			var parentFile = new FileInfo(OriginalPath);
			if (string.IsNullOrEmpty(ContainerPath)) return;
			var previewFolder = new DirectoryInfo(ContainerPath);
			bool update;
			if (!previewFolder.Exists)
				update = true;
			else if ((GenerateImages && !previewFolder.GetDirectories().Any(d => new[] { "png", "jpeg" }.Contains(d.Name))) ||
				(!GenerateImages && previewFolder.GetDirectories().Any(d => new[] { "png", "jpeg" }.Contains(d.Name))))
				update = true;
			else if ((GenerateText && !previewFolder.GetDirectories().Any(d => d.Name.Equals("txt"))) ||
				(!GenerateText && previewFolder.GetDirectories().Any(d => d.Name.Equals("txt"))))
				update = true;
			else
			{
				var time = parentFile.LastWriteTime.Subtract(previewFolder.CreationTime);
				if (time.Minutes > 0)
					update = true;
				else if (!parentFile.Exists)
					update = true;
				else
					update = false;
			}
			if (previewFolder.Exists && update)
				SyncManager.DeleteFolder(previewFolder);
			if (!parentFile.Exists) return;
			var previewGenerator = Parent.GetPreviewGenerator(this);
			if (previewGenerator != null)
				previewGenerator.GeneratePreview(GenerateImages, GenerateText);
		}

		public void ClearContent()
		{
			if (string.IsNullOrEmpty(ContainerPath)) return;
			var previewFolder = new DirectoryInfo(ContainerPath);
			if (previewFolder.Exists)
				SyncManager.DeleteFolder(previewFolder);
		}

		public void DeleteRelatedLinks()
		{
			if (string.IsNullOrEmpty(OriginalPath)) return;
			Parent.DeletePreviewableObject(OriginalPath);
		}
		#endregion

		public static UniversalPreviewContainer CreateInstance(IPreviewStorage parent, XmlNode node)
		{
			var pathNode = node.SelectSingleNode("OriginalPath");
			if (pathNode == null) return null;
			var originalPath = Path.Combine(parent.StoragePath, pathNode.InnerText.ToLower().Replace(parent.StoragePath.ToLower(), String.Empty));
			var previewContainer = CreateInstance(parent, originalPath);
			previewContainer.Deserialize(node);
			return previewContainer;
		}

		public static UniversalPreviewContainer CreateInstance(IPreviewStorage parent, string originalPath)
		{
			UniversalPreviewContainer previewContainer;
			switch (Path.GetExtension(originalPath).ToUpper())
			{
				case ".MPEG":
				case ".WMV":
				case ".AVI":
				case ".WMZ":
				case ".MPG":
				case ".ASF":
				case ".MOV":
				case ".MP4":
				case ".M4V":
				case ".FLV":
				case ".OGV":
				case ".OGM":
				case ".OGX":
					previewContainer = new VideoPreviewContainer(parent);
					break;
				default:
					previewContainer = new UniversalPreviewContainer(parent);
					break;
			}
			previewContainer.OriginalPath = originalPath;
			return previewContainer;
		}
	}

	public class VideoPreviewContainer : UniversalPreviewContainer
	{
		public override string ContainerPath
		{
			get { return Path.Combine(Parent.StoragePath, Constants.FtpPreviewContainersRootFolderName, "video", Identifier); }
		}

		public override FileTypes Type
		{
			get
			{
				switch (Extension.ToUpper())
				{
					case ".MPEG":
					case ".WMV":
					case ".AVI":
					case ".WMZ":
						return FileTypes.MediaPlayerVideo;
					case ".MPG":
					case ".ASF":
					case ".MOV":
					case ".MP4":
					case ".M4V":
					case ".FLV":
					case ".OGV":
					case ".OGM":
					case ".OGX":
						return FileTypes.QuickTimeVideo;
					default:
						return base.Type;
				}
			}
		}

		public VideoPreviewContainer(IPreviewStorage parent) : base(parent) { }

		public override IPreviewContainer Clone(IPreviewStorage parent)
		{
			IPreviewContainer previewContainer = new VideoPreviewContainer(parent);
			previewContainer.OriginalPath = Path.Combine(parent.StoragePath, RelativePath);
			return previewContainer;
		}

		public override void UpdateContent()
		{
			GenerateImages = false;
			GenerateText = true;
			base.UpdateContent();
			UpdateThumbnails();
		}

		public void UpdateThumbnails()
		{
			var parentFile = new FileInfo(OriginalPath);
			if (string.IsNullOrEmpty(ContainerPath)) return;
			var previewFolder = new DirectoryInfo(Path.Combine(ContainerPath, "thumb"));
			bool update;
			if (!previewFolder.Exists)
				update = true;
			else
			{
				var time = parentFile.LastWriteTime.Subtract(previewFolder.CreationTime);
				if (time.Minutes > 0)
					update = true;
				else if (!parentFile.Exists)
					update = true;
				else
					update = false;
			}
			if (previewFolder.Exists && update)
				SyncManager.DeleteFolder(previewFolder);
			if (!parentFile.Exists) return;
			var previewGenerator = Parent.GetPreviewGenerator(this);
			if (previewGenerator != null)
				previewGenerator.GeneratePreview(true, false);
		}
	}

	#region Compatibility with desktop version of Sales Depot
	public class PresentationPreviewContainer
	{
		public PresentationPreviewContainer(ILibraryLink parent)
		{
			Parent = parent;
			Identifier = Guid.NewGuid().ToString();
			ContainerPath = Path.Combine(Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName, Identifier);
		}

		public ILibraryLink Parent { get; private set; }
		public string Identifier { get; private set; }
		public string ContainerPath { get; private set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<FolderName>" + Identifier + @"</FolderName>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "FolderName":
						Identifier = childNode.InnerText;
						ContainerPath = Path.Combine(Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName, Identifier);
						break;
				}
			}
		}

		public string[] GetPreviewLinks(string format)
		{
			return null;
		}

		public string GetTextContent()
		{
			string result = string.Empty;
			return result;
		}

		public void UpdateContent()
		{
			var parentFile = new FileInfo(Parent.OriginalPath);
			var previewFolder = new DirectoryInfo(ContainerPath);
			bool needToUpdate = false;
			if (!previewFolder.Exists)
				needToUpdate = true;
			else if (parentFile.LastWriteTime > previewFolder.CreationTime)
				needToUpdate = true;
			if (!needToUpdate) return;
			if (!Directory.Exists(Path.Combine(Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName)))
				Directory.CreateDirectory(Path.Combine(Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName));
			if (previewFolder.Exists)
				SyncManager.DeleteFolder(previewFolder);
			Directory.CreateDirectory(ContainerPath);
			PowerPointHelper.Instance.ExportPresentationAsImages(Parent.OriginalPath, ContainerPath);
		}

		public Size GetThumbSize()
		{
			return new Size(0, 0);
		}

		public void ClearContent()
		{
			var previewFolder = new DirectoryInfo(ContainerPath);
			if (previewFolder.Exists)
				SyncManager.DeleteFolder(previewFolder);
		}
	}
	#endregion
}