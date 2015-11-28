using System;
using System.IO;
using Newtonsoft.Json;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class PowerPointLinkSettings : DocumentLinkSettings
	{
		public Guid Id { get; set; }

		private double _height;
		public double Height
		{
			get { return _height; }
			set
			{
				if (_height != value)
					OnSettingsChanged();
				_height = value;
			}
		}

		private double _width;
		public double Width
		{
			get { return _width; }
			set
			{
				if (_width != value)
					OnSettingsChanged();
				_width = value;
			}
		}

		[JsonIgnore]
		public string Orientation
		{
			get
			{
				return Height < Width ? "Landscape" : "Portrait";
			}
		}

		[JsonIgnore]
		public string Size
		{
			get
			{
				if (Width == 10 && Height == 7.5)
					return "4x3";
				if (Width == 10.75 && Height == 8.25)
					return "5x4";
				if (Width == 13 && Height == 7.32)
					return "16x9";
				if (Width == 7.5 && Height == 10)
					return "3x4";
				if (Width == 8.25 && Height == 10.75)
					return "4x5";
				if (Width == 7.32 && Height == 13)
					return "9x16";
				return "4x3";
			}
		}

		public string ContainerPath
		{
			get { return Path.Combine(ParentFileLink.ParentLibrary.Path, Constants.RegularPreviewContainersRootFolderName, Id.ToString()); }
		}

		public PowerPointLinkSettings()
		{
			Id = Guid.NewGuid();
		}

		public void UpdateSizeInfo(bool useExistedConnection = false)
		{
			if (!PowerPointHelper.Instance.ConnectHidden() && !useExistedConnection) return;
			double height;
			double width;
			PowerPointHelper.Instance.GetPresentationProperties(ParentFileLink.FullPath, out width, out height);
			Width = width;
			Height = height;
			if (!useExistedConnection)
				PowerPointHelper.Instance.Disconnect();
		}

		public void UpdateQuickViewContent(bool useExistedConnection = false)
		{
			var parentFile = new FileInfo(ParentFileLink.FullPath);
			var previewFolder = new DirectoryInfo(ContainerPath);
			var needToUpdate = false;
			if (!previewFolder.Exists)
				needToUpdate = true;
			else if (parentFile.LastWriteTime > previewFolder.CreationTime)
				needToUpdate = true;
			if (!needToUpdate) return;
			ClearQuickViewContent();
			if (!Directory.Exists(Path.Combine(ParentFileLink.ParentLibrary.Path, Constants.RegularPreviewContainersRootFolderName)))
				Directory.CreateDirectory(Path.Combine(ParentFileLink.ParentLibrary.Path, Constants.RegularPreviewContainersRootFolderName));
			Directory.CreateDirectory(ContainerPath);
			PowerPointHelper.Instance.ExportPresentationAsImages(ParentFileLink.FullPath, ContainerPath, !useExistedConnection);
			PngHelper.ConvertFiles(ContainerPath);
		}

		public void ClearQuickViewContent()
		{
			if (Directory.Exists(ContainerPath))
				Utils.DeleteFolder(ContainerPath);
		}
	}
}
