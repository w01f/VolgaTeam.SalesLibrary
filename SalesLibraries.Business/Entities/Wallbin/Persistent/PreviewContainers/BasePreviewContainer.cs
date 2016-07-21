using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public abstract class BasePreviewContainer : WallbinCollectionEntity
	{
		public const string DocumentSubFolderName = "files";
		public const string VideoSubFolderName = "video";

		#region Persistent Properties
		[Required]
		public string RelativePath { get; set; }
		public virtual Library Library { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return Library; }
			set { Library = value as Library; }
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder { get; set; }

		[NotMapped, JsonIgnore]
		public string SourcePath => Path.Combine(Library.Path, RelativePath);

		[NotMapped, JsonIgnore]
		public string ContainerPath => Path.Combine(
			Library.Path,
			Constants.WebPreviewContainersRootFolderName,
			PreviewSubFolder,
			ExtId.ToString());

		[NotMapped, JsonIgnore]
		protected abstract string PreviewSubFolder { get; }

		[NotMapped, JsonIgnore]
		public abstract string[] AvailablePreviewFormats { get; }

		[NotMapped, JsonIgnore]
		public bool IsUpToDate { get; protected set; }

		[NotMapped, JsonIgnore]
		public bool IsAlive { get; protected set; }
		#endregion

		public override void ResetParent()
		{
			Library = null;
		}

		protected virtual void UpdateState(IEnumerable<PreviewableLink> associatedLinks)
		{
			if (!File.Exists(SourcePath) ||
				associatedLinks.All(link => link.IsDead))
			{
				IsUpToDate = false;
				IsAlive = false;
				return;
			}
			IsAlive = true;
			var sourceFileActualDate = File.GetLastWriteTime(SourcePath);
			var containerActualDate = Directory.GetLastWriteTime(ContainerPath);
			var time = sourceFileActualDate.Subtract(containerActualDate);
			IsUpToDate = time.Minutes <= 0;
		}

		public void UpdateContent(IPreviewGenerator generator, CancellationToken cancellationToken)
		{
			if (generator == null)
				throw new NotImplementedException("Preview Generator is not implemented for that file format");
			var associatedLinks = Library.GetPreviewableLinksBySourcePath(SourcePath).ToList();
			UpdateState(associatedLinks);
			if (IsUpToDate)
				return;
			MarkAsModified();
			if (IsAlive)
			{
				ClearContent();
				generator.Generate(this, cancellationToken);
			}
			else
				DeleteContainer();
		}

		public void ClearContent()
		{
			if (Directory.Exists(ContainerPath))
				Utils.DeleteFolder(ContainerPath);
		}

		public void DeleteContainer()
		{
			ClearContent();
			Library.PreviewContainers.RemoveItem(this);
			Delete(Library.Context);
			ResetParent();
		}

		public IEnumerable<string> GetPreviewLinksByFormat(string format)
		{
			var previewFolderPath = Path.Combine(ContainerPath, format);
			return Directory.Exists(previewFolderPath) ?
				Directory.GetFiles(previewFolderPath).Where(f => !f.Contains(Constants.WindowsThumbnailFile)) :
				new string[] { };
		}

		public static BasePreviewContainer Create(string sourceFile, Library parent)
		{
			BasePreviewContainer previewContainer;
			if (FileFormatHelper.IsPowerPointFile(sourceFile))
				previewContainer = CreateEntity<PowerPointPreviewContainer>();
			else if (FileFormatHelper.IsWordFile(sourceFile))
				previewContainer = CreateEntity<WordPreviewContainer>();
			else if (FileFormatHelper.IsPdfFile(sourceFile))
				previewContainer = CreateEntity<PdfPreviewContainer>();
			else if (FileFormatHelper.IsExcelFile(sourceFile))
				previewContainer = CreateEntity<ExcelPreviewContainer>();
			else if (FileFormatHelper.IsVideoFile(sourceFile))
				previewContainer = CreateEntity<VideoPreviewContainer>();
			else
				return null;
			var relativePath = sourceFile.Replace(parent.Path, String.Empty);
			if (relativePath.StartsWith(@"\"))
				relativePath = relativePath.Substring(1);
			previewContainer.RelativePath = relativePath;
			return previewContainer;
		}
	}
}
