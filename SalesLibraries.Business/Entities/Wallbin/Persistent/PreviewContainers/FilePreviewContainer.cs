using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public abstract class FilePreviewContainer : BasePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override string SourcePath => Path.Combine(Library.Path, RelativePath);
		#endregion

		protected override void UpdateState(IEnumerable<IPreviewableLink> associatedLinks)
		{
			base.UpdateState(associatedLinks);
			if (!File.Exists(SourcePath) || associatedLinks.All(link => link.IsDead))
			{
				IsUpToDate = false;
				IsAlive = false;
				return;
			}
			var sourceFileActualDate = File.GetLastWriteTime(SourcePath);
			var containerActualDate = Directory.GetLastWriteTime(ContainerPath);
			var time = sourceFileActualDate.Subtract(containerActualDate);
			IsUpToDate = time.Minutes <= 0;
		}
	}
}
