using System;
using System.Data;
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

		private DateTime? _lastPresentationInfoUpdate;
		public DateTime? LastPresentationInfoUpdate
		{
			get { return _lastPresentationInfoUpdate; }
			set
			{
				if (_lastPresentationInfoUpdate != value)
					OnSettingsChanged();
				_lastPresentationInfoUpdate = value;
			}
		}

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
		private DateTime? _fakeFileDate;

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

		public DateTime? FakeFileDate
		{
			get { return _fakeFileDate; }
			set
			{
				if (_fakeFileDate != value)
					OnSettingsChanged();
				_fakeFileDate = value;
			}
		}

		[JsonIgnore]
		public string Orientation => Height < Width ? "Landscape" : "Portrait";

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
				if (Width == 13.333333333333334 && Height == 7.5)
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

		public string ContainerPath => Path.Combine(ParentFileLink.ParentLibrary.Path, Constants.RegularPreviewContainersRootFolderName, Id.ToString());

		public PowerPointLinkSettings()
		{
			Id = Guid.NewGuid();
		}

		public void UpdateSizeInfo()
		{
			using (var powerPointProcesor = new PowerPointHidden())
			{
				if (!powerPointProcesor.Connect(true)) return;
				UpdateSizeInfo(powerPointProcesor);
			}
		}

		public void UpdateSizeInfo(PowerPointProcessor powerPointProcessor)
		{
			double height;
			double width;
			powerPointProcessor.GetPresentationProperties(ParentFileLink.FullPath, out width, out height);
			Width = width;
			Height = height;
		}

		private void UpdateThemes(PowerPointProcessor powerPointProcessor)
		{
			powerPointProcessor.RenameDefaultOfficeDesigns(ParentFileLink.FullPath);
		}

		public void UpdatePresentationInfo(PowerPointProcessor powerPointProcessor, bool force = false)
		{
			var sourceFile = new FileInfo(ParentFileLink.FullPath);
			if (!sourceFile.Exists) return;
			if (LastPresentationInfoUpdate.HasValue && LastPresentationInfoUpdate == sourceFile.LastWriteTime) return;
			UpdateSizeInfo(powerPointProcessor);
			UpdateThemes(powerPointProcessor);
			LastPresentationInfoUpdate = sourceFile.LastWriteTime;
		}

		public void UpdateQuickViewContent(PowerPointProcessor powerPointProcessor)
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
			powerPointProcessor.ExportPresentationAsImages(ParentFileLink.FullPath, ContainerPath);
			PngHelper.ConvertFiles(ContainerPath);
		}

		public void ClearQuickViewContent()
		{
			if (Directory.Exists(ContainerPath))
				Utils.DeleteFolder(ContainerPath);
		}
	}
}
