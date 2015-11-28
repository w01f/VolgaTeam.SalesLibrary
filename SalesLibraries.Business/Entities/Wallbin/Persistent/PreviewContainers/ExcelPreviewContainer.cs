using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class ExcelPreviewContainer : BasePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder
		{
			get { return DocumentSubFolderName; }
		}

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats
		{
			get { return new[] { PreviewFormats.Text }; }
		}

		[NotMapped, JsonIgnore]
		public bool GenerateText { get; private set; }
		#endregion

		protected override void UpdateState(IEnumerable<PreviewableLink> associatedLinks)
		{
			var associatedLinksList = associatedLinks.ToList();
			GenerateText = associatedLinksList
				.Select(link => link.Settings)
				.OfType<ExcelLinkSettings>()
				.Any(set => set.GenerateContentText);
			if (!GenerateText)
			{
				IsUpToDate = IsAlive = false;
				return;
			}
			base.UpdateState(associatedLinksList);
			if (!IsUpToDate)
				return;
			IsUpToDate = AvailablePreviewFormats.All(previewFormat => Directory.Exists(Path.Combine(ContainerPath, previewFormat)));
		}
	}
}
