using System.Collections.Generic;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.SalesDepot.Business.LinkViewers
{
	class PresentationPreviewContainer
	{
		private readonly PowerPointLink _sourceLink;

		public List<PresentationSlideThumbnail> Thumbnails { get; set; }

		public PowerPointLinkSettings Settings => (PowerPointLinkSettings)_sourceLink.Settings;

		public PresentationPreviewContainer(PowerPointLink sourceLink)
		{
			_sourceLink = sourceLink;
			Thumbnails = new List<PresentationSlideThumbnail>();
		}

		public void ReleaseThumbnails()
		{
			Thumbnails.ForEach(t => t.Release());
			Thumbnails.Clear();
		}

		public void GetPreviewImages()
		{
			Thumbnails.Clear();
			Thumbnails.AddRange(Directory.GetFiles(Settings.ContainerPath, "*.png")
				.Select(filePath => new PresentationSlideThumbnail(filePath))
				.OrderBy(t => t.Index)
				);
		}
	}
}
